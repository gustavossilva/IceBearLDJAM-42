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

 	public void Move (Tubarao tubarao, float velocidadeRodeio, float centerX, float centerY) 
	{
		alpha += velocidadeRodeio;

		X = centerX + (a * Mathf.Cos(alpha));
		Y = centerY + (b * Mathf.Sin(alpha));

		// Flips image when it reaches the max points of the ellipse
		if(X - centerX <= -a + 0.1f && tubarao.isFacingLeft)
			tubarao.Flip();
		else if(X - centerX >= a - 0.1f && !tubarao.isFacingLeft)
			tubarao.Flip();

		// Set as the new position
		tubarao.rb2D.position = new Vector3(X,Y);
 	}

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

		gameObject.SetActive(false);
 	}
}
