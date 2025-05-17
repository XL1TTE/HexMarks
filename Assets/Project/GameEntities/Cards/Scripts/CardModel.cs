using System;
using System.Collections;
using System.Collections.Generic;
using CardTags;
using CMSystem;
using DG.Tweening;
using Project.Cards.Effects;
using Project.DataResolving;
using Project.JobSystem;
using UnityEngine;
using XL1TTE.GameActions;

namespace Project.Cards{
    
    [Serializable]
    public class CardState
    {
        public CardState(CMSEntity cmsModel){
            m_cmsModel = cmsModel;
        }
        
        [SerializeField] private CMSEntity m_cmsModel;
        public CMSEntity GetModel() => m_cmsModel;        

    
        
    }
}


