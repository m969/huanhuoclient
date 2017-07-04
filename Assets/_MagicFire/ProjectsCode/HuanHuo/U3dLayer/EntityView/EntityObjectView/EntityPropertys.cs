namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;

    public class EntityPropertys
    {
        public const string EntityName = "entityName";
        public const string Position = "position";
        public const string Direction = "position";
    }

    public class CombatPropertys : EntityPropertys
    {
        public const string Hp = "HP";
        public const string HpMax = "HP_Max";
    }

    public class AvatarPropertys : CombatPropertys
    {
        public const string GoldCount = "goldCount";

        public const string Sp = "SP";
        public const string SpMax = "SP_Max";
        public const string Msp = "MSP";
        public const string MspMax = "MSP_Max";

        public const string Talent = "talent";
        public const string Level = "level";
        public const string LevelName = "levelName";
        public const string LevelPeriod = "levelPeriod";
        public const string LevelPeriodName = "levelPeriodName";
        public const string LastOnlineTime = "lastOnlineTime";
        public const string CurrentTaskId = "currentTaskID";
        public const string AvatarBag = "avatarBag";
        public const string TaskInfoList = "taskInfoList";
        public const string TaskCounter = "taskCounter";
        public const string CurrentEquipmentId = "currentEquipmentID";
    }

    public class MonsterPropertys : CombatPropertys
    {
        public const string SpawnPos = "spawnPos";
    }

    public class NpcPropertys : EntityPropertys
    {
        public const string StoreGoodsIdList = "storeGoodsIDList";
        public const string AvatarInfoKeepDict = "avatarInfoKeepDict";
    }

    public class TriggerPeopertys : EntityPropertys
    {
        public const string TriggerId = "triggerID";
        public const string TriggerSize = "triggerSize";
        public const string Damage = "damage";
        public const string ParentSkill = "parentSkill";
        public const string ParentEntityId = "parentEntityID";
        public const string SpellCaster = "spellCaster";
    }
}