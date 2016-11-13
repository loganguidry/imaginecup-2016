﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	static public float MinimumX;
	public float acceleration;
	public float jumpPower;
	public GameObject bangPowTextPopup;
	public GameObject bulletPrefab;
	public bool circularAiming;
	public float maxVelocity;
	Vector2 velocity = Vector2.zero;
	Transform weaponAnchor;
	string currentDirection = "right";

	GameObject pistolWeapon;
	GameObject rifleWeapon;

	[HideInInspector]
	public GameObject CurrentWeapon;

	float lastWeaponShootTime;

	public LayerMask groundLayerMask;

	void Start()
	{
		weaponAnchor = transform.Find("WeaponAnchor");

		// Weapons
		pistolWeapon = weaponAnchor.Find("Pistol").gameObject;
		rifleWeapon = weaponAnchor.Find("Rifle").gameObject;
		rifleWeapon.GetComponent<SpriteRenderer>().enabled = false;
		CurrentWeapon = pistolWeapon;
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
		if (Input.GetKey(KeyCode.A))
			velocity += new Vector2(-acceleration, 0);
		if (Input.GetKey(KeyCode.D))
			velocity += new Vector2(acceleration, 0);

		// Jump
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
		{
			RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0, 0), Vector3.down, 0.1f, groundLayerMask);
			RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0, 0), Vector3.down, 0.1f, groundLayerMask);
			if (hitLeft.transform != null || hitRight.transform != null)
				velocity += new Vector2(0, -velocity.y + jumpPower);
		}

		// Keep in the level
		if (transform.position.x < MinimumX)
		{
			transform.position = new Vector3(MinimumX, transform.position.y, transform.position.z);
			velocity = new Vector2(0f, velocity.y);
		}

		// Keep within max velocity
		if (velocity.x > maxVelocity)
			velocity.x = maxVelocity;
		if (velocity.x < -maxVelocity)
			velocity.x = -maxVelocity;

		// Set the new velocity
		GetComponent<Rigidbody2D>().velocity = velocity;

		if (!circularAiming)
		{
			// Set direction
			if (Time.time - lastWeaponShootTime >= 0.35f)
			{
				if (velocity.x > 0)
					currentDirection = "right";
				else if (velocity.x < 0)
					currentDirection = "left";
			}

			// Point in a direction
			transform.Find("Sprite").localScale = new Vector3(1, 1, 1);
			if (currentDirection == "left")
			{
				weaponAnchor.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
				transform.Find("Sprite").localScale = new Vector3(-1, 1, 1);
			}
			else
			{
				weaponAnchor.rotation = Quaternion.Euler(Vector3.zero);
			}
		}
	}

	void Weapon()
	{
		// Switch weapon
		if (Input.GetKeyDown(KeyCode.Q))
		{
			// Hide weapons
			pistolWeapon.GetComponent<SpriteRenderer>().enabled = false;
			rifleWeapon.GetComponent<SpriteRenderer>().enabled = false;

			// Change weapon
			if (CurrentWeapon.name == "Pistol")
				CurrentWeapon = rifleWeapon;
			else if (CurrentWeapon.name == "Rifle")
				CurrentWeapon = pistolWeapon;

			// Show weapons
			CurrentWeapon.GetComponent<SpriteRenderer>().enabled = true;
		}

		if (circularAiming)
		{
			// Point weapon towards mouse
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 dist = mousePos - weaponAnchor.position;
			float angle = Mathf.Atan(dist.y / dist.x) * Mathf.Rad2Deg;
			if (dist.x < 0)
				angle = 180 + angle;
			weaponAnchor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

			// Fire weapon by mouse click
			if (Input.GetMouseButtonDown(0) && Time.time - lastWeaponShootTime >= CurrentWeapon.GetComponent<WeaponProperties>().delay)
				FireWeapon();
		}
		else
		{
			if (Time.time - lastWeaponShootTime >= CurrentWeapon.GetComponent<WeaponProperties>().delay)
			{
				// Point weapon in specified direction
				bool shoot = false;
				if (Input.GetKey(KeyCode.LeftArrow))
				{
					shoot = true;
					currentDirection = "left";
				}
				if (Input.GetKey(KeyCode.RightArrow))
				{
					shoot = true;
					currentDirection = "right";
				}

				if (shoot)
				{
					// Point in a direction
					transform.Find("Sprite").localScale = new Vector3(1, 1, 1);
					if (currentDirection == "left")
					{
						weaponAnchor.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
						transform.Find("Sprite").localScale = new Vector3(-1, 1, 1);
					}
					else
					{
						weaponAnchor.rotation = Quaternion.Euler(Vector3.zero);
					}

					// Fire weapon
					FireWeapon();
					lastWeaponShootTime = Time.time;
				}
			}
		}
	}

	void FireWeapon()
	{
		// Shake camera
		CameraShake.Kick(0.025f);

		// Text popup
		GameObject clonedPowText = Instantiate(bangPowTextPopup, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as GameObject;
		clonedPowText.transform.SetParent(GameManager.UserInterface);

		// Display bullet trail
		if (CurrentWeapon.name == "Pistol" || CurrentWeapon.name == "Rifle")
		{
			GameObject clonedBullet = Instantiate(bulletPrefab, CurrentWeapon.transform.Find("Nozzle").position, weaponAnchor.rotation) as GameObject;
		}

		// Play gunshot sound
		Camera.main.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(CurrentWeapon.GetComponent<WeaponProperties>().GunshotSound);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(velocity.x, velocity.y, 0));
	}
}