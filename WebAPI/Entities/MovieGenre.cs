namespace ChallengeBackend.WebAPI.Entities
{
    public record MovieGenre
    {
        public int GenreId { get; init; }
        public Genre Genre { get; init; }
        public int MovieId { get; init; }
        public Movie Movie { get; init; }
    }
}
