using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	private float newMap = 0f;
	private int map = 1;
	private float speed = 5f;

	// Use this for initialization
	void Start () {
		//0 - -38.4 em 60s (1 minuto por fase)
	}
	
	// Update is called once per frame
	void Update () {
		//check if there is a game over

		//Check boss fight
		transform.position += Vector3.left *(speed * Time.deltaTime);
		newMap = transform.position.x;
		if(newMap < -38.4 * map)
		{
			Debug.Log("new map start");
			map++;
			if(map == 5)
			{
				speed = 0; //boss fight
			}
			//Start new map
		}
	}
}
