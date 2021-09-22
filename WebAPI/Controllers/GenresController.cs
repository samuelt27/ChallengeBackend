using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChallengeBackend.WebAPI.DataAccess;
using ChallengeBackend.WebAPI.DTOs.Genres;
using ChallengeBackend.WebAPI.Entities;
using ChallengeBackend.WebAPI.Helpers;
using ChallengeBackend.WebAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenresController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GenreDto>>> GetGenres([FromQuery] GenresFilterDto filter)
        {
            var entities = _dbContext.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                entities = entities.Where(i => i.Name.Contains(filter.Name));

            var response = await entities
                .AsNoTracking()
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .PaginatedResponseAsync(filter.PageNumber, filter.PageSize);

            return StatusCode(200, response);
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateGenre([FromForm] CreateGenreDto dto)
        {
            var entity = _mapper.Map<Genre>(dto);

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return StatusCode(201, entity.Id);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenre(int id, [FromForm] CreateGenreDto dto)
        {
            var entity = await _dbContext.Genres.SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Genre)} de id ({id})");

            _mapper.Map(dto, entity);
            await _dbContext.SaveChangesAsync();

            return StatusCode(204);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var entity = await _dbContext.Genres.SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Genre)} de id ({id})");

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
