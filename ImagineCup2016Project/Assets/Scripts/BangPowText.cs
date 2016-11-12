using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BangPowText : MonoBehaviour
{
	public string[] randomText = new string[0];
	public Color[] randomColors = new Color[0];
	public float randomRotation;
	public float randomPosition;
	public float riseSpeed;
	public float fadeSpeed;
	Text txt;

	void Start ()
	{
		// Random position and rotation
		transform.Translate(new Vector3(Random.Range(-randomPosition, randomPosition), 80f, 0));
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(-randomRotation, randomRotation));

		// Reference components
		txt = GetComponent<Text>();

		// Choose random text
		int randomTextIndex = Random.Range(0, randomText.Length);
		txt.text = randomText[randomTextIndex];

		// Choose random color
		int randomColorIndex = Random.Range(0, randomColors.Length);
		txt.color = randomColors[randomColorIndex];
	}

	void Update ()
	{
		// Rise up
		transform.Translate(new Vector3(0, riseSpeed * Time.deltaTime, 0));

		// Fade out
		Color c = txt.color;
		txt.color = new Color(c.r, c.g, c.b, c.a - fadeSpeed * Time.deltaTime);

		// Delete
		if (txt.color.a <= 0)
			Destroy(gameObject);
	}
}
