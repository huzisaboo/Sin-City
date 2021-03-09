using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{
	public SceneReference mainMenuScene;
	public MenuClassifier mainMenuClassifier;
	//private string m_currentScene;

	public void onBackToMainMenu()
    {
		//SceneLoader.Instance.onSceneLoadedEvent.AddListener(mainMenuLoadedCallback);
		MenuManager.Instance.hideMenu(menuClassifier);
		//m_currentScene = SceneLoader.Instance.GetActiveSceneName();
		SceneLoader.Instance.UnloadActiveScene();
		SceneLoader.Instance.SetActiveScene(mainMenuScene);
		MenuManager.Instance.showMenu(mainMenuClassifier);
	}

	public void onQuitGame()
	{
		Application.Quit();
	}

	public void onPlayAgain()
    {
		SceneLoader.Instance.ReloadActiveScene();
    }	
}
