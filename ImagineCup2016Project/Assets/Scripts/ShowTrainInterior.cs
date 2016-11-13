using UnityEngine;
using System.Collections;

public class ShowTrainInterior : MonoBehaviour
{
	public float fadeSpeed;
	private BoxCollider2D triggerZone;

	void Start ()
	{
		triggerZone = GetComponent<BoxCollider2D>();
	}

	void Update ()
	{
		if (GameManager.Player.GetComponent<BoxCollider2D>().IsTouching(triggerZone))
		{
			Color c = GetComponent<SpriteRenderer>().color;
			float newA = Mathf.Max(c.a - fadeSpeed * Time.deltaTime, 0f);
			GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, newA);
		}
		else
		{
			Color c = GetComponent<SpriteRenderer>().color;
			float newA = Mathf.Min(c.a + fadeSpeed * Time.deltaTime, 1f);
			GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, newA);
		}
	}
}
