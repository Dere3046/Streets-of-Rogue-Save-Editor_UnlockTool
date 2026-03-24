# Rogue_Save_Editor_UnlockTool

为游戏《地痞街区》(Streets of Rogue) 制作的存档修改工具，用于读取和修改 `GameUnlocks.dat` 文件。

[English Version](./README.md)

## 功能
- 将二进制存档文件导出为可读写的 JSON 格式
- 将修改后的 JSON 重新打包为游戏可识别的二进制存档
- 从零创建新的存档文件
- 一键解锁所有物品/角色/特性
- 修改游戏统计数据（nuggets、胜利次数、死亡次数等）

## 要求
- .NET 8.0 运行环境
- 游戏 DLL 文件（`Assembly-CSharp.dll`、`Assembly-CSharp-firstpass.dll`）必须位于 `win_x64/lib` 文件夹中

## 编译
```bash
cd win_x64
dotnet build -c Release
```

## 使用方法

### 命令列表

```bash
# 显示帮助
UnlockToolConsole.exe

# 加载存档并导出为 JSON
UnlockToolConsole.exe load GameUnlocks.dat > output.json

# 从 JSON 导入创建新存档
UnlockToolConsole.exe import input.json GameUnlocks_new.dat

# 导出存档到 JSON 文件
UnlockToolConsole.exe json GameUnlocks.dat output.json

# 创建新的空存档
UnlockToolConsole.exe create new.dat

# 解锁指定内容
UnlockToolConsole.exe unlock GameUnlocks.dat RobotPlayer Agent

# 解锁所有内容
UnlockToolConsole.exe unlockall GameUnlocks.dat

# 锁定指定内容
UnlockToolConsole.exe lock GameUnlocks.dat RobotPlayer Agent

# 修改统计数据
UnlockToolConsole.exe stats GameUnlocks.dat nuggets 99999
UnlockToolConsole.exe stats GameUnlocks.dat wins 100

# 列出解锁项（可按类型筛选）
UnlockToolConsole.exe list GameUnlocks.dat
UnlockToolConsole.exe list GameUnlocks.dat Agent
```

### 命令别名
| 完整命令 | 别名 |
|---------|------|
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

### 解锁类型
- `Agent` - 角色（Cop, Guard, Thief, RobotPlayer 等）
- `Trait` - 特性（Strong, Fast, Tough 等）
- `Item` - 物品（Pistol, Shotgun, Medkit 等）
- `Floor` - 楼层（Floor2, Floor3, Floor4, Floor5）
- `Challenge` - 挑战（Sandbox, InfiniteAmmo 等）
- `Extra` - 额外内容/成就
- `BigQuest` - 大型任务
- `Ability` - 能力
- `Loadout` - 装备配置

### 使用示例

```bash
# 导出存档到 JSON，编辑后导入
UnlockToolConsole.exe json GameUnlocks.dat save.json
# ... 编辑 save.json ...
UnlockToolConsole.exe import save.json GameUnlocks_modified.dat

# 创建全解锁存档
UnlockToolConsole.exe create full.dat
UnlockToolConsole.exe unlockall full.dat

# 修改现有存档的nuggets数量为 99999
UnlockToolConsole.exe stats GameUnlocks.dat nuggets 99999

# 解锁特定角色
UnlockToolConsole.exe unlock GameUnlocks.dat Vampire Agent

# 查看所有已解锁的角色
UnlockToolConsole.exe list GameUnlocks.dat Agent
```

## 项目结构
```
Rogue_Save_Editor_UnlockTool/
├── win_x64/                        # 源代码目录
│   ├── Program.cs                  # 主程序入口
│   ├── GameUnlocksUtility.cs       # 存档工具类（加载/保存/转换）
│   ├── SaveDataDto.cs              # JSON 序列化 DTO 类
│   ├── UnlockToolConsole.csproj    # 项目文件
│   ├── lib/                        # 游戏 DLL（编译必需）
│   │   ├── Assembly-CSharp.dll
│   │   └── Assembly-CSharp-firstpass.dll
│   └── data/                       # 示例存档
│       └── GameUnlocks.dat
├── README.md                       # 英文文档
└── README_zh.md                    # 本文档
```

## 免责申明
本工具仅供学习研究使用。修改游戏存档可能导致存档损坏或游戏异常，请务必提前备份原始文件。作者不对任何数据丢失或游戏问题负责。
