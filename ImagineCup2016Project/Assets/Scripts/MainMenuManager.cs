using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	private Vector3 startPos;

	void Start ()
	{
		startPos = transform.position;
	}

	void Update ()
	{
		// Start game
		if (Input.anyKeyDown)
			SceneManager.LoadScene("level1");

		// Move camera slightly
	}
}
