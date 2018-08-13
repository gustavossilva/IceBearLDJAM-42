using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSortingLayer : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	private string obstacleLayerName = "Obstacle";
	private string midgroundLayerName = "Midground";

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if(SpawnerInimigo.urso.position.y > transform.parent.position.y)
		{
			spriteRenderer.sortingLayerName = obstacleLayerName;
		}
		else
		{
			spriteRenderer.sortingLayerName = midgroundLayerName;
		}
	}
}
