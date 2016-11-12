using UnityEngine;
using System.Collections;

public class BreakGlassOnTouch : MonoBehaviour
{
	public GameObject breakingPrefab;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			// Instantiate the glass breaking effect
			GameObject glassBreakClone = Instantiate(breakingPrefab, transform.position, Quaternion.identity) as GameObject;
		
			// Destroy this object
			Destroy(gameObject);
		}
	}
}
