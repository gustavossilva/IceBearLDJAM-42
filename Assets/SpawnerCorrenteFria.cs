using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCorrenteFria : MonoBehaviour {

	[SerializeField] private Transform maxTransformPos;
	[SerializeField] private Transform minTransformPos;

	public float spawnTime;
	private float currentTime;
	private ObjectPool pool;
	private Vector2 minPos;
	private Vector2 maxPos;


	// Use this for initialization
	void Start () {
		pool = GetComponent<ObjectPool>();

		minPos = transform.TransformPoint(minTransformPos.position);
		maxPos = transform.TransformPoint(maxTransformPos.position);
	}

	void Update () {
		currentTime += Time.deltaTime;

		if(currentTime >= spawnTime)
		{
			currentTime = 0;
			Spawn();
		}
	}

	void Spawn()
	{
		GameObject obstacleGO = pool.GetPooledObject();

		// Coloca o game object em um local entre maxPos e minPos
		obstacleGO.transform.position = new Vector2(maxPos.x, Random.Range(minPos.y, maxPos.y));

		// Ativa gameobject
		obstacleGO.SetActive(true);
	}
}
