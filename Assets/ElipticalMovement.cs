using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElipticalMovement : MonoBehaviour {
	
	public float a = 2f;
	public float b = 1f;

	public float alpha;
 
	float X, Y;

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
}
