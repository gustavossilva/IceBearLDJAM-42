using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	public bool gameOver = false;
	public bool isBossAlive = true;

	public bool startBossBatle = false;

	public Transform topleft, bottomLeft;

	public int map;

	protected override void Awake() 
	{
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	private void Update() {
		if(isBossAlive && startBossBatle)
		{
			topleft.position = new Vector2(-6.7f,topleft.position.y);
			bottomLeft.position = new Vector2(-6.7f,bottomLeft.position.y);
		}
		else
		{
			topleft.position = new Vector2(-8f,topleft.position.y);
			bottomLeft.position = new Vector2(-8f,bottomLeft.position.y);			
		}
	}
}
