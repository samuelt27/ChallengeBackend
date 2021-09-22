using ChallengeBackend.WebAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.Services
{
    public class LocalFileStore : IFileStore
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;


        public LocalFileStore(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
        }


        public async Task<string> SaveFile(byte[] content, string extension, string container, string contenType)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";

            string folder = Path.Combine(_environment.WebRootPath, container);

            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(path, content);

            var currentUrl = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";

            var saveUrl = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");

            return saveUrl;
        }

        public Task DeleteFile(string path, string container)
        {
            if(path is not null)
            {
                var fileName = Path.GetFileName(path);

                var fileDirectory = Path.Combine(_environment.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                    File.Delete(fileDirectory);
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType)
        {
            await DeleteFile(path, container);

            return await SaveFile(content, extension, container, contentType);
        }
    }
}
