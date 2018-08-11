using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

	public GameObject player;
	public PlayerMotor playerMotor;
	public PlayerController playerController;

	protected override void Awake()
	{
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	private void Start() 
	{
		playerMotor = player.GetComponent<PlayerMotor>();
		playerController = player.GetComponent<PlayerController>();
	}

}
