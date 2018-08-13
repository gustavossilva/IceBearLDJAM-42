using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	public bool gameOver = false;
	public bool isBossAlive = true;

	protected override void Awake() 
	{
		IsPersistentBetweenScenes = false;
		base.Awake();
	}
}
