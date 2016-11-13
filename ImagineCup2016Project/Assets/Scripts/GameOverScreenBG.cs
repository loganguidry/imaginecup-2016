using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreenBG : MonoBehaviour
{
	public float fadeSpeed = 0.25f;
	Image img;

	void Start ()
	{
		img = GetComponent<Image>();
		img.color = new Color(0, 0, 0, 0);
	}

	void Update ()
	{
		if (GameManager.PlayerDead || GameManager.PlayerWon)
		{
			Color c = img.color;
			img.color = new Color(c.r, c.g, c.b, Mathf.Min(0.5f, c.a + fadeSpeed * Time.deltaTime));
		}
	}
}
