namespace Game.Application.Services;

public interface IRandomizerService
{
    public Task<int> GetRandomChoice();
}
