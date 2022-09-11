using Events;
using UnityEngine;

namespace Turret
{
    public class TurretHealth : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler TurretHealthChange;
        public static event EventManager.VoidEventHandler PlayerDied;
        public int CurrentHealth { get; private set; }

        [SerializeField] private int maxHealth;
        
        private void Awake()
        {
            SetTurretHealth(maxHealth);
        }

        public void AddTurretHealth(int amount)
        {
            CurrentHealth += amount;
            
            TurretHealthChange?.Invoke();

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                
                PlayerDied?.Invoke();
            }
        }

        public void SetTurretHealth(int amount)
        {
            CurrentHealth = amount;
            
            TurretHealthChange?.Invoke();
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }
    }
}
