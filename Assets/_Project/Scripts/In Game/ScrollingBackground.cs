using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public int map = 1;

	private float newMap = 0f;
	private int lastMap = 5;
	private float speed = 5f;

	// Use this for initialization
	void Start () {
		//0 - -38.4 em 60s (1 minuto por fase)
		GameManager.Instance.map = map;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.gameOver)
		{
			return;
		}
		//check if there is a game over
		//Init of map - 20.6 to get the final pos
		//Check boss fight
		transform.position += Vector3.left *(speed * Time.deltaTime);
		newMap = transform.position.x;
		if(newMap < -38.4 * map)
		{
			Debug.Log("new map start");
			map++;
			GameManager.Instance.map = map;
			//Start new map
		}
		if(map == 5 && GameManager.Instance.isBossAlive)
		{
			speed = 0; //boss fight
		}
		else if(map == 5 && !GameManager.Instance.isBossAlive)
		{
			speed = 5f;
		}
		if(transform.position.x < -173.2) //(-38.4f * (lastMap-1) - 20.6f)
		{
			speed = 0;
		}
	}
}
