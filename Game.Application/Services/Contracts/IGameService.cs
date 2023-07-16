using Game.Application.Dtos;

namespace Game.Application.Services;

public interface IGameService
{
    Task<List<ChoiceResponse>> GetChoicesAsync();
    Task<ChoiceResponse> GetRandomChoiceAsync();
    Task<PlayResponse> PlayAsync(int playerChoiceId);
}