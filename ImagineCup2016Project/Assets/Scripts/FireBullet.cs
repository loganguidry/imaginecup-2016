using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour
{
	public LayerMask layerMask;

	void Start ()
	{
		// Fire a raycast to get the first object
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f, layerMask);
		if (hit.transform != null && hit.transform.tag != "Player")
		{
			// Stop the bullet trail at the collision point
			float dist = (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude;
			transform.localScale = new Vector3(dist / 10f, transform.localScale.y, transform.localScale.z);

			// Damage enemy (if you hit an enemy)
			float currentWeaponDamage = GameManager.Player.GetComponent<PlayerController>().CurrentWeapon.GetComponent<WeaponProperties>().damage;
			if (hit.transform.tag == "Enemy")
				hit.transform.GetComponent<EnemyManager>().TakeDamage(currentWeaponDamage);
		}
	}
}
