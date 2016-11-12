using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	static public float MinimumX;
	public float acceleration;
	Vector2 velocity = Vector2.zero;
	Transform weaponAnchor;

	void Start()
	{
		weaponAnchor = transform.Find("WeaponAnchor");	
	}

	void Update()
	{
		Movement();
		Weapon();
	}

	void Movement()
	{
		// Get the current velocity
		velocity = GetComponent<Rigidbody2D>().velocity;

		// Move left and right
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			velocity += new Vector2(-acceleration, 0);
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			velocity += new Vector2(acceleration, 0);

		// Jump
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
			velocity += new Vector2(0, -velocity.y + 5);

		// Keep in the level
		if (transform.position.x < MinimumX)
		{
			transform.position = new Vector3(MinimumX, transform.position.y, transform.position.z);
			velocity = new Vector2(0f, velocity.y);
		}

		// Set the new velocity
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	void Weapon()
	{
		// Point weapon towards mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dist = mousePos - weaponAnchor.position;
		float angle = Mathf.Atan(dist.y / dist.x) * Mathf.Rad2Deg;
		if (dist.x < 0)
			angle = 180 + angle;
		weaponAnchor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		// Fire weapon
		if (Input.GetMouseButtonDown(0))
			FireWeapon();
	}

	void FireWeapon()
	{
		print("bang pow");
		CameraShake.Kick(0.05f);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(velocity.x, velocity.y, 0));
	}
}