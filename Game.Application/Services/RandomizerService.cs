using System.Text.Json;
using Game.Application.Dtos;
using Game.Application.Exceptions;
using Game.Application.Options;
using Game.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Game.Application.Services;

public class RandomizerService : IRandomizerService
{
    private readonly Randomizer _randomizerOptions;
    private readonly GameDbContext _dbContext;

    public RandomizerService(GameDbContext dbContext, IOptions<Randomizer> randomizerOptions)
    {
        _randomizerOptions = randomizerOptions.Value;
        _dbContext = dbContext;
    }

    public async Task<int> GetRandomChoice()
    {
        HttpClient client = new();
        client.Timeout = TimeSpan.FromSeconds(5);
        HttpResponseMessage response = await client.GetAsync(_randomizerOptions.Url);

        if (!response.IsSuccessStatusCode)
        {
            throw new AppException("Problem communication with external service.");
        }

        string stringResponse = await response.Content.ReadAsStringAsync();
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        RandomizerResponse result = JsonSerializer.Deserialize<RandomizerResponse>(stringResponse, options);

        return await GetRandomId(result.Random);
    }

    private async Task<int> GetRandomId(int n)
    {
        List<int> ids = await _dbContext.Choices.Select(x => x.Id).ToListAsync();
        int index = (n + 1) % ids.Count;
        return ids[index];
    }
}
