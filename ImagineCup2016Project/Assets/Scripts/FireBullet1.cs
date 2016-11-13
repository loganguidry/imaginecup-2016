using UnityEngine;
using System.Collections;

public class FireBullet1 : MonoBehaviour
{
	public LayerMask layerMask;

	void Start ()
	{
		// Fire a raycast to get the first object
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f, layerMask);
		if (hit.transform != null && hit.transform.tag != "Enemy")
		{
			// Stop the bullet trail at the collision point
			float dist = (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude;
			transform.localScale = new Vector3(dist / 10f, transform.localScale.y, transform.localScale.z);

			// Damage player
			float damage = Random.Range(1f, 4f);
			if (hit.transform.tag == "Player")
				GameManager.DamagePlayer(damage);
		}
	}
}
