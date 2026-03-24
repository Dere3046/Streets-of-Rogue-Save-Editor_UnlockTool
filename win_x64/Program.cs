using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RogueSaveEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }

            string command = args[0].ToLowerInvariant();

            try
            {
                switch (command)
                {
                    case "load":
                    case "l":
                    case "read":
                        if (args.Length < 2)
                        {
                            Console.Error.WriteLine("Error: Please specify save file path");
                            PrintUsage();
                            return;
                        }
                        LoadAndPrint(args[1]);
                        break;

                    case "save":
                    case "s":
                    case "write":
                        if (args.Length < 2)
                        {
                            Console.Error.WriteLine("Error: Please specify output file path");
                            PrintUsage();
                            return;
                        }
                        SaveFromJson(args[1], args.Length > 2 ? args[2] : null);
                        break;

                    case "unlock":
                    case "u":
                        if (args.Length < 4)
                        {
                            Console.Error.WriteLine("Error: Please specify save file, unlock name, and type");
                            Console.Error.WriteLine("Usage: unlock <file> <name> <type>");
                            return;
                        }
                        UnlockSpecific(args[1], args[2], args[3]);
                        break;

                    case "unlockall":
                    case "ua":
                    case "all":
                        if (args.Length < 2)
                        {
                            Console.Error.WriteLine("Error: Please specify save file path");
                            return;
                        }
                        UnlockAll(args[1]);
                        break;

                    case "lock":
                    case "lk":
                        if (args.Length < 4)
                        {
                            Console.Error.WriteLine("Error: Please specify save file, unlock name, and type");
                            return;
                        }
                        LockSpecific(args[1], args[2], args[3]);
                        break;

                    case "json":
                    case "j":
                    case "export":
                        if (args.Length < 3)
                        {
                            Console.Error.WriteLine("Error: Please specify input file and output JSON path");
                            return;
                        }
                        ExportToJson(args[1], args[2]);
                        break;

                    case "import":
                    case "i":
                        if (args.Length < 3)
                        {
                            Console.Error.WriteLine("Error: Please specify JSON file and output path");
                            return;
                        }
                        ImportFromJson(args[1], args[2]);
                        break;

                    case "create":
                    case "new":
                    case "c":
                        if (args.Length < 2)
                        {
                            Console.Error.WriteLine("Error: Please specify output file path");
                            return;
                        }
                        CreateNewSave(args[1]);
                        break;

                    case "stats":
                        if (args.Length < 2)
                        {
                            Console.Error.WriteLine("Error: Please specify save file path");
                            return;
                        }
                        ModifyStats(args);
                        break;

                    case "list":
                        if (args.Length < 2)
                        {
                            Console.Error.WriteLine("Error: Please specify save file path");
                            return;
                        }
                        ListUnlocks(args[1], args.Length > 2 ? args[2] : null);
                        break;

                    default:
                        if (File.Exists(command))
                        {
                            LoadAndPrint(command);
                        }
                        else if (args.Length == 2 && File.Exists(command))
                        {
                            SaveFromJson(command, args[1]);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unknown command: {command}");
                            PrintUsage();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine($"Details: {ex.InnerException.Message}");
                }
                Environment.Exit(1);
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("  Streets of Rogue Save Editor Tool");
            Console.WriteLine("  GameUnlocks.dat Parser and Generator");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("Usage: UnlockToolConsole <command> [args]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  load, l <file>                       - Load and display save (outputs JSON)");
            Console.WriteLine("  save, s <output> [json]              - Create save from JSON (stdin or file)");
            Console.WriteLine("  unlock, u <file> <name> <type>       - Unlock specific item");
            Console.WriteLine("  unlockall, ua <file>                 - Unlock everything");
            Console.WriteLine("  lock, lk <file> <name> <type>        - Lock specific item");
            Console.WriteLine("  json, j <file> <output>              - Export to JSON file");
            Console.WriteLine("  import, i <json> <output>            - Import from JSON to save");
            Console.WriteLine("  create, c <output>                   - Create new empty save");
            Console.WriteLine("  stats <file> <stat> <value>          - Modify statistics");
            Console.WriteLine("  list <file> [type]                   - List unlocks");
            Console.WriteLine();
            Console.WriteLine("Unlock Types:");
            Console.WriteLine("  Agent       - Characters (Cop, Guard, Thief, RobotPlayer, etc.)");
            Console.WriteLine("  Trait       - Traits");
            Console.WriteLine("  Item        - Items");
            Console.WriteLine("  Floor       - Floors (Floor2, Floor3, Floor4, Floor5)");
            Console.WriteLine("  Challenge   - Challenges");
            Console.WriteLine("  Extra       - Extra unlocks (achievements)");
            Console.WriteLine("  BigQuest    - Big Quests");
            Console.WriteLine("  Ability     - Abilities");
            Console.WriteLine("  Loadout     - Loadouts");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  UnlockToolConsole l \"C:\\GameUnlocks.dat\" > output.json");
            Console.WriteLine("  UnlockToolConsole u \"C:\\GameUnlocks.dat\" RobotPlayer Agent");
            Console.WriteLine("  UnlockToolConsole ua \"C:\\GameUnlocks.dat\"");
            Console.WriteLine("  UnlockToolConsole j \"C:\\GameUnlocks.dat\" \"C:\\output.json\"");
            Console.WriteLine("  UnlockToolConsole i \"C:\\input.json\" \"C:\\GameUnlocks.dat\"");
            Console.WriteLine("  UnlockToolConsole stats \"C:\\GameUnlocks.dat\" nuggets 99999");
            Console.WriteLine("  UnlockToolConsole c \"C:\\new.dat\"");
        }

        static void LoadAndPrint(string filePath)
        {
            Console.Error.WriteLine($"Loading save: {filePath}");
            var data = GameUnlocksUtility.Load(filePath);

            Console.Error.WriteLine($"[Info] Unlock count: {data.unlocks?.Count ?? 0}");
            Console.Error.WriteLine($"[Info] Nuggets: {data.nuggets}");
            Console.Error.WriteLine($"[Info] HighScore count: {data.highScores?.Count ?? 0}");
            Console.Error.WriteLine($"[Info] Game version: {data.currentVersion ?? "Unknown"}");

            var dto = GameUnlocksUtility.ToDto(data);
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(dto, jsonSettings);
            Console.Out.Write(json);
        }

        static void SaveFromJson(string outputFile, string inputFile)
        {
            string jsonStr;
            if (!string.IsNullOrEmpty(inputFile))
            {
                jsonStr = File.ReadAllText(inputFile);
                Console.Error.WriteLine($"[Info] Reading JSON from file: {inputFile}");
            }
            else
            {
                jsonStr = Console.In.ReadToEnd();
                Console.Error.WriteLine($"[Info] Reading JSON from stdin");
            }

            var jsonSettings = new JsonSerializerSettings();
            var dto = JsonConvert.DeserializeObject<UnlockSaveDataDto>(jsonStr, jsonSettings);
            if (dto == null)
            {
                Console.Error.WriteLine("[Error] JSON deserialization returned null");
                Environment.Exit(1);
            }

            var data = GameUnlocksUtility.FromDto(dto);
            Console.Error.WriteLine($"[Info] Unlock count: {data.unlocks?.Count ?? 0}");
            Console.Error.WriteLine($"[Info] Nuggets: {data.nuggets}");

            GameUnlocksUtility.Save(outputFile, data);
        }

        static void UnlockSpecific(string filePath, string unlockName, string unlockType)
        {
            var data = GameUnlocksUtility.Load(filePath);
            GameUnlocksUtility.UnlockItem(data, unlockName, unlockType);
            Console.Error.WriteLine($"Unlocked: {unlockName} ({unlockType})");
            GameUnlocksUtility.Save(filePath, data);
        }

        static void UnlockAll(string filePath)
        {
            var data = GameUnlocksUtility.Load(filePath);
            GameUnlocksUtility.UnlockEverything(data);
            Console.Error.WriteLine("All unlocks completed!");
            GameUnlocksUtility.Save(filePath, data);
        }

        static void LockSpecific(string filePath, string unlockName, string unlockType)
        {
            var data = GameUnlocksUtility.Load(filePath);
            GameUnlocksUtility.LockItem(data, unlockName, unlockType);
            Console.Error.WriteLine($"Locked: {unlockName} ({unlockType})");
            GameUnlocksUtility.Save(filePath, data);
        }

        static void ExportToJson(string filePath, string outputPath)
        {
            var data = GameUnlocksUtility.Load(filePath);
            var dto = GameUnlocksUtility.ToDto(data);

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(dto, jsonSettings);
            File.WriteAllText(outputPath, json, System.Text.Encoding.UTF8);
            Console.Error.WriteLine($"Exported to JSON: {outputPath}");
        }

        static void ImportFromJson(string jsonPath, string outputPath)
        {
            var json = File.ReadAllText(jsonPath, System.Text.Encoding.UTF8);

            Console.Error.WriteLine($"[Info] Reading JSON file: {jsonPath} ({json.Length} bytes)");

            var jsonSettings = new JsonSerializerSettings();
            var dto = JsonConvert.DeserializeObject<UnlockSaveDataDto>(json, jsonSettings);
            if (dto == null)
            {
                Console.Error.WriteLine("[Error] JSON deserialization returned null");
                Environment.Exit(1);
            }

            var data = GameUnlocksUtility.FromDto(dto);
            Console.Error.WriteLine($"[Info] Importing {data.unlocks?.Count ?? 0} unlocks");
            Console.Error.WriteLine($"[Info] Nuggets: {data.nuggets}");

            GameUnlocksUtility.Save(outputPath, data, false);
        }

        static void CreateNewSave(string filePath)
        {
            var data = GameUnlocksUtility.CreateNewSaveData();
            GameUnlocksUtility.Save(filePath, data, false);
            Console.Error.WriteLine("New save file created successfully!");
        }

        static void ModifyStats(string[] args)
        {
            var filePath = args[1];
            var data = GameUnlocksUtility.Load(filePath);

            if (args.Length >= 4)
            {
                var statName = args[2].ToLowerInvariant();
                var statValue = args[3];

                switch (statName)
                {
                    case "deaths":
                    case "totaldeaths":
                        data.totalDeaths = int.Parse(statValue);
                        Console.Error.WriteLine($"Set totalDeaths: {statValue}");
                        break;
                    case "wins":
                    case "totalwins":
                        data.totalWins = int.Parse(statValue);
                        Console.Error.WriteLine($"Set totalWins: {statValue}");
                        break;
                    case "gamesplayed":
                    case "totalgamesplayed":
                        data.totalGamesPlayed = int.Parse(statValue);
                        Console.Error.WriteLine($"Set totalGamesPlayed: {statValue}");
                        break;
                    case "nuggets":
                        data.nuggets = int.Parse(statValue);
                        Console.Error.WriteLine($"Set nuggets: {statValue}");
                        break;
                    case "finishedtutorial":
                        data.finishedTutorial = bool.Parse(statValue);
                        Console.Error.WriteLine($"Set finishedTutorial: {statValue}");
                        break;
                    default:
                        Console.Error.WriteLine($"Unknown stat: {statName}");
                        Console.Error.WriteLine("Available stats: deaths, wins, gamesPlayed, nuggets, finishedTutorial");
                        return;
                }

                GameUnlocksUtility.Save(filePath, data);
            }
            else
            {
                Console.Error.WriteLine("Usage: stats <file> <stat> <value>");
                Console.Error.WriteLine("Available stats: deaths, wins, gamesPlayed, nuggets, finishedTutorial");
            }
        }

        static void ListUnlocks(string filePath, string filterType)
        {
            var data = GameUnlocksUtility.Load(filePath);
            GameUnlocksUtility.ListUnlocks(data, filterType);
        }
    }
}
