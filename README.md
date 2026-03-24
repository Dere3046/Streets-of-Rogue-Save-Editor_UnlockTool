# Rogue_Save_Editor_UnlockTool

A save editor for the game **Streets of Rogue** to read and modify `GameUnlocks.dat` files.

[中文版](./README_zh.md)

## Features
- Export binary save file to human-readable JSON format
- Repack modified JSON back to game-recognized binary format
- Create new save files from scratch
- Unlock all items/agents/traits with a single command
- Modify in-game statistics (nuggets, wins, deaths, etc.)

## Requirements
- .NET 8.0 Runtime
- Game DLL files (`Assembly-CSharp.dll`, `Assembly-CSharp-firstpass.dll`) must be present in the `win_x64/lib` folder

## Build
```bash
cd win_x64
dotnet build -c Release
```

## Usage

### Commands

```bash
# Show help
UnlockToolConsole.exe

# Load save and export to JSON
UnlockToolConsole.exe load GameUnlocks.dat > output.json

# Import JSON to create new save
UnlockToolConsole.exe import input.json GameUnlocks_new.dat

# Export save to JSON file
UnlockToolConsole.exe json GameUnlocks.dat output.json

# Create new empty save
UnlockToolConsole.exe create new.dat

# Unlock specific item
UnlockToolConsole.exe unlock GameUnlocks.dat RobotPlayer Agent

# Unlock everything
UnlockToolConsole.exe unlockall GameUnlocks.dat

# Lock specific item
UnlockToolConsole.exe lock GameUnlocks.dat RobotPlayer Agent

# Modify statistics
UnlockToolConsole.exe stats GameUnlocks.dat nuggets 99999
UnlockToolConsole.exe stats GameUnlocks.dat wins 100

# List unlocks (optionally filter by type)
UnlockToolConsole.exe list GameUnlocks.dat
UnlockToolConsole.exe list GameUnlocks.dat Agent
```

### Command Aliases
| Full Command | Alias |
|-------------|-------|
| `load` | `l`, `read` |
| `save` | `s`, `write` |
| `unlock` | `u` |
| `unlockall` | `ua`, `all` |
| `lock` | `lk` |
| `json` | `j`, `export` |
| `import` | `i` |
| `create` | `c`, `new` |
| `list` | - |
| `stats` | - |

### Unlock Types
- `Agent` - Characters (Cop, Guard, Thief, RobotPlayer, etc.)
- `Trait` - Traits (Strong, Fast, Tough, etc.)
- `Item` - Items (Pistol, Shotgun, Medkit, etc.)
- `Floor` - Floors (Floor2, Floor3, Floor4, Floor5)
- `Challenge` - Challenges (Sandbox, InfiniteAmmo, etc.)
- `Extra` - Extra unlocks/achievements
- `BigQuest` - Big Quests
- `Ability` - Abilities
- `Loadout` - Loadouts

### Examples

```bash
# Export save to JSON, edit, then import back
UnlockToolConsole.exe json GameUnlocks.dat save.json
# ... edit save.json ...
UnlockToolConsole.exe import save.json GameUnlocks_modified.dat

# Create a new save with all unlocks
UnlockToolConsole.exe create full.dat
UnlockToolConsole.exe unlockall full.dat

# Modify existing save to have 99999 nuggets
UnlockToolConsole.exe stats GameUnlocks.dat nuggets 99999

# Unlock a specific character
UnlockToolConsole.exe unlock GameUnlocks.dat Vampire Agent

# View all unlocked agents
UnlockToolConsole.exe list GameUnlocks.dat Agent
```

## Project Structure
```
Rogue_Save_Editor_UnlockTool/
├── win_x64/                        # Source code
│   ├── Program.cs                  # Main entry point
│   ├── GameUnlocksUtility.cs       # Save file utilities
│   ├── SaveDataDto.cs              # DTO classes for JSON
│   ├── UnlockToolConsole.csproj    # Project file
│   ├── lib/                        # Game DLLs (required for build)
│   │   ├── Assembly-CSharp.dll
│   │   └── Assembly-CSharp-firstpass.dll
│   └── data/                       # Sample save file
│       └── GameUnlocks.dat
├── README.md                       # This file
└── README_zh.md                    # Chinese documentation
```

## Disclaimer
This tool is provided for educational and research purposes only. Modifying game save files may lead to corruption or unexpected behavior. Always back up your original files before use. The author assumes no responsibility for any data loss or game issues.
