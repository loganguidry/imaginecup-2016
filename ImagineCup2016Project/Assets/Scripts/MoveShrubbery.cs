using UnityEngine;
using System.Collections;

public class MoveShrubbery : MonoBehaviour
{
	public float moveSpeed;

	void Update ()
	{
		transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
	}
}
