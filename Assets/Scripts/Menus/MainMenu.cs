using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
	public SceneReference sceneToLoad;

	public void onLoadScene()
	{
		SceneLoader.Instance.onSceneLoadedEvent.AddListener(SceneLoadedCallback);
		SceneLoader.Instance.LoadScene(sceneToLoad, true);
		MenuManager.Instance.hideMenu(menuClassifier);
	}

	public void onQuitGame()
    {
		Application.Quit();
    }

	private void SceneLoadedCallback(List<string> p_scenesLoaded)
    {
		SceneLoader.Instance.SetActiveScene(sceneToLoad);
		SceneLoader.Instance.onSceneLoadedEvent.RemoveListener(SceneLoadedCallback);
    }
}
