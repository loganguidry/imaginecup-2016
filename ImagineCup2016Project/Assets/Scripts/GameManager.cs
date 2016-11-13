using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public Transform UserInterface;
	static public GameObject Player;
	public float gravity;
	public float minX;

	static public float PlayerHealth = 100f;

	void Start ()
	{
		UserInterface = GameObject.Find("UI_Canvas").transform;
		Player = GameObject.Find("Player");
		PlayerHealth = 100f;
		PlayerController.MinimumX = minX;
		Physics.gravity = new Vector3(0, -gravity, 0);

		// Player should pass through enemies
		Physics2D.IgnoreLayerCollision(8, 9, true);

		// Enemies should pass through enemies
		Physics2D.IgnoreLayerCollision(9, 9, true);
	}

	static public void DamagePlayer(float amount)
	{
		PlayerHealth -= amount;

		if (PlayerHealth <= 0)
			Die();
	}

	static public void Die()
	{
		print("PLAYER DIED [GAMEMANAGER.CS]");
	}
}
