using UnityEngine;

namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 0;
        }
    
        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    
    }
}
