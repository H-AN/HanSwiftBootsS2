

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace HanSwiftBootsS2;

[PluginMetadata(
    Id = "HanSwiftBootsS2",
    Version = "1.0.0",
    Name = "飞鞋 for Sw2/SwiftBoots for Sw2",
    Author = "H-AN",
    Description = "飞鞋,让你跳跃的更高!/SwiftBoots Jump higher")]
public partial class HanSwiftBootsS2(ISwiftlyCore core) : BasePlugin(core)
{
    private ServiceProvider? ServiceProvider { get; set; }

    private HanSwiftBootsS2CFG _SwiftBootsCFG = null!;
    private HanSwiftBootsS2Globals _Globals = null!;
    private HanSwiftBootsS2Service _Service = null!;
    public override void Load(bool hotReload)
    {
        Core.Configuration.InitializeJsonWithModel<HanSwiftBootsS2CFG>("HanSwiftBootsS2CFG.jsonc", "SwiftBootsS2CFG").Configure(builder =>
        {
            builder.AddJsonFile("HanSwiftBootsS2CFG.jsonc", false, true);
        });

        var collection = new ServiceCollection();
        collection.AddSwiftly(Core);

        collection
            .AddOptionsWithValidateOnStart<HanSwiftBootsS2CFG>()
            .BindConfiguration("SwiftBootsS2CFG");

        collection.AddSingleton<HanSwiftBootsS2Globals>();
        collection.AddSingleton<HanSwiftBootsS2Service>();

        ServiceProvider = collection.BuildServiceProvider();

        _Globals = ServiceProvider.GetRequiredService<HanSwiftBootsS2Globals>();
        _Service = ServiceProvider.GetRequiredService<HanSwiftBootsS2Service>();

        var monitor = ServiceProvider.GetRequiredService<IOptionsMonitor<HanSwiftBootsS2CFG>>();

        _SwiftBootsCFG = monitor.CurrentValue;

        monitor.OnChange(newConfig =>
        {
            _SwiftBootsCFG = newConfig;
            Core.Logger.LogInformation("[飞鞋] SwiftBootsCFG 配置文件已热重载并同步。");
        });

        _Service.HookEvent();
    }
    public override void Unload()
    {
        ServiceProvider!.Dispose();
    }


}