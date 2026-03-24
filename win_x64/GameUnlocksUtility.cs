using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace RogueSaveEditor
{
    /// <summary>
    /// Streets of Rogue GameUnlocks.dat Tool
    /// Uses original game classes from Assembly-CSharp.dll
    /// </summary>
    public static class GameUnlocksUtility
    {
        /// <summary>
        /// Load GameUnlocks.dat from file
        /// </summary>
        public static UnlockSaveData Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var formatter = new BinaryFormatter();
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var data = formatter.Deserialize(fs) as UnlockSaveData;
                if (data == null)
                {
                    throw new InvalidDataException("Failed to deserialize save data");
                }
                return data;
            }
        }

        /// <summary>
        /// Save GameUnlocks.dat to file
        /// </summary>
        public static void Save(string filePath, UnlockSaveData data, bool createBackup = true)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (createBackup && File.Exists(filePath))
            {
                var backupPath = filePath + ".backup";
                File.Copy(filePath, backupPath, overwrite: true);
                Console.Error.WriteLine($"Backup created: {backupPath}");
            }

            var formatter = new BinaryFormatter();
            using (var fs = File.Create(filePath))
            {
                formatter.Serialize(fs, data);
            }
            Console.Error.WriteLine($"Save written: {filePath}");
        }

        /// <summary>
        /// Create new empty save data
        /// </summary>
        public static UnlockSaveData CreateNewSaveData()
        {
            var data = new UnlockSaveData
            {
                unlocks = new List<Unlock>(),
                highScores = new List<HighScore>(),
                customCharacterSlots = new List<string>(),
                storedItem = null,
                storedItem2 = null,
                storedItem3 = null,
                storedItem4 = null,
                storedItem5 = null,
                totalDeaths = 0,
                totalWins = 0,
                totalGamesPlayed = 0,
                nuggets = 0,
                lastDailyRun = null,
                finishedTutorial = false,
                viewedReadThis = false,
                currentVersion = "v99i",
                loadoutList = new List<Unlock>[30],
                rewardConfigConsoleList = new List<string>[10],
                traitConfigConsoleList = new List<string>[10],
                mutatorConfigConsoleList = new List<string>[10]
            };

            for (int i = 0; i < 16; i++)
            {
                data.customCharacterSlots.Add("");
            }

            for (int i = 0; i < 30; i++)
            {
                data.loadoutList[i] = new List<Unlock>();
            }

            for (int i = 0; i < 10; i++)
            {
                data.rewardConfigConsoleList[i] = new List<string>();
                data.traitConfigConsoleList[i] = new List<string>();
                data.mutatorConfigConsoleList[i] = new List<string>();
            }

            return data;
        }

        /// <summary>
        /// Unlock a specific item
        /// </summary>
        public static void UnlockItem(UnlockSaveData data, string unlockName, string unlockType)
        {
            var unlock = data.unlocks.FirstOrDefault(u => u.unlockName == unlockName && u.unlockType == unlockType);
            if (unlock != null)
            {
                unlock.unlocked = true;
                unlock.progressCount = 0;
            }
            else
            {
                unlock = new Unlock(unlockName, unlockType, true);
                data.unlocks.Add(unlock);
            }
        }

        /// <summary>
        /// Lock a specific item
        /// </summary>
        public static void LockItem(UnlockSaveData data, string unlockName, string unlockType)
        {
            var unlock = data.unlocks.FirstOrDefault(u => u.unlockName == unlockName && u.unlockType == unlockType);
            if (unlock != null)
            {
                unlock.unlocked = false;
            }
        }

        /// <summary>
        /// Unlock all agents
        /// </summary>
        public static void UnlockAllAgents(UnlockSaveData data)
        {
            var agents = new[] {
                "Cop", "Guard", "Slave", "Thief", "Addict", "Gangbanger", "Gorilla",
                "Scientist", "ShapeShifter", "Vampire", "Werewolf", "Wrestler",
                "Comedian", "Businessman", "Shopkeeper", "Assassin", "Athlete",
                "Bartender", "Cannibal", "Slavemaster", "Firefighter", "Mafia",
                "RobotPlayer", "Zombie", "Doctor", "Engineer", "Hacker", "Soldier",
                "Mercenary", "Sniper", "Demolitionist", "MechPilot", "Courier",
                "Bouncer", "Alien", "Guard2", "UpperCruster"
            };
            foreach (var agent in agents)
            {
                UnlockItem(data, agent, "Agent");
            }
        }

        /// <summary>
        /// Unlock all traits
        /// </summary>
        public static void UnlockAllTraits(UnlockSaveData data)
        {
            var traits = new[] {
                "Strong", "Fast", "Tough", "Smart", "Charismatic", "Lucky",
                "Crazy", "Stealthy", "Gunslinger", "MeleeExpert", "ExplosivesExpert"
            };
            foreach (var trait in traits)
            {
                UnlockItem(data, trait, "Trait");
            }
        }

        /// <summary>
        /// Unlock all items
        /// </summary>
        public static void UnlockAllItems(UnlockSaveData data)
        {
            var items = new[] {
                "Pistol", "Shotgun", "Rifle", "SMG", "SniperRifle",
                "Knife", "Sword", "Bat", "Crowbar", "Grenade",
                "Medkit", "Resurrection", "Invisibility", "Teleporter"
            };
            foreach (var item in items)
            {
                UnlockItem(data, item, "Item");
            }
        }

        /// <summary>
        /// Unlock all floors
        /// </summary>
        public static void UnlockAllFloors(UnlockSaveData data)
        {
            var floors = new[] { "Floor2", "Floor3", "Floor4", "Floor5" };
            foreach (var floor in floors)
            {
                UnlockItem(data, floor, "Floor");
            }
        }

        /// <summary>
        /// Unlock all extras
        /// </summary>
        public static void UnlockAllExtras(UnlockSaveData data)
        {
            var extras = new[] {
                "CompleteTutorial", "KilledKillerRobot", "FoundAlien",
                "RefrigeratorRun", "KillVampireAsWerewolf", "FallOffLevelEdge",
                "CreateCustomCharacter", "PutResurrectionInWater", "HaveFourStatusEffects",
                "EnslaveSlavemaster", "WinArenaFight", "KillEveryoneInLevel",
                "AskNPCLeaveLevel", "KillWithGravestone", "GiveCyanideCocktail",
                "ElectrocuteInWater"
            };
            foreach (var extra in extras)
            {
                UnlockItem(data, extra, "Extra");
            }
        }

        /// <summary>
        /// Unlock everything
        /// </summary>
        public static void UnlockEverything(UnlockSaveData data)
        {
            if (data.unlocks == null) data.unlocks = new List<Unlock>();

            foreach (var unlock in data.unlocks)
            {
                unlock.unlocked = true;
                unlock.progressCount = 0;
            }

            UnlockAllAgents(data);
            UnlockAllTraits(data);
            UnlockAllItems(data);
            UnlockAllFloors(data);
            UnlockAllExtras(data);

            data.totalWins = 100;
            data.totalDeaths = 50;
            data.totalGamesPlayed = 200;
            data.nuggets = 99999;
            data.finishedTutorial = true;
        }

        /// <summary>
        /// Print save data summary
        /// </summary>
        public static void PrintSummary(UnlockSaveData data)
        {
            Console.Error.WriteLine("=== Streets of Rogue Save Data ===");
            Console.Error.WriteLine($"Game version: {data.currentVersion ?? "Unknown"}");
            Console.Error.WriteLine($"Deaths: {data.totalDeaths} | Wins: {data.totalWins} | Nuggets: {data.nuggets}");
            Console.Error.WriteLine($"Total unlocks: {data.unlocks?.Count(u => u.unlocked) ?? 0} / {data.unlocks?.Count ?? 0}");
        }

        /// <summary>
        /// List unlocks by type
        /// </summary>
        public static void ListUnlocks(UnlockSaveData data, string filterType = null)
        {
            Console.Error.WriteLine("=== Streets of Rogue Save Data ===");
            Console.Error.WriteLine($"Game version: {data.currentVersion ?? "Unknown"}");
            Console.Error.WriteLine($"Deaths: {data.totalDeaths} | Wins: {data.totalWins} | Nuggets: {data.nuggets}");
            Console.Error.WriteLine();

            var unlocks = data.unlocks ?? new List<Unlock>();
            var query = filterType != null
                ? unlocks.Where(u => u.unlockType == filterType)
                : unlocks;

            foreach (var group in query.GroupBy(u => u.unlockType))
            {
                Console.Error.WriteLine($"--- {group.Key} ---");
                var unlockedCount = group.Count(u => u.unlocked);
                Console.Error.WriteLine($"Unlocked: {unlockedCount} / {group.Count()}");
                foreach (var unlock in group.OrderBy(u => u.unlockName))
                {
                    var status = unlock.unlocked ? "[+]" : "[ ]";
                    var progress = unlock.progressCount > 0 ? $" (progress: {unlock.progressCount})" : "";
                    Console.Error.WriteLine($"  {status} {unlock.unlockName}{progress}");
                }
                Console.Error.WriteLine();
            }
        }

        /// <summary>
        /// Convert game UnlockSaveData to DTO for JSON serialization
        /// </summary>
        public static UnlockSaveDataDto ToDto(UnlockSaveData data)
        {
            if (data == null) return null;

            var dto = new UnlockSaveDataDto
            {
                unlocks = data.unlocks?.Select(ToUnlockDto).ToList(),
                highScores = data.highScores?.Select(ToHighScoreDto).ToList(),
                customCharacterSlots = data.customCharacterSlots,
                storedItem = ToInvItemLightDto(data.storedItem),
                storedItem2 = ToInvItemLightDto(data.storedItem2),
                storedItem3 = ToInvItemLightDto(data.storedItem3),
                storedItem4 = ToInvItemLightDto(data.storedItem4),
                storedItem5 = ToInvItemLightDto(data.storedItem5),
                totalDeaths = data.totalDeaths,
                totalWins = data.totalWins,
                totalGamesPlayed = data.totalGamesPlayed,
                nuggets = data.nuggets,
                lastDailyRun = data.lastDailyRun,
                finishedTutorial = data.finishedTutorial,
                viewedReadThis = data.viewedReadThis,
                currentVersion = data.currentVersion,
                loadoutList = data.loadoutList?.Select(l => l?.Select(ToUnlockDto).ToList()).ToList(),
                rewardConfigConsoleList = data.rewardConfigConsoleList?.Select(l => l?.ToList()).ToList(),
                traitConfigConsoleList = data.traitConfigConsoleList?.Select(l => l?.ToList()).ToList(),
                mutatorConfigConsoleList = data.mutatorConfigConsoleList?.Select(l => l?.ToList()).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Convert DTO to game UnlockSaveData
        /// </summary>
        public static UnlockSaveData FromDto(UnlockSaveDataDto dto)
        {
            if (dto == null) return null;

            var data = new UnlockSaveData
            {
                unlocks = dto.unlocks?.Select(ToUnlock).ToList(),
                highScores = dto.highScores?.Select(ToHighScore).ToList(),
                customCharacterSlots = dto.customCharacterSlots,
                storedItem = ToInvItemLight(dto.storedItem),
                storedItem2 = ToInvItemLight(dto.storedItem2),
                storedItem3 = ToInvItemLight(dto.storedItem3),
                storedItem4 = ToInvItemLight(dto.storedItem4),
                storedItem5 = ToInvItemLight(dto.storedItem5),
                totalDeaths = dto.totalDeaths,
                totalWins = dto.totalWins,
                totalGamesPlayed = dto.totalGamesPlayed,
                nuggets = dto.nuggets,
                lastDailyRun = dto.lastDailyRun,
                finishedTutorial = dto.finishedTutorial,
                viewedReadThis = dto.viewedReadThis,
                currentVersion = dto.currentVersion,
                loadoutList = dto.loadoutList?.Select(l => l?.Select(ToUnlock).ToList()).ToArray() ?? new List<Unlock>[30],
                rewardConfigConsoleList = dto.rewardConfigConsoleList?.Select(l => l?.ToList()).ToArray() ?? new List<string>[10],
                traitConfigConsoleList = dto.traitConfigConsoleList?.Select(l => l?.ToList()).ToArray() ?? new List<string>[10],
                mutatorConfigConsoleList = dto.mutatorConfigConsoleList?.Select(l => l?.ToList()).ToArray() ?? new List<string>[10]
            };

            return data;
        }

        private static UnlockDto ToUnlockDto(Unlock u)
        {
            if (u == null) return null;
            return new UnlockDto
            {
                unlockName = u.unlockName,
                unlockNameType = u.unlockNameType,
                unlockDescriptionType = u.unlockDescriptionType,
                unlockType = u.unlockType,
                unlocked = u.unlocked,
                nowAvailable = u.nowAvailable,
                onlyInCharacterCreation = u.onlyInCharacterCreation,
                removeInMech = u.removeInMech,
                freeItem = u.freeItem,
                notActive = u.notActive,
                unavailable = u.unavailable,
                removal = u.removal,
                replacing = u.replacing,
                isUpgrade = u.isUpgrade,
                cantSwap = u.cantSwap,
                cantLose = u.cantLose,
                dcUnlock = u.dcUnlock,
                progressCount = u.progressCount,
                progressList = u.progressList,
                agents = u.agents,
                categories = u.categories,
                specialAbilities = u.specialAbilities,
                leadingTraits = u.leadingTraits,
                leadingItems = u.leadingItems,
                leadingBigQuests = u.leadingBigQuests,
                prerequisites = u.prerequisites,
                cancellations = u.cancellations,
                recommendations = u.recommendations,
                upgrade = u.upgrade,
                cost = u.cost,
                cost2 = u.cost2,
                cost3 = u.cost3,
                notOnClient = u.notOnClient
            };
        }

        private static Unlock ToUnlock(UnlockDto d)
        {
            if (d == null) return null;
            var u = new Unlock(d.unlockName, d.unlockType, d.unlocked, d.cost, d.cost2, d.cost3);
            u.unlockNameType = d.unlockNameType;
            u.unlockDescriptionType = d.unlockDescriptionType;
            u.nowAvailable = d.nowAvailable;
            u.onlyInCharacterCreation = d.onlyInCharacterCreation;
            u.removeInMech = d.removeInMech;
            u.freeItem = d.freeItem;
            u.notActive = d.notActive;
            u.unavailable = d.unavailable;
            u.removal = d.removal;
            u.replacing = d.replacing;
            u.isUpgrade = d.isUpgrade;
            u.cantSwap = d.cantSwap;
            u.cantLose = d.cantLose;
            u.dcUnlock = d.dcUnlock;
            u.progressCount = d.progressCount;
            u.progressList = d.progressList ?? new List<string>();
            u.agents = d.agents ?? new List<string>();
            u.categories = d.categories ?? new List<string>();
            u.specialAbilities = d.specialAbilities ?? new List<string>();
            u.leadingTraits = d.leadingTraits ?? new List<string>();
            u.leadingItems = d.leadingItems ?? new List<string>();
            u.leadingBigQuests = d.leadingBigQuests ?? new List<string>();
            u.prerequisites = d.prerequisites ?? new List<string>();
            u.cancellations = d.cancellations ?? new List<string>();
            u.recommendations = d.recommendations ?? new List<string>();
            u.upgrade = d.upgrade;
            u.notOnClient = d.notOnClient;
            return u;
        }

        private static HighScoreDto ToHighScoreDto(HighScore h)
        {
            if (h == null) return null;
            return new HighScoreDto
            {
                skillPoints = h.skillPoints,
                skillLevel = h.skillLevel,
                agentName = h.agentName,
                finalFloor = h.finalFloor,
                finalFloorText = h.finalFloorText,
                timeTaken = h.timeTaken,
                timeTakenText = h.timeTakenText,
                deathMethod = h.deathMethod,
                deathKiller = h.deathKiller
            };
        }

        private static HighScore ToHighScore(HighScoreDto d)
        {
            if (d == null) return null;
            return new HighScore
            {
                skillPoints = d.skillPoints,
                skillLevel = d.skillLevel,
                agentName = d.agentName,
                finalFloor = d.finalFloor,
                finalFloorText = d.finalFloorText,
                timeTaken = d.timeTaken,
                timeTakenText = d.timeTakenText,
                deathMethod = d.deathMethod,
                deathKiller = d.deathKiller
            };
        }

        private static InvItemLightDto ToInvItemLightDto(InvItemLight i)
        {
            if (i == null) return null;
            return new InvItemLightDto
            {
                invItemID = i.invItemID,
                itemNetID = i.itemNetID,
                invItemName = i.invItemName,
                invItemRealName = i.invItemRealName,
                invItemDescription = i.invItemDescription,
                spriteName = i.spriteName,
                invItemCount = i.invItemCount,
                equipped = i.equipped,
                hierarchy = i.hierarchy,
                hierarchy2 = i.hierarchy2,
                itemValue = i.itemValue,
                slotNum = i.slotNum,
                autoSortToolbarSlot = i.autoSortToolbarSlot,
                initCount = i.initCount,
                initCountAI = i.initCountAI,
                rewardCount = i.rewardCount,
                maxAmmo = i.maxAmmo,
                lowCountThreshold = i.lowCountThreshold,
                stackable = i.stackable,
                stackableContents = i.stackableContents,
                canRepeatInShop = i.canRepeatInShop,
                nonStackableInShop = i.nonStackableInShop,
                specialDamage = i.specialDamage,
                incendiaryDamage = i.incendiaryDamage,
                throwDistance = i.throwDistance,
                throwDamage = i.throwDamage,
                throwExtraDist = i.throwExtraDist,
                touchDamage = i.touchDamage,
                otherDamage = i.otherDamage,
                meleeDamage = i.meleeDamage,
                meleeNoHit = i.meleeNoHit,
                nonLethal = i.nonLethal,
                hasCharges = i.hasCharges,
                startingItem = i.startingItem,
                canBeUsedOnDoor = i.canBeUsedOnDoor,
                canBeUsedOnWindow = i.canBeUsedOnWindow,
                canBeUsedOnSafe = i.canBeUsedOnSafe,
                canBeUsedOnComputer = i.canBeUsedOnComputer,
                isKey = i.isKey,
                isSafeCombination = i.isSafeCombination,
                notInLoadoutMachine = i.notInLoadoutMachine,
                noCountText = i.noCountText,
                doesNoDamage = i.doesNoDamage,
                rapidFire = i.rapidFire,
                shortRangeProjectile = i.shortRangeProjectile,
                longerRapidFire = i.longerRapidFire,
                noRefills = i.noRefills,
                cantStoreInATMMachine = i.cantStoreInATMMachine,
                itemType = i.itemType,
                weaponCode = (int)i.weaponCode,
                specificChunk = i.specificChunk,
                specificSector = i.specificSector,
                healthChange = i.healthChange,
                statusEffect = i.statusEffect,
                goesInToolbar = i.goesInToolbar,
                cantBeCloned = i.cantBeCloned,
                dontAutomaticallySelect = i.dontAutomaticallySelect,
                thiefCantSteal = i.thiefCantSteal,
                dontSelectNPC = i.dontSelectNPC,
                isWeapon = i.isWeapon,
                isArmor = i.isArmor,
                isArmorHead = i.isArmorHead,
                questItem = i.questItem,
                questItemCanBuy = i.questItemCanBuy,
                permanentHeadPiece = i.permanentHeadPiece,
                behindHair = i.behindHair,
                cantShowHair = i.cantShowHair,
                cantShowHairAtAll = i.cantShowHairAtAll,
                destroyAtLevelEnd = i.destroyAtLevelEnd,
                specialMeleeTexture = i.specialMeleeTexture,
                doSpill = i.doSpill,
                canHaveStartingOwner = i.canHaveStartingOwner,
                stealable = i.stealable,
                used = i.used,
                addedToMoneyCount = i.addedToMoneyCount,
                noSoundOnAgentHit = i.noSoundOnAgentHit,
                weaponToBeLoaded = i.weaponToBeLoaded,
                reactOnTouch = i.reactOnTouch,
                noShadow = i.noShadow,
                canCatchFire = i.canCatchFire,
                rechargingItem = i.rechargingItem,
                rechargeAmount = i.rechargeAmount,
                rechargeAmountInverse = i.rechargeAmountInverse,
                treasureVal = i.treasureVal,
                canHaveExtraAmmo = i.canHaveExtraAmmo,
                identifiedContents = i.identifiedContents,
                cantDrop = i.cantDrop,
                cantDropNPC = i.cantDropNPC,
                cantDropSpecificCharacter = i.cantDropSpecificCharacter,
                characterExclusive = i.characterExclusive,
                characterExclusiveSpecificCharacter = i.characterExclusiveSpecificCharacter,
                hitSoundType = i.hitSoundType,
                armorDepletionType = i.armorDepletionType,
                startingChunk = i.startingChunk,
                ownerID = i.ownerID,
                canFix = i.canFix,
                tiedToItemCode = i.tiedToItemCode,
                Categories = i.Categories,
                contents = i.contents,
                chunks = i.chunks,
                sectors = i.sectors,
                speedMod = i.speedMod,
                strengthMod = i.strengthMod,
                accuracyMod = i.accuracyMod,
                enduranceMod = i.enduranceMod,
                shadowOffset = i.shadowOffset,
                chanceToWear = i.chanceToWear,
                gunKnockback = i.gunKnockback,
                justAddedForNewCharacterEveryLevel = i.justAddedForNewCharacterEveryLevel,
                dontFlash = i.dontFlash,
                colliderSize = i.colliderSize
            };
        }

        private static InvItemLight ToInvItemLight(InvItemLightDto d)
        {
            if (d == null) return null;
            return new InvItemLight
            {
                invItemID = d.invItemID,
                itemNetID = d.itemNetID,
                invItemName = d.invItemName,
                invItemRealName = d.invItemRealName,
                invItemDescription = d.invItemDescription,
                spriteName = d.spriteName,
                invItemCount = d.invItemCount,
                equipped = d.equipped,
                hierarchy = d.hierarchy,
                hierarchy2 = d.hierarchy2,
                itemValue = d.itemValue,
                slotNum = d.slotNum,
                autoSortToolbarSlot = d.autoSortToolbarSlot,
                initCount = d.initCount,
                initCountAI = d.initCountAI,
                rewardCount = d.rewardCount,
                maxAmmo = d.maxAmmo,
                lowCountThreshold = d.lowCountThreshold,
                stackable = d.stackable,
                stackableContents = d.stackableContents,
                canRepeatInShop = d.canRepeatInShop,
                nonStackableInShop = d.nonStackableInShop,
                specialDamage = d.specialDamage,
                incendiaryDamage = d.incendiaryDamage,
                throwDistance = d.throwDistance,
                throwDamage = d.throwDamage,
                throwExtraDist = d.throwExtraDist,
                touchDamage = d.touchDamage,
                otherDamage = d.otherDamage,
                meleeDamage = d.meleeDamage,
                meleeNoHit = d.meleeNoHit,
                nonLethal = d.nonLethal,
                hasCharges = d.hasCharges,
                startingItem = d.startingItem,
                canBeUsedOnDoor = d.canBeUsedOnDoor,
                canBeUsedOnWindow = d.canBeUsedOnWindow,
                canBeUsedOnSafe = d.canBeUsedOnSafe,
                canBeUsedOnComputer = d.canBeUsedOnComputer,
                isKey = d.isKey,
                isSafeCombination = d.isSafeCombination,
                notInLoadoutMachine = d.notInLoadoutMachine,
                noCountText = d.noCountText,
                doesNoDamage = d.doesNoDamage,
                rapidFire = d.rapidFire,
                shortRangeProjectile = d.shortRangeProjectile,
                longerRapidFire = d.longerRapidFire,
                noRefills = d.noRefills,
                cantStoreInATMMachine = d.cantStoreInATMMachine,
                itemType = d.itemType,
                weaponCode = (weaponType)d.weaponCode,
                specificChunk = d.specificChunk,
                specificSector = d.specificSector,
                healthChange = d.healthChange,
                statusEffect = d.statusEffect,
                goesInToolbar = d.goesInToolbar,
                cantBeCloned = d.cantBeCloned,
                dontAutomaticallySelect = d.dontAutomaticallySelect,
                thiefCantSteal = d.thiefCantSteal,
                dontSelectNPC = d.dontSelectNPC,
                isWeapon = d.isWeapon,
                isArmor = d.isArmor,
                isArmorHead = d.isArmorHead,
                questItem = d.questItem,
                questItemCanBuy = d.questItemCanBuy,
                permanentHeadPiece = d.permanentHeadPiece,
                behindHair = d.behindHair,
                cantShowHair = d.cantShowHair,
                cantShowHairAtAll = d.cantShowHairAtAll,
                destroyAtLevelEnd = d.destroyAtLevelEnd,
                specialMeleeTexture = d.specialMeleeTexture,
                doSpill = d.doSpill,
                canHaveStartingOwner = d.canHaveStartingOwner,
                stealable = d.stealable,
                used = d.used,
                addedToMoneyCount = d.addedToMoneyCount,
                noSoundOnAgentHit = d.noSoundOnAgentHit,
                weaponToBeLoaded = d.weaponToBeLoaded,
                reactOnTouch = d.reactOnTouch,
                noShadow = d.noShadow,
                canCatchFire = d.canCatchFire,
                rechargingItem = d.rechargingItem,
                rechargeAmount = d.rechargeAmount,
                rechargeAmountInverse = d.rechargeAmountInverse,
                treasureVal = d.treasureVal,
                canHaveExtraAmmo = d.canHaveExtraAmmo,
                identifiedContents = d.identifiedContents,
                cantDrop = d.cantDrop,
                cantDropNPC = d.cantDropNPC,
                cantDropSpecificCharacter = d.cantDropSpecificCharacter,
                characterExclusive = d.characterExclusive,
                characterExclusiveSpecificCharacter = d.characterExclusiveSpecificCharacter,
                hitSoundType = d.hitSoundType,
                armorDepletionType = d.armorDepletionType,
                startingChunk = d.startingChunk,
                ownerID = d.ownerID,
                canFix = d.canFix,
                tiedToItemCode = d.tiedToItemCode,
                Categories = d.Categories ?? new List<string>(),
                contents = d.contents ?? new List<string>(),
                chunks = d.chunks ?? new List<int>(),
                sectors = d.sectors ?? new List<int>(),
                speedMod = d.speedMod,
                strengthMod = d.strengthMod,
                accuracyMod = d.accuracyMod,
                enduranceMod = d.enduranceMod,
                shadowOffset = d.shadowOffset,
                chanceToWear = d.chanceToWear,
                gunKnockback = d.gunKnockback,
                justAddedForNewCharacterEveryLevel = d.justAddedForNewCharacterEveryLevel,
                dontFlash = d.dontFlash,
                colliderSize = d.colliderSize
            };
        }
    }
}
