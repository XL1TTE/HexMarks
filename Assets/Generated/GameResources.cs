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
    public static class CMS
    {
        public static class Cards
        {
            public static CMSEntityPfb card_FireBall => Resources.Load<CMSEntityPfb>("CMS/Cards/card_FireBall");
            public static CMSEntityPfb card_SummonLunarWraith => Resources.Load<CMSEntityPfb>("CMS/Cards/card_SummonLunarWraith");
        }
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
    public static class GameEntities
    {
        public static class Cards
        {
            public static class Abilities
            {
                public static class FireBall
                {
                    public static AudioClip fireball => Resources.Load<AudioClip>("GameEntities/Cards/Abilities/FireBall/fireball");
                }
            }
            public static class Prefabs
            {
                public static CardView FireBall => Resources.Load<CardView>("GameEntities/Cards/Prefabs/FireBall");
            }
            public static class Sprites
            {
                public static Sprite s_Card => Resources.Load<Sprite>("GameEntities/Cards/Sprites/s_Card");
                public static Sprite s_FireBall => Resources.Load<Sprite>("GameEntities/Cards/Sprites/s_FireBall");
                public static Sprite s_FireIce => Resources.Load<Sprite>("GameEntities/Cards/Sprites/s_FireIce");
            }
        }
        public static class Enemies
        {
            public static class Abilities
            {
                public static class CaveGolem
                {
                    public static AudioClip Monster_Rumble3 => Resources.Load<AudioClip>("GameEntities/Enemies/Abilities/CaveGolem/Monster_Rumble3");
                }
            }
            public static class Prefabs
            {
                public static EnemyView CaveGolemBoss => Resources.Load<EnemyView>("GameEntities/Enemies/Prefabs/CaveGolemBoss");
                public static EnemyView Enemy => Resources.Load<EnemyView>("GameEntities/Enemies/Prefabs/Enemy");
                public static EnemyView Golem => Resources.Load<EnemyView>("GameEntities/Enemies/Prefabs/Golem");
                public static EnemyView Wizard => Resources.Load<EnemyView>("GameEntities/Enemies/Prefabs/Wizard");
            }
            public static class Sprites
            {
                public static class Golem
                {
                    public static Sprite Golem_1_attack => Resources.Load<Sprite>("GameEntities/Enemies/Sprites/Golem/Golem_1_attack");
                    public static Sprite Golem_1_die => Resources.Load<Sprite>("GameEntities/Enemies/Sprites/Golem/Golem_1_die");
                    public static Sprite Golem_1_idle => Resources.Load<Sprite>("GameEntities/Enemies/Sprites/Golem/Golem_1_idle");
                }
                public static class Wizard
                {
                    public static Sprite Attack => Resources.Load<Sprite>("GameEntities/Enemies/Sprites/Wizard/Attack");
                    public static Sprite Death => Resources.Load<Sprite>("GameEntities/Enemies/Sprites/Wizard/Death");
                    public static Sprite Idle => Resources.Load<Sprite>("GameEntities/Enemies/Sprites/Wizard/Idle");
                }
            }
        }
        public static class Heroes
        {
            public static class Prefabs
            {
                public static HeroView HeroExample => Resources.Load<HeroView>("GameEntities/Heroes/Prefabs/HeroExample");
            }
        }
    }
    public static class UI
    {
        public static class Fonts
        {
        }
        public static HealthBar EnemyHealthBar => Resources.Load<HealthBar>("UI/EnemyHealthBar");
        public static HealthBar HeroHealthBar => Resources.Load<HealthBar>("UI/HeroHealthBar");
        public static CanvasScaler UINotification => Resources.Load<CanvasScaler>("UI/UINotification");
    }
    public static ProjectContext ProjectContext => Resources.Load<ProjectContext>("ProjectContext");
}
