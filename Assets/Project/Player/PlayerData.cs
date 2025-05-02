using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{

    public class PlayerData
    {
        private float m_MaxHealth = 100.0f;
        public float GetMaxHealth() => m_MaxHealth;
        private void SetMaxHealth(float value) => m_MaxHealth = value; 
    }
}
