using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstaculo : MonoBehaviour {
	[SerializeField] private Transform maxTransformPos;
	[SerializeField] private Transform minTransformPos;

	public float spawnTime;
	private float currentTime;
	private ObjectPool pool;
	private Vector2 minPos;
	private Vector2 maxPos;

	[SerializeField] private ParticleSystem ps;
	private bool startSnow = false;

	// Use this for initialization
	void Start () {
		pool = GetComponent<ObjectPool>();

		minPos = transform.TransformPoint(minTransformPos.position);
		maxPos = transform.TransformPoint(maxTransformPos.position);

		// DEBUG
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if(currentTime >= spawnTime && GameManager.Instance.map < 4)
		{
			currentTime = 0;
			Spawn();
		}

		if(GameManager.Instance.map == 5)
		{
			if(!startSnow)
			{
				startSnow = true;
				ps.Play();
			}
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
