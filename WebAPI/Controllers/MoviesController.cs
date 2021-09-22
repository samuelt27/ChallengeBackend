using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChallengeBackend.WebAPI.DataAccess;
using ChallengeBackend.WebAPI.DTOs.Movies;
using ChallengeBackend.WebAPI.Entities;
using ChallengeBackend.WebAPI.Helpers;
using ChallengeBackend.WebAPI.Helpers.Enums;
using ChallengeBackend.WebAPI.Interfaces;
using ChallengeBackend.WebAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStore _fileStore;

        public MoviesController(ApplicationDbContext dbContext, IMapper mapper, IFileStore fileStore)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStore = fileStore;
        }


        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetMoviesDto>>> GetMovies([FromQuery] MoviesFilterDto filter)
        {
            var entities = _dbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                entities = entities.Where(i => i.Title.Contains(filter.Title));

            if (!string.IsNullOrEmpty(filter.Genre))
                entities = _dbContext.MovieGenres
                    .Where(i => i.Genre.Name.Contains(filter.Genre))
                    .Select(i => i.Movie);

            if(!string.IsNullOrEmpty(filter.Order))
            {
                if (filter.Order.ToUpper() == Order.ASC.ToString())
                    entities = entities.OrderBy(i => i.Release);

                if (filter.Order.ToUpper() == Order.DESC.ToString())
                    entities = entities.OrderByDescending(i => i.Release);
            }
            
            var response = await entities
                .AsNoTracking()
                .ProjectTo<GetMoviesDto>(_mapper.ConfigurationProvider)
                .PaginatedResponseAsync(filter.PageNumber, filter.PageSize);

            return StatusCode(200, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMovieDetailsDto>> GetMovieById(int id)
        {
            var entity = await _dbContext.Movies
                .AsNoTracking()
                .Include(i => i.MovieGenres).ThenInclude(i => i.Genre)
                .Include(i => i.MovieCharacters).ThenInclude(i => i.Character)
                .SingleOrDefaultAsync(i => i.Id.Equals(id));

            if(entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Movie)} de id ({id})");

            return StatusCode(200, _mapper.Map<GetMovieDetailsDto>(entity));
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateMovie([FromForm] CreateMovieDto dto)
        {
            var entity = _mapper.Map<Movie>(dto);

            if (dto.Image is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);

                    var content = memoryStream.ToArray();

                    var extension = Path.GetExtension(dto.Image.FileName);

                    entity.Image = await _fileStore.SaveFile(content, extension, "Movies", dto.Image.ContentType);
                }
            }

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return StatusCode(201, entity.Id);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, CreateMovieDto dto)
        {
            var entity = await _dbContext.Movies.SingleOrDefaultAsync(i => i.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Movie)} de id ({id})");

            _mapper.Map(dto, entity);

            if (dto.Image is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);

                    var content = memoryStream.ToArray();

                    var extension = Path.GetExtension(dto.Image.FileName);

                    entity.Image = await _fileStore.EditFile(content, extension, "Movies", entity.Image, dto.Image.ContentType);
                }
            }

            await _dbContext.SaveChangesAsync();

            return StatusCode(204);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var entity = await _dbContext.Movies.SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Movie)} de id ({id})");

            _dbContext.Remove(entity);
            await _fileStore.DeleteFile(entity.Image, "Movies");
            await _dbContext.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
