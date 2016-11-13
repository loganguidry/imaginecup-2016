using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowCurrentWeapon : MonoBehaviour
{
	Text txt;
	Text hintTxt;

	void Start()
	{
		txt = GetComponent<Text>();
		hintTxt = transform.GetChild(1).GetComponent<Text>();
	}

	void Update()
	{
		if (GameManager.PlayerDead || GameManager.PlayerWon)
		{
			txt.text = "";
			hintTxt.text = "";
			GetComponentInChildren<Image>().enabled = false;
		}
		else
		{
			txt.text = GameManager.Player.GetComponent<PlayerController>().CurrentWeapon.name;
			GetComponentInChildren<Image>().sprite = GameManager.Player.GetComponent<PlayerController>().CurrentWeapon.GetComponent<SpriteRenderer>().sprite;
		}
	}
}
