using Game.Application.Dtos;
using Game.Application.Enums;
using Game.Application.Exceptions;
using Game.Data;
using Game.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Game.Application.Services;

public class GameService : IGameService
{
    private readonly GameDbContext _dbContext;
    private readonly IRandomizerService _randomizerService;

    public GameService(GameDbContext dbContext, IRandomizerService randomizerService)
    {
        _dbContext = dbContext;
        _randomizerService = randomizerService;
    }

    public async Task<List<ChoiceResponse>> GetChoicesAsync()
    {
        return await _dbContext.Choices.Select(x => ChoiceResponse.Map(x)).ToListAsync();
    }

    public async Task<ChoiceResponse> GetRandomChoiceAsync()
    {
        int selectedId = await _randomizerService.GetRandomChoice();

        Choice choice = await _dbContext.Choices.FirstOrDefaultAsync(x => x.Id == selectedId) ??
                        throw new AppException("Choice not found");

        return ChoiceResponse.Map(choice);
    }

    public async Task<PlayResponse> PlayAsync(int playerChoiceId)
    {
        int botChoiceId = await _randomizerService.GetRandomChoice();

        Result result = Result.Tie;
        if (playerChoiceId != botChoiceId)
        {
            bool ruleFound = await _dbContext.Rules.AnyAsync(x => x.ChoiceId == playerChoiceId && x.WinAgainstId == botChoiceId);

            result = ruleFound ? Result.Win : Result.Lose;
        }

        return new()
        {
            Result = result,
            Player = playerChoiceId,
            Bot = botChoiceId
        };
    }
}