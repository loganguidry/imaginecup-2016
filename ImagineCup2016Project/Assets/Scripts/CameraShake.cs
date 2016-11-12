using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	static public Vector3 OriginalPosition;
	static public float Power = 0f;
	static public float MaxPower = 0.25f;
	public float damping = 0.95f;
	public float wobbleSpeed = 0.1f;
	float sineStep = 0f;

	void Start()
	{
		OriginalPosition = transform.position;
	}

	static public void Kick(float amount)
	{
		Power += amount;
		if (Power > MaxPower)
			Power = MaxPower;
	}

	void Update()
	{
		// Wobble
		sineStep += wobbleSpeed * Time.deltaTime;
		float sineValue = Mathf.Sin(sineStep);
		float cosineValue = Mathf.Cos(sineStep * 3);
		transform.position = OriginalPosition;
		transform.Translate(new Vector2(cosineValue * Power, sineValue * Power));

		// Decrease power
		Power *= damping;
	}
}
