using System;
using System.Collections.Generic;

namespace RogueSaveEditor
{
    /// <summary>
    /// DTO for JSON serialization - mirrors UnlockSaveData structure
    /// </summary>
    public class UnlockSaveDataDto
    {
        public List<UnlockDto> unlocks { get; set; }
        public List<HighScoreDto> highScores { get; set; }
        public List<string> customCharacterSlots { get; set; }
        public InvItemLightDto storedItem { get; set; }
        public InvItemLightDto storedItem2 { get; set; }
        public InvItemLightDto storedItem3 { get; set; }
        public InvItemLightDto storedItem4 { get; set; }
        public InvItemLightDto storedItem5 { get; set; }
        public int totalDeaths { get; set; }
        public int totalWins { get; set; }
        public int totalGamesPlayed { get; set; }
        public int nuggets { get; set; }
        public string lastDailyRun { get; set; }
        public bool finishedTutorial { get; set; }
        public bool viewedReadThis { get; set; }
        public string currentVersion { get; set; }
        public List<List<UnlockDto>> loadoutList { get; set; }
        public List<List<string>> rewardConfigConsoleList { get; set; }
        public List<List<string>> traitConfigConsoleList { get; set; }
        public List<List<string>> mutatorConfigConsoleList { get; set; }
    }

    /// <summary>
    /// DTO for JSON serialization - mirrors Unlock structure
    /// </summary>
    public class UnlockDto
    {
        public string unlockName { get; set; }
        public string unlockNameType { get; set; }
        public string unlockDescriptionType { get; set; }
        public string unlockType { get; set; }
        public bool unlocked { get; set; }
        public bool nowAvailable { get; set; }
        public bool onlyInCharacterCreation { get; set; }
        public bool removeInMech { get; set; }
        public bool freeItem { get; set; }
        public bool notActive { get; set; }
        public bool unavailable { get; set; }
        public bool removal { get; set; }
        public string replacing { get; set; }
        public bool isUpgrade { get; set; }
        public bool cantSwap { get; set; }
        public bool cantLose { get; set; }
        public bool dcUnlock { get; set; }
        public int progressCount { get; set; }
        public List<string> progressList { get; set; }
        public List<string> agents { get; set; }
        public List<string> categories { get; set; }
        public List<string> specialAbilities { get; set; }
        public List<string> leadingTraits { get; set; }
        public List<string> leadingItems { get; set; }
        public List<string> leadingBigQuests { get; set; }
        public List<string> prerequisites { get; set; }
        public List<string> cancellations { get; set; }
        public List<string> recommendations { get; set; }
        public string upgrade { get; set; }
        public int cost { get; set; }
        public int cost2 { get; set; }
        public int cost3 { get; set; }
        public bool notOnClient { get; set; }
    }

    /// <summary>
    /// DTO for JSON serialization - mirrors HighScore structure
    /// </summary>
    public class HighScoreDto
    {
        public int skillPoints { get; set; }
        public int skillLevel { get; set; }
        public string agentName { get; set; }
        public int finalFloor { get; set; }
        public string finalFloorText { get; set; }
        public float timeTaken { get; set; }
        public string timeTakenText { get; set; }
        public string deathMethod { get; set; }
        public string deathKiller { get; set; }
    }

    /// <summary>
    /// DTO for JSON serialization - mirrors InvItemLight structure
    /// </summary>
    public class InvItemLightDto
    {
        public int invItemID { get; set; }
        public int itemNetID { get; set; }
        public string invItemName { get; set; }
        public string invItemRealName { get; set; }
        public string invItemDescription { get; set; }
        public string spriteName { get; set; }
        public int invItemCount { get; set; }
        public bool equipped { get; set; }
        public float hierarchy { get; set; }
        public float hierarchy2 { get; set; }
        public int itemValue { get; set; }
        public int slotNum { get; set; }
        public int autoSortToolbarSlot { get; set; }
        public int initCount { get; set; }
        public int initCountAI { get; set; }
        public int rewardCount { get; set; }
        public int maxAmmo { get; set; }
        public int lowCountThreshold { get; set; }
        public bool stackable { get; set; }
        public bool stackableContents { get; set; }
        public bool canRepeatInShop { get; set; }
        public bool nonStackableInShop { get; set; }
        public bool specialDamage { get; set; }
        public bool incendiaryDamage { get; set; }
        public int throwDistance { get; set; }
        public int throwDamage { get; set; }
        public bool throwExtraDist { get; set; }
        public int touchDamage { get; set; }
        public int otherDamage { get; set; }
        public int meleeDamage { get; set; }
        public bool meleeNoHit { get; set; }
        public bool nonLethal { get; set; }
        public bool hasCharges { get; set; }
        public bool startingItem { get; set; }
        public bool canBeUsedOnDoor { get; set; }
        public bool canBeUsedOnWindow { get; set; }
        public bool canBeUsedOnSafe { get; set; }
        public bool canBeUsedOnComputer { get; set; }
        public bool isKey { get; set; }
        public bool isSafeCombination { get; set; }
        public bool notInLoadoutMachine { get; set; }
        public bool noCountText { get; set; }
        public bool doesNoDamage { get; set; }
        public bool rapidFire { get; set; }
        public bool shortRangeProjectile { get; set; }
        public bool longerRapidFire { get; set; }
        public bool noRefills { get; set; }
        public bool cantStoreInATMMachine { get; set; }
        public string itemType { get; set; }
        public int weaponCode { get; set; }
        public int specificChunk { get; set; }
        public int specificSector { get; set; }
        public int healthChange { get; set; }
        public string statusEffect { get; set; }
        public bool goesInToolbar { get; set; }
        public bool cantBeCloned { get; set; }
        public bool dontAutomaticallySelect { get; set; }
        public bool thiefCantSteal { get; set; }
        public bool dontSelectNPC { get; set; }
        public bool isWeapon { get; set; }
        public bool isArmor { get; set; }
        public bool isArmorHead { get; set; }
        public bool questItem { get; set; }
        public bool questItemCanBuy { get; set; }
        public bool permanentHeadPiece { get; set; }
        public bool behindHair { get; set; }
        public bool cantShowHair { get; set; }
        public bool cantShowHairAtAll { get; set; }
        public bool destroyAtLevelEnd { get; set; }
        public bool specialMeleeTexture { get; set; }
        public bool doSpill { get; set; }
        public bool canHaveStartingOwner { get; set; }
        public bool stealable { get; set; }
        public bool used { get; set; }
        public bool addedToMoneyCount { get; set; }
        public bool noSoundOnAgentHit { get; set; }
        public bool weaponToBeLoaded { get; set; }
        public bool reactOnTouch { get; set; }
        public bool noShadow { get; set; }
        public bool canCatchFire { get; set; }
        public bool rechargingItem { get; set; }
        public int rechargeAmount { get; set; }
        public int rechargeAmountInverse { get; set; }
        public int treasureVal { get; set; }
        public bool canHaveExtraAmmo { get; set; }
        public bool identifiedContents { get; set; }
        public bool cantDrop { get; set; }
        public bool cantDropNPC { get; set; }
        public string cantDropSpecificCharacter { get; set; }
        public bool characterExclusive { get; set; }
        public string characterExclusiveSpecificCharacter { get; set; }
        public string hitSoundType { get; set; }
        public string armorDepletionType { get; set; }
        public int startingChunk { get; set; }
        public int ownerID { get; set; }
        public bool canFix { get; set; }
        public int tiedToItemCode { get; set; }
        public List<string> Categories { get; set; }
        public List<string> contents { get; set; }
        public List<int> chunks { get; set; }
        public List<int> sectors { get; set; }
        public int speedMod { get; set; }
        public int strengthMod { get; set; }
        public int accuracyMod { get; set; }
        public int enduranceMod { get; set; }
        public int shadowOffset { get; set; }
        public int chanceToWear { get; set; }
        public int gunKnockback { get; set; }
        public bool justAddedForNewCharacterEveryLevel { get; set; }
        public bool dontFlash { get; set; }
        public string colliderSize { get; set; }
    }
}
