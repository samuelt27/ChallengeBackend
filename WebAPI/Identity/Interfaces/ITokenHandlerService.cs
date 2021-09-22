namespace ChallengeBackend.WebAPI.Identity.Interfaces
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters parameters);
    }
}
