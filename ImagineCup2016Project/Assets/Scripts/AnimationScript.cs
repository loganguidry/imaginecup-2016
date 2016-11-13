﻿using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{
	public int fps = 30;
	public bool playForward = true;
	public string currentAnim = "Enemy";
	public Sprite[] playerPistol = new Sprite[10];
	public Sprite playerPistolStill;
	public Sprite[] playerRifle = new Sprite[10];
	public Sprite playerRifleStill;
	public Sprite[] enemy = new Sprite[10];

	void Update()
	{
		// Change frame
		if (currentAnim == "Player Pistol")
		{
			if (GameManager.Player.GetComponent<PlayerController>().velocity.magnitude <= 0.01f)
			{
				// Still frame
				GetComponent<SpriteRenderer>().sprite = playerPistolStill;
			}
			else
			{
				// Animate
				int index = Mathf.FloorToInt((Time.time * fps) % playerPistol.Length);
				if (!playForward)
					index = playerPistol.Length - 1 - index;
				print(index);
				GetComponent<SpriteRenderer>().sprite = playerPistol[index];
			}
		}
		else if (currentAnim == "Player Rifle")
		{
			if (GameManager.Player.GetComponent<PlayerController>().velocity.magnitude <= 0.01f)
			{
				// Still frame
				GetComponent<SpriteRenderer>().sprite = playerRifleStill;
			}
			else
			{
				// Animate
				int index = Mathf.FloorToInt((Time.time * fps) % playerRifle.Length);
				GetComponent<SpriteRenderer>().sprite = playerRifle[index];
			}
		}
		else if (currentAnim == "Enemy")
		{
			int index = Mathf.FloorToInt((Time.time * fps) % enemy.Length);
			GetComponent<SpriteRenderer>().sprite = enemy[index];
		}
		else
			print("Not a valid animation name in AnimationScript.cs");
	}
}
