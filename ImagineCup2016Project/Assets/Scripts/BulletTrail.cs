using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour
{
	public float fadeSpeed;
	SpriteRenderer sprite;

	void Start ()
	{
		sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		// Fade out
		Color c = sprite.color;
		sprite.color = new Color(c.r, c.g, c.b, c.a - fadeSpeed * Time.deltaTime);

		// Destroy
		if (sprite.color.a <= 0)
			Destroy(gameObject);
	}
}
