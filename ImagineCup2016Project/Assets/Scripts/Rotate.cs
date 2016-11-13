using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public float rotateSpeed;
	public bool slowerOnMenu;

	void Start()
	{
		if (slowerOnMenu)
		{
			if (SceneManager.GetActiveScene().name == "mainMenu")
				rotateSpeed = -100f;
		}
	}

	void Update()
	{
		transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
	}
}
