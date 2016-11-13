using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	static public Transform UserInterface;
	static public GameObject Player;
	static public bool PlayerDead = false;
	static public bool PlayerWon = false;
	public float gravity;
	public float minX;
	public float maxX;

	public GameObject damageIndicatorPrefab;
	static public GameObject DamageIndicatorPrefab;

	static public float PlayerHealth = 100f;

	static public Transform[] Hearts = new Transform[3];

	static public List<GameObject> Enemies = new List<GameObject>();

	static public GameObject go_Screen;

	void Start()
	{
		UserInterface = GameObject.Find("UI_Canvas").transform;
		Player = GameObject.Find("Player");
		PlayerDead = false;
		PlayerWon = false;
		Physics.gravity = new Vector3(0, -gravity, 0);
		PlayerController.MinimumX = minX;
		PlayerController.MaximumX = maxX;
		DamageIndicatorPrefab = damageIndicatorPrefab;
		PlayerHealth = 100f;

		// Reference health display hearts
		for(int i = 0; i < 3; i++)
			Hearts[i] = UserInterface.Find("HealthIndicator").GetChild(i);

		// Get each enemy
		foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
		{
			if (obj.name.StartsWith("Enemy"))
				Enemies.Add(obj);
		}

		// Gameover screen
		go_Screen = GameObject.Find("GameOverScreen_GO");
		go_Screen.SetActive(false);

		// Player should pass through enemies
		Physics2D.IgnoreLayerCollision(8, 9, true);

		// Enemies should pass through enemies
		Physics2D.IgnoreLayerCollision(9, 9, true);
	}

	void Update()
	{
		// Win if all enemies are dead
		bool allDead = true;
		foreach (GameObject obj in Enemies)
		{
			try
			{
				if (obj.GetComponent<EnemyManager>().isAlive)
					allDead = false;
			} catch {}
		}
		if (allDead)
			Win();
	}

	static public void DamagePlayer(float amount)
	{
		// Player is already dead
		if (PlayerDead)
			return;

		// Take away health
		PlayerHealth -= amount;

		// Create a text popup
		GameObject diClone = Instantiate(
			DamageIndicatorPrefab,
			Player.transform.position + new Vector3(
				Random.Range(-0.2f, 0.2f),
				0.5f + Random.Range(0f, 0.2f),
				0
			),
			Quaternion.identity//Quaternion.Euler(new Vector3(0, 0, Random.Range(-15f, 15f)))
		) as GameObject;
		//diClone.GetComponent<TextMesh>().text = "-" + Mathf.FloorToInt(amount).ToString();

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
		go_Screen.SetActive(true);

		// Text display
		GameObject.Find("GameOverHeaderText").GetComponent<Text>().text = "Game Over!";
		GameObject.Find("GameOverHeaderText").GetComponent<Text>().color = new Color(1f, 0.6f, 0.4f);
	}

	static public void Win()
	{
		print("Player won [GameManager.cs - Win()]");
		PlayerWon = true;
		go_Screen.SetActive(true);

		// Text display
		GameObject.Find("GameOverHeaderText").GetComponent<Text>().text = "You Win!";
		GameObject.Find("GameOverHeaderText").GetComponent<Text>().color = new Color(0.25f, 1f, 0.75f);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene("level1");
	}

	public void GoHome()
	{
		SceneManager.LoadScene("mainMenu");
	}
}
