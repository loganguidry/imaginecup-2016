using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	void Start ()
	{
	
	}

	void Update ()
	{
		// Start game
		if (Input.anyKeyDown)
			SceneManager.LoadScene("level1");
	}
}
