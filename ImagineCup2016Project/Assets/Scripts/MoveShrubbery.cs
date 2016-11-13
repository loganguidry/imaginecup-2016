using UnityEngine;
using System.Collections;

public class MoveShrubbery : MonoBehaviour
{
	public float moveSpeed;
	public float highestPosition;
	public float lowestPosition;
	float spawnTime;

	void Start()
	{
		spawnTime = Time.time;
		float randomHeight = Random.Range(lowestPosition, highestPosition);
		transform.position = new Vector3((Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)) + new Vector3(Random.Range(1, 10), 0, 0)).x, randomHeight, 0);
		moveSpeed = -randomHeight;
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.FloorToInt(-randomHeight * 10 + 10);
	}

	void Update()
	{
		transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

		// Delete
		if (Time.time - spawnTime >= 10f)
			Destroy(gameObject);
	}
}
