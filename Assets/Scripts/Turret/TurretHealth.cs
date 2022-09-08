using System;
using Events;
using UnityEngine;

namespace Turret
{
    public class TurretHealth : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler TurretHealthChange;
        public int CurrentHealth { get; private set; }

        [SerializeField] private int maxHealth;
        
        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void ChangeTurretHealth(int amount)
        {
            CurrentHealth += amount;
            
            TurretHealthChange?.Invoke();
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }
    }
}
