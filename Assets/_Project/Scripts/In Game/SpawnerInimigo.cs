using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInimigo : MonoBehaviour {

	public float spawnTime;
	private float currentTime;
	private ObjectPool pool;

	// Use this for initialization
	void Start ()
	{
		pool = GetComponent<ObjectPool>();
		currentTime = 0f;	
	}
	
	void Update () 
	{
		currentTime += Time.deltaTime;

		if(currentTime >= spawnTime)
		{
			currentTime = 0f;
			Spawn();
		}
	}

	void Spawn()
	{
		
	}

}
