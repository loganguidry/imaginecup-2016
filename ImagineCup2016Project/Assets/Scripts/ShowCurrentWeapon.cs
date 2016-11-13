using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowCurrentWeapon : MonoBehaviour
{
	Text txt;

	void Start()
	{
		txt = GetComponent<Text>();
	}

	void Update()
	{
		if (GameManager.PlayerDead)
			txt.text = "";
		else
			txt.text = GameManager.Player.GetComponent<PlayerController>().CurrentWeapon.name;
	}
}
