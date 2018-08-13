using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	public bool gameOver = false;
	public bool winner = false;
	public bool isBossAlive = true;
	public bool startBossBatle = false;
	public Transform topleft, bottomLeft;
	public int map;
	public SkeletonAnimation filhoAnimation;
	public AnimationReferenceAsset filhoFeliz,filhoTriste;

	private float gameOverTimer = 0;

	private bool isFilhoSad = true;

	protected override void Awake() 
	{
		IsPersistentBetweenScenes = false;
		base.Awake();
	}
	private void Start() {
		filhoAnimation.AnimationState.SetAnimation(0,filhoTriste,true);
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
		if(gameOver)
		{
			gameOverTimer += Time.deltaTime;
			if(gameOverTimer > 2 && !winner)
			{
				SceneManager.LoadScene("SceneGameOver");
			}
			else if(gameOverTimer > 2 && winner)
			{
				SceneManager.LoadScene("SceneFinal");
			}
		}
	}

	public void UpdateFilhoAnimation()
	{
		if(isFilhoSad)
		{
			isFilhoSad = false;
			filhoAnimation.AnimationState.SetAnimation(0,filhoFeliz,true);
		}
	}
}
