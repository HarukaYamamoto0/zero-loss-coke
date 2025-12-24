# 🏭 ZeroLossCoke

**Eliminate coke loss in high-quality fuel production!**

An elegant Vintage Story mod that removes the 50% loss in converting bituminous coal to coke, ensuring maximum
production through safe Harmony patches.

## ✨ Features

- **✅ 100% Efficient Production**: Converts all bituminous coal to coke without losses
- **🎚️ Fully Configurable**: Adjust production with multipliers and limits
- **🛡️ Safe and Stable**: Uses only Postfix patches (no risky Transpiler)
- **📊 Detailed Logs**: Optional monitoring of all conversions
- **⚡ Lightweight and Efficient**: Zero impact on server performance

## 📥 Installation

1. Download the latest version from the [releases page](https://github.com/HarukaYamamoto0/zero-loss-coke/releases)
2. Extract the `.zip` file to your Vintage Story `Mods` folder
3. Start the game and enable the mod in the mod manager
4. (Optional) Adjust settings in `zerolosscoke.json`

## ⚙️ Configuration

The mod automatically creates a `zerolosscoke.json` file on first run:

```json
{
  "YieldMultiplier": 2.0,
  "MinYield": 0,
  "MaxYield": 0,
  "DebugLogging": true
}
```

### Configuration Options

| Setting             | Type    | Default | Description                                        |
|---------------------|---------|---------|----------------------------------------------------|
| **YieldMultiplier** | `float` | `2.0`   | Production multiplier (2.0 = double, 1.0 = normal) |
| **MinYield**        | `int`   | `0`     | Minimum guaranteed amount (0 = disabled)           |
| **MaxYield**        | `int`   | `0`     | Maximum allowed amount (0 = unlimited)             |
| **DebugLogging**    | `bool`  | `false`  | Enables detailed console logs                      |

### Configuration Examples

**Default production (no losses)**

```json
{
  "YieldMultiplier": 2.0,
  "DebugLogging": true
}
```

**Balanced production (75% efficiency)**

```json
{
  "YieldMultiplier": 1.5,
  "MinYield": 4,
  "DebugLogging": false
}
```

**Guaranteed production (never less than 12)**

```json
{
  "YieldMultiplier": 2.0,
  "MinYield": 12,
  "MaxYield": 32
}
```

## 🎮 How It Works In-Game

### Production Process

1. Build a valid **coke oven** with refractory walls and a door
2. Place **bituminous coal piles** inside the oven
3. Ignite and wait ~12 in-game hours
4. **Result**: All coal is converted to coke (no. 50% loss)

### Example Logs

```
[ZeroLossCoke] Adjusted at 511991, 4, 512000: 8 -> 16
[ZeroLossCoke] Adjusted at 511991, 4, 511998: 5 -> 10
```

## 🛠️ For Developers

### Technical Architecture

```csharp
// Main patch - Adjusts production after conversion
[HarmonyPostfix]
[HarmonyPatch(typeof(BlockEntityCoalPile), "OnBurningTickServer")]
private static void AdjustCokeYieldPostfix(BlockEntityCoalPile __instance)
{
    // Intercepts after the game's conversion
    // Applies configurable multiplier
    // Ensures min/max limits
}
```

### Dependencies

- **Vintage Story 1.21.6+**
- **HarmonyLib** (included in package)
- **.NET 8.0**

### Compilation

```bash
# Clone the repository
git clone https://github.com/HarukaYamamoto0/ZeroLossCoke.git

# Restore NuGet packages
dotnet restore

# Compile in Debug mode
dotnet build --configuration Debug

# Or compile for release
dotnet build --configuration Release
```

## 🔍 Troubleshooting

| Problem                   | Solution                                       |
|---------------------------|------------------------------------------------|
| Mod doesn't load          | Verify HarmonyLib is in the Mods folder        |
| Configuration not applied | Restart server after editing zerolosscoke.json |
| Logs not appearing        | Enable `DebugLogging: true` in configuration   |
| Incorrect production      | Check `YieldMultiplier` in config file         |

## 📄 License

This project is licensed under the **MIT License** – see the [LICENSE](LICENSE) file for details.

## 🐛 Reporting Bugs

Found an issue? Please:

1. Check if an [issue](https://github.com/HarukaYamamoto0/zero-loss-coke/issues) already exists
2. Include relevant server logs
3. Describe steps to reproduce the bug
4. Report your Vintage Story and mod versions

**Compatible with multiplayer and singleplayer servers** • **Automatic configuration updates** • **Seamless integration
with base game**

*"Because every piece of coke counts!"* 🏭🔥
