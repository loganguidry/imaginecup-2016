﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	static public float MinimumX;
	public float acceleration;
	public float jumpPower;
	public GameObject bangPowTextPopup;
	public GameObject bulletPrefab;
	Vector2 velocity = Vector2.zero;
	Transform weaponAnchor;
	Transform userInterface;

	GameObject pistolWeapon;
	GameObject rifleWeapon;
	GameObject currentWeapon;

	void Start()
	{
		weaponAnchor = transform.Find("WeaponAnchor");
		userInterface = GameObject.Find("UI_Canvas").transform;

		// Weapons
		pistolWeapon = weaponAnchor.Find("Pistol").gameObject;
		rifleWeapon = weaponAnchor.Find("Rifle").gameObject;
		rifleWeapon.GetComponent<SpriteRenderer>().enabled = false;
		currentWeapon = pistolWeapon;
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
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
			velocity += new Vector2(0, -velocity.y + jumpPower);

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
		// Shake camera
		CameraShake.Kick(0.05f);

		// Text popup
		GameObject clonedPowText = Instantiate(bangPowTextPopup, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as GameObject;
		clonedPowText.transform.SetParent(userInterface);

		// Display bullet trail
		if (currentWeapon.name == "Pistol" || currentWeapon.name == "Rifle")
		{
			GameObject clonedBullet = Instantiate(bulletPrefab, currentWeapon.transform.Find("Nozzle").position, weaponAnchor.rotation) as GameObject;
		}

		// Play gunshot sound
		Camera.main.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(currentWeapon.GetComponent<WeaponProperties>().GunshotSound);
	
		// Fire a raycast to hit objects
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(velocity.x, velocity.y, 0));
	}
}