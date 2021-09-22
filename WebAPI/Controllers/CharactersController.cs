using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChallengeBackend.WebAPI.DataAccess;
using ChallengeBackend.WebAPI.DTOs.Characters;
using ChallengeBackend.WebAPI.Entities;
using ChallengeBackend.WebAPI.Helpers;
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
    public class CharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStore _fileStore;

        public CharactersController(ApplicationDbContext dbContext, IMapper mapper, IFileStore fileStore)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStore = fileStore;
        }


        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetCharactersDto>>> GetCharacters([FromQuery] CharactersFilterDto filter)
        {
            var entities = _dbContext.Characters.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                entities = entities.Where(i => i.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Movie))
                entities = _dbContext.MovieCharacters
                    .Where(i => i.Movie.Title.Contains(filter.Movie))
                    .Select(i => i.Character);
            
            var response = await entities
                .AsNoTracking()
                .ProjectTo<GetCharactersDto>(_mapper.ConfigurationProvider)
                .PaginatedResponseAsync(filter.PageNumber, filter.PageSize);

            return StatusCode(200, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterDetailsDto>> GetCharacterById(int id)
        {
            var entity = await _dbContext.Characters
                .AsNoTracking()
                .Include(i => i.MovieCharacters).ThenInclude(i => i.Movie)
                .SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Character)} de id ({id})");

            return StatusCode(200, _mapper.Map<GetCharacterDetailsDto>(entity));
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateCharacter([FromForm] CreateCharacterDto dto)
        {
            var entity = _mapper.Map<Character>(dto);

            if (dto.Image is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);

                    var content = memoryStream.ToArray();

                    var extension = Path.GetExtension(dto.Image.FileName);

                    entity.Image = await _fileStore.SaveFile(content, extension, "Characters", dto.Image.ContentType);
                }
            }

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return StatusCode(201, entity.Id);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, [FromForm] CreateCharacterDto dto)
        {
            var entity = await _dbContext.Characters.SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Character)} de id ({id})");

            _mapper.Map(dto, entity);

            if (dto.Image is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);

                    var content = memoryStream.ToArray();

                    var extension = Path.GetExtension(dto.Image.FileName);

                    entity.Image = await _fileStore.EditFile(content, extension, "Characters", entity.Image, dto.Image.ContentType);
                }
            }

            await _dbContext.SaveChangesAsync();

            return StatusCode(204);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var entity = await _dbContext.Characters.SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (entity is null)
                return StatusCode(404, $"No se encontró la entidad {nameof(Character)} de id ({id})");

            _dbContext.Remove(entity);
            await _fileStore.DeleteFile(entity.Image, "Characters");
            await _dbContext.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
