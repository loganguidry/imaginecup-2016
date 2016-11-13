using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	public float cameraMoveAmount;
	private Vector3 startPos;

	void Start ()
	{
		startPos = transform.position;
		Cursor.visible = false;
	}

	void Update ()
	{
		// Start game
		if (Input.GetKeyDown(KeyCode.Space))
			SceneManager.LoadScene("level1");

		// Get mouse position
		Vector3 mousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		Rect cr = GetComponent<Camera>().pixelRect;
		Vector3 middle = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(cr.width / 2f, cr.height / 2f, 0));
		Vector3 diff = mousePos - middle;

		// Move camera slightly
		transform.position = startPos;
		transform.Translate(diff * cameraMoveAmount);
	}
}
