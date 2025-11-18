
<div align="center"><h1><img width="600" height="131" alt="68747470733a2f2f70616e2e73616d7979632e6465762f732f56596d4d5845" src="https://github.com/user-attachments/assets/d0316faa-c2d0-478f-a642-1e3c3651f1d4" /></h1></div>

<div class="section">
<div align="center"><h1>SwiftBoots for Sw2 / 飞鞋 for Sw2</h1></div>
<div align="center"><strong>描述:</strong> 飞鞋，让玩家跳得更高！支持 SteamID 个性化配置和队伍区分的跳跃力。</p></div>
<div align="center"><strong>SwiftBoots allows players to jump higher! Supports SteamID-specific configuration and team-based jump boost.</p></div>
</div>

<div class="section">
<h2>功能特点/Features</h2>
<ul>
    <li>支持 <strong>玩家 SteamID 个性化跳跃高度</strong></li>
    <li>支持 <strong>队伍统一配置</strong>（CT / T）</li>
    <li>支持 <strong>持续 Tick 设置</strong>，可控制跳跃持续时间</li>
    <li>SteamID 配置优先，如果SteamId限制不匹配则使用队伍配置</li>
    <li>Supports <strong>SteamID-specific jump height</strong></li>
    <li>Supports <strong>Team-based configuration</strong> (CT / T)</li>
    <li>Supports <strong>Tick duration</strong> control for jump boost</li>
    <li>SteamID configuration has priority; if SteamID restriction does not match, team configuration is used</li>
</ul>
</div>

<div class="section">
<h2>配置文件示例/Configuration Example</h2>
<pre><code>{
  "Players": {
    "76561198135531234": {        //64位SteamId
      "Enabled": true,           //是否开启 若不开启则 应用队伍配置
      "Team": 0,                // 0 = 不限制队伍，1 = CT，2 = T 可限制队伍 若队伍关闭 应用队伍配置
      "JumpBoostHeight": 1200,  // 跳跃高度 CS2 默认跳跃高度为250
      "DurationTicks": 500      // 持续 Tick 跳跃浮空的持续tick 8为默认时间
    },
    "76561198135531235": {
      "Enabled": false,
      "Team": 0,
      "JumpBoostHeight": 1000,
      "DurationTicks": 200
    }
  },
  "Teams": {  //队伍统一配置 
    "CT": {
      "Enabled": true, //是否开启 
      "JumpBoostHeight": 500, //跳跃高度 cs2 默认高度为 250
      "DurationTicks": 8  //默认持续时间 tick 
    },
    "T": {
      "Enabled": false,
      "JumpBoostHeight": 500,
      "DurationTicks": 8
    }
  }
}</code></pre>
</div>
<div class="section">
<h2>使用说明/Usage</h2>
<ul>
    <li>SteamID 配置优先 / SteamID configuration has priority:
        <ul>
            <li>Enabled=true 且队伍匹配 → 应用玩家专属跳跃力</li>
            <li>Enabled=false 或队伍不匹配 → 应用队伍配置</li>
            <li>Enabled=true and team matches → apply player-specific jump boost</li>
            <li>Enabled=false or team mismatch → use team configuration</li>
        </ul>
    </li>
    <li>队伍配置 / Team configuration:
        <ul>
            <li>Enabled=true 表示队伍玩家可使用跳跃力</li>
            <li>JumpBoostHeight 控制跳跃高度</li>
            <li>DurationTicks 控制跳跃持续时间（Tick 为单位，1 Tick ≈ 0.015 秒）</li>
            <li>Enabled=true → players in the team can use jump boost</li>
            <li>JumpBoostHeight controls the jump height</li>
            <li>DurationTicks controls the duration in ticks (1 Tick ≈ 0.015 seconds)</li>
        </ul>
    </li>
    <li>默认跳跃力：如果 SteamID 与队伍都未开启，玩家使用普通跳跃</li>
    <li>Default jump: if neither SteamID nor team is enabled, players use normal jump</li>
</ul>
</div>

<div class="section">
<h2>示例场景/Example Scenarios</h2>
<ul>
    <li>玩家 A SteamID 配置开启且 Team=0 → 无论哪队，抬升高度 1200</li>
    <li>玩家 B SteamID 配置开启但 Team=2，玩家为 CT → SteamID 配置不生效，回退到 CT 队伍配置</li>
    <li>玩家 C SteamID 未配置 → 使用队伍默认配置</li>
    <li>Player A SteamID enabled and Team=0 → jump height 1200 for all teams</li>
    <li>Player B SteamID enabled but Team=2, player is CT → SteamID configuration ignored, falls back to CT team configuration</li>
    <li>Player C SteamID not configured → uses default team configuration</li>
</ul>
</div>

<div class="section">
<h2>热重载支持/Hot Reload</h2>
<p>修改 <code>HanSwiftBootsS2CFG.jsonc</code> 后，无需重启服务器，插件会自动同步配置。</p>
<p>Modify <code>HanSwiftBootsS2CFG.jsonc</code> and the plugin automatically updates without server restart.</p>
</div>

</body>
</html>
