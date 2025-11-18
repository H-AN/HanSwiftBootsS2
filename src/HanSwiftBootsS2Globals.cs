

using SwiftlyS2.Shared.Players;

namespace HanSwiftBootsS2;

public class HanSwiftBootsS2Globals
{
    public Dictionary<IPlayer, int> JumpBoostState { get; } = new();

    public Dictionary<IPlayer, float> JumpBoostHeight { get; } = new();


}