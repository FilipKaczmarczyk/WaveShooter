using PlayerData.ScriptableObjects;
using SceneLoading;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO playerData;
    
        public void NewGame()
        {
            playerData.Reset();

            SceneLoader.InvokeLoadGameplayScene();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
