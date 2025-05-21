using System;
using System.Collections.Generic;
using GameUtilities;
using Zenject;

namespace Project.InteractorSystem
{
    public abstract class InteractionBase{}

    public class Interactor 
    {
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            AutoAddInteractions(diContainer);
        }
        
        private void AutoAddInteractions(DiContainer diContainer){
            Type[] types = ReflectionUtility.GetSubclasses<InteractionBase>();
            foreach(var t in types){
                InteractionBase interaction = diContainer.Instantiate(t) as InteractionBase;
                m_all.Add(interaction);
            }
        }
        
        internal List<InteractionBase> m_all = new();
        
        public List<T> GetAll<T>(){
            return Interactor_Cache<T>.Get(this);
        }
        
    }
    
    internal static class Interactor_Cache<T>{
        private static List<T> m_cache;
        
        public static List<T> Get(Interactor interactor){
            if(m_cache == null){
                m_cache = new List<T>(64);
            }
            
            foreach(var i in interactor.m_all){
                if(i is T interaction){
                    m_cache.Add(interaction);
                }
            }
            return m_cache;      
        }
    }
}
