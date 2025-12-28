# 🏭 ZeroLossCoke

**Remove coke loss from bituminous coal conversion in Vintage Story.**

ZeroLossCoke is a lightweight, server-side Vintage Story mod that removes the vanilla 50% loss when converting bituminous coal into coke.  
It uses safe Harmony Postfix patches to ensure predictable, configurable, and stable production results.

## ✨ Features

- **100% Efficient Production**  
  Eliminates the vanilla 50% coke loss.

- **Fully Configurable Output**  
  Control yield using multipliers and optional min/max limits.

- **Server-Safe Patching**  
  Uses only Harmony Postfix patches (no Transpilers, no IL rewriting).

- **Optional Debug Logging**  
  Detailed server-side logs for administrators.

- **Lightweight & Performance Friendly**  
  No additional ticks, loops, or allocations.

## 📥 Installation

1. Download the latest release from:  
   https://github.com/HarukaYamamoto0/zero-loss-coke/releases
2. Extract the `.zip` into your Vintage Story `Mods` folder
3. Start the game or server
4. (Optional) Adjust settings in `zerolosscoke.json`

> The mod is **server-side only**. Clients do not need to install it.

## ⚙️ Configuration

On first run, the mod generates a `zerolosscoke.json` file:

```json
{
  "YieldMultiplier": 2.0,
  "MinYield": 0,
  "MaxYield": 0,
  "DebugLogging": true
}
````

### Configuration Options

| Setting             | Type    | Default | Description                                                   |
| ------------------- | ------- | ------- | ------------------------------------------------------------- |
| **YieldMultiplier** | `float` | `2.0`   | Final production multiplier (2.0 = full yield, 1.0 = vanilla) |
| **MinYield**        | `int`   | `0`     | Guaranteed minimum output (0 disables the limit)              |
| **MaxYield**        | `int`   | `0`     | Maximum allowed output (0 = unlimited)                        |
| **DebugLogging**    | `bool`  | `true`  | Enables detailed server-side logs                             |

### Configuration Examples

**Vanilla-equivalent (no loss, full yield):**

```json
{
  "YieldMultiplier": 2.0
}
```

**Balanced production (75% efficiency):**

```json
{
  "YieldMultiplier": 1.5,
  "MinYield": 4
}
```

**Guaranteed output with limits:**

```json
{
  "YieldMultiplier": 2.0,
  "MinYield": 12,
  "MaxYield": 32
}
```

## 🎮 How It Works In-Game

1. Build a valid **coke oven** using refractory blocks and a door
2. Place **bituminous coal piles** inside
3. Ignite the oven and wait for the burn cycle to finish
4. Receive the configured coke output with no vanilla loss

**Example:**
16 coal → 16 coke (instead of 8)

## 📊 Debug Logging

When `DebugLogging` is enabled, the server log will include entries like:

```
[ZeroLossCoke] Adjusted at 511991, 4, 512000: 8 -> 16
[ZeroLossCoke] Adjusted at 511991, 4, 511998: 5 -> 10
```

## 🛠️ Technical Notes (For Developers)

* Patch target:
  `BlockEntityCoalPile.OnBurningTickServer()`
* Patch type:
  Harmony **Postfix**
* Behavior:
  Adjusts final output after vanilla logic completes
* No Transpilers
* No IL manipulation
* No reflection-based hooks

### Dependencies

* **Vintage Story:** 1.20.x – 1.21.x
* **HarmonyLib:** bundled with the mod
* **.NET:** multi-targeted (net7.0 / net8.0)

## 🧪 Building From Source

```bash
git clone https://github.com/HarukaYamamoto0/zero-loss-coke.git
cd zero-loss-coke

dotnet restore
dotnet build --configuration Release
```

Build artifacts are generated per target framework.

## 🔍 Troubleshooting

| Issue                  | Solution                                             |
| ---------------------- | ---------------------------------------------------- |
| Mod does not load      | Ensure the correct Vintage Story version is used     |
| Config changes ignored | Restart the server after editing `zerolosscoke.json` |
| No logs visible        | Enable `DebugLogging`                                |
| Unexpected output      | Verify multiplier and min/max settings               |

## 📄 License

Licensed under the **MIT License**.
See [LICENSE](LICENSE) for details.

## 🐛 Bug Reports & Support

If you find a bug:

1. Check existing issues first
2. Include relevant server logs
3. Provide reproduction steps
4. Specify Vintage Story and mod versions

*“Because losing half your coke was never good design.”* 🏭🔥
