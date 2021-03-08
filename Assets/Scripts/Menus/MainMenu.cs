using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
	public SceneReference sceneToLoad;

	public void onLoadScene()
	{
		SceneLoader.Instance.LoadScene(sceneToLoad, true);
		MenuManager.Instance.hideMenu(menuClassifier);
	}

	public void onQuitGame()
    {
		Application.Quit();
    }
}
