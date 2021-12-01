using ChallengeBackend.WebAPI.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedResponse<TDestination>> PaginatedResponseAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedResponse<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }
}
