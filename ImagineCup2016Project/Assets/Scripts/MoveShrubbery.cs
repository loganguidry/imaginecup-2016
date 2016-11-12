using UnityEngine;
using System.Collections;

public class MoveShrubbery : MonoBehaviour
{
	public float moveSpeed;
	public float highestPosition;
	public float lowestPosition;

	void Start()
	{
		transform.position = new Vector3((Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)) + new Vector3(Random.Range(1, 10), 0, 0)).x, Random.Range(lowestPosition, highestPosition), 0);
	}

	void Update()
	{
		transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
	}
}
