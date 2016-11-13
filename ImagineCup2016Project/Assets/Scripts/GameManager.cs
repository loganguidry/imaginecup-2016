using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public Transform UserInterface;
	static public GameObject Player;
	static public bool PlayerDead = false;
	public float gravity;
	public float minX;
	public float maxX;

	static public float PlayerHealth = 100f;

	static public Transform[] Hearts = new Transform[3];

	void Start ()
	{
		UserInterface = GameObject.Find("UI_Canvas").transform;
		Player = GameObject.Find("Player");
		PlayerHealth = 100f;
		PlayerController.MinimumX = minX;
		PlayerController.MaximumX = maxX;
		Physics.gravity = new Vector3(0, -gravity, 0);

		// Player should pass through enemies
		Physics2D.IgnoreLayerCollision(8, 9, true);

		// Enemies should pass through enemies
		Physics2D.IgnoreLayerCollision(9, 9, true);

		// Reference health display hearts
		for(int i = 0; i < 3; i++)
			Hearts[i] = UserInterface.Find("HealthIndicator").GetChild(i);
	}

	static public void DamagePlayer(float amount)
	{
		// Player is already dead
		if (PlayerDead)
			return;

		// Take away health
		PlayerHealth -= amount;

		// No more health
		if (PlayerHealth <= 0)
			Die();

		// Display new health
		for(int i = 0; i < 3; i++)
		{
			
			float newVal = 1f;
			/*
			if (i == 0)
				newVal = PlayerHealth / 25f;
			else if (i == 1)
				newVal = (PlayerHealth - 25) / 25f;
			else if (i == 2)
				newVal = (PlayerHealth - 50) / 25f;
			else if (i == 3)
				newVal = (PlayerHealth - 75) / 25f;
			*/
			if (i == 0)
				newVal = PlayerHealth / (1/3f*100);
			else if (i == 1)
				newVal = (PlayerHealth - (1/3f*100)) / (1/3f*100);
			else if (i == 2)
				newVal = (PlayerHealth - (1/3f*200)) / (1/3f*100);
			float newFill = Mathf.Min(1f, Mathf.Max(0f, newVal));
			Hearts[i].GetComponent<Image>().fillAmount = newFill;
		}
	}

	static public void Die()
	{
		print("Player died [GameManager.cs - Die()]");
		PlayerDead = true;

		// Text display
		GameObject.Find("GameOverHeaderText").GetComponent<Text>().text = "Game Over!";
		GameObject.Find("GameOverHeaderText").GetComponent<Text>().color = Color.red;
	}
}
