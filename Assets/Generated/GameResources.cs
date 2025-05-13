using CMSystem;
using Project.Actors;
using Project.Cards;
using Project.Enemies;
using Project.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

// This file is auto-generated. Do not modify manually.

public static class GameResources
{
    public static class Cards
    {
        public static CardView CardView => Resources.Load<CardView>("Cards/CardView");
        public static Sprite Card_001 => Resources.Load<Sprite>("Cards/Card_001");
        public static Sprite Card_002 => Resources.Load<Sprite>("Cards/Card_002");
        public static Sprite Card_003 => Resources.Load<Sprite>("Cards/Card_003");
    }
    public static class CMS
    {
        public static class Enemies
        {
            public static CMSEntityPfb e_CaveGolemBoss => Resources.Load<CMSEntityPfb>("CMS/Enemies/e_CaveGolemBoss");
            public static CMSEntityPfb e_StoneGolem => Resources.Load<CMSEntityPfb>("CMS/Enemies/e_StoneGolem");
            public static CMSEntityPfb e_Wizzard => Resources.Load<CMSEntityPfb>("CMS/Enemies/e_Wizzard");
        }
        public static class Heroes
        {
            public static CMSEntityPfb Hero_1 => Resources.Load<CMSEntityPfb>("CMS/Heroes/Hero 1");
            public static CMSEntityPfb Hero_2 => Resources.Load<CMSEntityPfb>("CMS/Heroes/Hero 2");
        }
        public static class MapLocations
        {
            public static CMSEntityPfb Dungeon_1 => Resources.Load<CMSEntityPfb>("CMS/MapLocations/Dungeon 1");
            public static CMSEntityPfb Dungeon_2 => Resources.Load<CMSEntityPfb>("CMS/MapLocations/Dungeon 2");
        }
    }
    public static class Enemies
    {
        public static class Defs
        {
        }
        public static class DieAnims
        {
        }
        public static class Prefabs
        {
            public static EnemyView CaveGolemBoss => Resources.Load<EnemyView>("Enemies/Prefabs/CaveGolemBoss");
            public static EnemyView Enemy => Resources.Load<EnemyView>("Enemies/Prefabs/Enemy");
            public static EnemyView Golem => Resources.Load<EnemyView>("Enemies/Prefabs/Golem");
            public static EnemyView Wizard => Resources.Load<EnemyView>("Enemies/Prefabs/Wizard");
        }
        public static class Sprites
        {
            public static class Golem
            {
                public static Sprite Golem_1_attack => Resources.Load<Sprite>("Enemies/Sprites/Golem/Golem_1_attack");
                public static Sprite Golem_1_die => Resources.Load<Sprite>("Enemies/Sprites/Golem/Golem_1_die");
                public static Sprite Golem_1_idle => Resources.Load<Sprite>("Enemies/Sprites/Golem/Golem_1_idle");
            }
            public static class Wizard
            {
                public static Sprite Attack => Resources.Load<Sprite>("Enemies/Sprites/Wizard/Attack");
                public static Sprite Death => Resources.Load<Sprite>("Enemies/Sprites/Wizard/Death");
                public static Sprite Idle => Resources.Load<Sprite>("Enemies/Sprites/Wizard/Idle");
            }
        }
    }
    public static class Fonts
    {
    }
    public static class Heroes
    {
        public static HeroView Hero_1 => Resources.Load<HeroView>("Heroes/Hero 1");
    }
    public static class UI
    {
        public static HealthBar EnemyHealthBar => Resources.Load<HealthBar>("UI/EnemyHealthBar");
        public static HealthBar HeroHealthBar => Resources.Load<HealthBar>("UI/HeroHealthBar");
        public static CanvasScaler UINotification => Resources.Load<CanvasScaler>("UI/UINotification");
    }
    public static ProjectContext ProjectContext => Resources.Load<ProjectContext>("ProjectContext");
}
