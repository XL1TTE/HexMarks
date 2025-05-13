using CMSystem;
using Project.Actors;
using Project.Actors.Stats;
using SaveDataProto.DataClasses;

namespace SaveData{
    public static class SaveFileMapper{
        
        
        public static void protoSaveToSaveFile(protoSaveFile protoSave){
            
            var protoHeroes = protoSave.PlayerData.Heroes;

            foreach (var h in protoHeroes)
            {
                var hero = new HeroState(
                    h.Id, CMS.Get<CMSEntity>(h.IdModel),
                    new HeroStats
                    {
                        m_BaseStats = new ActorStats
                        {
                            m_Health = h.Stats.BaseStats.Health,
                            m_MaxHealth = h.Stats.BaseStats.MaxHealth,
                            m_Initiative = h.Stats.BaseStats.Initiative
                        },
                        m_MaxCardsInHand = h.Stats.HandCapacity
                    });

                SaveFile.AccessPlayerData().AddHero(hero);
            }
        }
        
        public static protoSaveFile SaveFileToProtoSave(){

            var protoSave = new protoSaveFile();

            protoSave.PlayerData = new protoPlayerSave();

            foreach (var h in SaveFile.AccessPlayerData().GetHeroes())
            {
                var hero = new protoHero
                {
                    Id = h.m_id,
                    IdModel = h.m_model.id,
                    Stats = new protoHeroStats
                    {
                        BaseStats = new protoActorStats
                        {
                            Health = h.m_stats.m_BaseStats.m_Health,
                            MaxHealth = h.m_stats.m_BaseStats.m_MaxHealth,
                            Initiative = h.m_stats.m_BaseStats.m_Initiative
                        },
                        HandCapacity = h.m_stats.m_MaxCardsInHand
                    }
                };

                protoSave.PlayerData.Heroes.Add(hero);
            }
            return protoSave;
        }
    }   
}
