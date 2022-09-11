using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
	public class SceneLoader : MonoBehaviour
	{
		private static event EventManager.VoidEventHandler LoadGameplay;
		private static event EventManager.VoidEventHandler LoadMainMenu;
	
		private static SceneLoader _instance;
	
		void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else if (_instance != this)
			{
				Destroy(gameObject);
			}
		}
	
		private void OnEnable()
		{
			LoadGameplay += LoadGameplayScene;
		
			LoadMainMenu += LoadMenuScene;
		}

		private void OnDisable()
		{
			LoadGameplay -= LoadGameplayScene;
		
			LoadMainMenu -= LoadMenuScene;
		}

		private void LoadMenuScene()
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		}

		private void LoadGameplayScene()
		{
			SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
		}
	
		public static void InvokeLoadGameplayScene()
		{
			LoadGameplay?.Invoke();
		}

		public static void InvokeLoadMainMenuScene()
		{
			LoadMainMenu?.Invoke();
		}
	}
}