using CMSystem;
using Project.Actors;
using UnityEngine;

namespace Project.Cards{
    

    public class CardState
    {
        public CardState(CMSEntity cmsModel){
            model = cmsModel;
        }   
        public readonly CMSEntity model;
        private         Hero      m_owner;
        
        
        public Hero GetOwner() => m_owner;
        public void SetOwner(Hero hero) => m_owner = hero;        
    }
}


