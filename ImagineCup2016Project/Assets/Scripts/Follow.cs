using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
	public Transform target;
	public float damping;
	public Vector3 offset;
	public bool isCamera;

	void Update ()
	{
		transform.Translate(((target.position + offset) - transform.position) * damping);

		if (isCamera)
			CameraShake.OriginalPosition = transform.position;
	}
}
