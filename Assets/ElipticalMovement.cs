using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElipticalMovement : MonoBehaviour {
	
	public float a = 2f;
	public float b = 1f;

	public float alpha;
 
	float X, Y;

 	public void Move (float velocidadeRodeio, float centerX, float centerY) 
	{
		alpha += velocidadeRodeio;

		X = centerX + (a * Mathf.Cos(alpha));
		Y = centerY + (b * Mathf.Sin(alpha));

		transform.position = new Vector3(X,Y);
 	}
}
