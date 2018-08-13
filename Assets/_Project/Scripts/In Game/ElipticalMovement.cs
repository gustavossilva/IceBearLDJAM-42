using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElipticalMovement : MonoBehaviour {
	
	public float a = 2f;
	public float b = 1f;

	[HideInInspector] public float alpha;
 
	float X, Y;

	public float surroundingTime = 4f;
	[HideInInspector] public float currentSurroundingTime;
	[HideInInspector] public Animator anim;
	[HideInInspector] public GameObject[] children;


	void Awake()
	{
		anim = GetComponent<Animator>();
		children = new GameObject[transform.childCount];

		for (int i = 0; i < children.Length; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].SetActive(true);
		}
	}

	void OnEnable()
	{
		anim.enabled = true;
		for (int i = 0; i < children.Length; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].SetActive(true);
		}
	}

	void OnDisable()
	{
		anim.enabled = false;
	}

 	// public void Move (Tubarao tubarao, float velocidadeRodeio, float centerX, float centerY) 
	// {
	// 	alpha += velocidadeRodeio;

	// 	X = centerX + (a * Mathf.Cos(alpha));
	// 	Y = centerY + (b * Mathf.Sin(alpha));

	// 	// Flips image when it reaches the max points of the ellipse
	// 	if(X - centerX <= -a + 0.1f && tubarao.isFacingLeft)
	// 		tubarao.Flip();
	// 	else if(X - centerX >= a - 0.1f && !tubarao.isFacingLeft)
	// 		tubarao.Flip();

	// 	// Set as the new position
	// 	tubarao.rb2D.position = new Vector3(X,Y);
 	// }

	public IEnumerator Move (Tubarao tubarao, float velocidadeRodeio) 
	{
		currentSurroundingTime = 0f;
		
		while(currentSurroundingTime <= surroundingTime)
		{
			float centerX = tubarao.urso.position.x;
			float centerY = tubarao.urso.position.y;

			alpha += (velocidadeRodeio * Time.deltaTime);

			X = centerX + (a * Mathf.Cos(alpha));
			Y = centerY + (b * Mathf.Sin(alpha));

			// Flips image when it reaches the max points of the ellipse
			if(X - centerX <= -a + 0.1f && tubarao.isFacingLeft)
				tubarao.Flip();
			else if(X - centerX >= a - 0.1f && !tubarao.isFacingLeft)
				tubarao.Flip();

			// Set as the new position
			tubarao.rb2D.position = new Vector3(X,Y);

			currentSurroundingTime += Time.deltaTime;

			yield return null;
		}

		for (int i = 0; i < children.Length; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].SetActive(false);
		}

		IcePosition position = tubarao.Attack();

		float currentChewingTime = 0f;

		while(currentChewingTime <= tubarao.chewingTime)
		{
			currentChewingTime += Time.deltaTime;
			// Apply damage to the platform
			IceController.Instance.TakeDamageByElement(-(tubarao.damagePerSecond * (int)currentChewingTime), position);
			yield return null;
		}

		// If the player has not killed the shark yet, e.g. it is still alive
		if(tubarao.isAlive)
		{
			IceController.Instance.StopSharkAnim(position);
			gameObject.SetActive(false);
		}
 	}
}
