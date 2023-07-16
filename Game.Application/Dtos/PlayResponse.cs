using Game.Application.Enums;

namespace Game.Application.Dtos;

public class PlayResponse
{
    public Result Result { get; set; }
    public int Player { get; set; }
    public int Bot { get; set; }
}
