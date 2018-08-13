using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInimigo : MonoBehaviour {

	public float spawnTime;
	private float currentTime;
	private ObjectPool pool;

	[SerializeField] private Transform maxTransformPos;
	[SerializeField] private Transform minTransformPos;

	public static Transform urso;
	private Vector2 minPos;
	private Vector2 maxPos;

	public static Dictionary<IcePosition, Tubarao> dic;

	// Use this for initialization
	void Start ()
	{
		pool = GetComponent<ObjectPool>();
		currentTime = 0f;
		urso = GameObject.Find("Spine GameObject (URSO)").transform;	

		dic = new Dictionary<IcePosition, Tubarao>();

		minPos = transform.TransformPoint(minTransformPos.position);
		maxPos = transform.TransformPoint(maxTransformPos.position);
	}
	
	void Update () 
	{
		// Only counts time when there is an available space to chew
		if(dic.Count < 4)
		{
			currentTime += Time.deltaTime;
			
			if(currentTime >= spawnTime)
			{
				currentTime = 0f;
				Spawn();

				// fazer com que quando o inimigo morrer, ele avisar q morreu pro spawner spawnar outro inimigo, depois de um delay aleatorio
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
