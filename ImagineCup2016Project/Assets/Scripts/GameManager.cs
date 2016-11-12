using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public GameObject Player;
	public float gravity;

	void Start ()
	{
		Player = GameObject.Find("Player");
		PlayerController.MinimumX = Player.transform.position.x;
		Physics.gravity = new Vector3(0, -gravity, 0);
	}
}
