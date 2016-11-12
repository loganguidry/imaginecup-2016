using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public float rotateSpeed;

	void Update ()
	{
		transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
	}
}
