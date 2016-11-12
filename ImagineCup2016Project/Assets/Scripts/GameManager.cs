using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public GameObject Player;
	public float gravity;
	public float minX;

	void Start ()
	{
		Player = GameObject.Find("Player");
		PlayerController.MinimumX = minX;
		Physics.gravity = new Vector3(0, -gravity, 0);
	}
}
