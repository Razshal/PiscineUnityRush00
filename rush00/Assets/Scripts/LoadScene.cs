using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour {
	public void load (string name) 
    {
        SceneManager.LoadScene(name);
	}

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void exit() 
    {
		Application.Quit();
	}
}
