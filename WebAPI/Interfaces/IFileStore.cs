using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.Interfaces
{
    public interface IFileStore
    {
        Task<string> SaveFile(byte[] content, string extension, string container, string contenType);

        Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType);

        Task DeleteFile(string path, string container);
    }
}
