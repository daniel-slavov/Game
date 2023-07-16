using Game.Application.Dtos;
using Game.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<List<ChoiceResponse>> Choices()
    {
        return await _gameService.GetChoicesAsync();
    }

    [HttpGet]
    public async Task<ChoiceResponse> Choice()
    {
        return await _gameService.GetRandomChoiceAsync();
    }

    [HttpPost]
    public async Task<PlayResponse> Play(PlayRequest request)
    {
        return await _gameService.PlayAsync(request.Player);
    }
}