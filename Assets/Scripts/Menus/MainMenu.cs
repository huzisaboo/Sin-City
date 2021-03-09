using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
	public SceneReference sceneToLoad;


    public void onLoadScene()
	{
		SceneLoader.Instance.onSceneLoadedEvent.AddListener(GameSceneLoadedCallback);
		SceneLoader.Instance.LoadScene(sceneToLoad, true);
		//Debug.Log(SceneLoader.Instance.onSceneLoadedEvent.GetPersistentEventCount());
		
	}

	public void onQuitGame()
    {
		Application.Quit();
    }

	private void GameSceneLoadedCallback(List<string> p_scenesLoaded)
    {
		MenuManager.Instance.hideMenu(menuClassifier);
		SceneLoader.Instance.SetActiveScene(p_scenesLoaded[0]);
		SceneLoader.Instance.onSceneLoadedEvent.RemoveListener(GameSceneLoadedCallback);
    }
}
