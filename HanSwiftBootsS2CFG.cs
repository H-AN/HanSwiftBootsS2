namespace HanSwiftBootsS2;

public class HanSwiftBootsS2CFG
{
    // SteamID 
    public Dictionary<string, SteamIDConfig> Players { get; set; } = new();

    // Team 配置
    public TeamConfig Teams { get; set; } = new();
}

public class SteamIDConfig
{
    public bool Enabled { get; set; } = true;
    public int Team { get; set; } = 0; //0=全队,1=CT,2=T
    public float JumpBoostHeight { get; set; } = 500;
    public int DurationTicks { get; set; } = 8;
}

public class TeamConfig
{
    public TeamEntry CT { get; set; } = new();
    public TeamEntry T { get; set; } = new();
}

public class TeamEntry
{
    public bool Enabled { get; set; } = true;
    public float JumpBoostHeight { get; set; } = 500;
    public int DurationTicks { get; set; } = 8;
}
