using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BossBattle : MonoBehaviour {

	public GameObject bossObstacles;

	public Transform bearTransform;

	public Transform initialPosition;

	private SkeletonAnimation skeletonAnimation;

	public AnimationReferenceAsset idle,attack;

	private float initialX = 7.12f;

	private bool intro= true;

	private bool attacking = false;

	private int life = 3;

	private float attackTimer = 0f;

	private bool animationIsRunning = false;

	// Use this for initialization
	void Start () {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.startBossBatle)
		{
			if(!bossObstacles.activeSelf)
			{
				bossObstacles.SetActive(true);
			}
			if(intro)
				transform.position = new Vector2(Mathf.Lerp(transform.position.x,initialX,5*Time.deltaTime),transform.position.y);
				if(transform.position.x < 7.2)
				{
					intro = false;
				}
			//enquanto espera pelo ataque, segue o Y do player.
			if(!attacking)
			{
				if(!animationIsRunning)
				{
					skeletonAnimation.AnimationState.SetAnimation(0,idle,true);
					animationIsRunning = true;
				}
				attackTimer += Time.deltaTime;
				transform.position = new Vector2(transform.position.x,bearTransform.position.y -1.24f);
			}
			else
			{
				if(animationIsRunning)
				{
					skeletonAnimation.AnimationState.SetAnimation(0,attack,true);
					animationIsRunning = false;
				}
				transform.position += Vector3.left *  (10*Time.deltaTime);
				if(transform.position.y > bearTransform.position.y)
				{
					GetComponent<MeshRenderer>().sortingLayerName = "Enemy";
				}
				else
				{
					GetComponent<MeshRenderer>().sortingLayerName = "Player";
				}
			}
			if(attackTimer > 3)
			{
				attacking = true;
			}
			if(life > 0 && transform.position.x < -13.38) //passou da tela e não tomou dano
			{
				transform.position = initialPosition.position;
				attacking = false;
				attackTimer = 0;
				intro = true;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Obstacle"))
		{
			life--;
			if(life == 0)
			{
				bossObstacles.GetComponent<Animator>().SetTrigger("final");
				gameObject.SetActive(false);
				GameManager.Instance.isBossAlive = false;
			}
		}
		if(other.CompareTag("Player"))
		{
			IceBehaviour iceToDamage = null;
			for(int i=0;i<IceController.Instance.iceScripts.Length;i++)
			{
				if(IceController.Instance.iceScripts[i] != null)
				{
					iceToDamage = IceController.Instance.iceScripts[i];
					break;
				}
			}
			if(iceToDamage!=null)
				IceController.Instance.TakeDamageByElement(-50,iceToDamage.MyPosition);
		}
		if(other.CompareTag("Player") || other.CompareTag("Obstacle"))
		{
			transform.position = initialPosition.position;
			attacking = false;
			attackTimer = 0;
			intro = true;
		}
	}

	//se o boss acertar o player, dar dano na integridade;

	//se o boss acertar um obstaculo, dar dano nele;

	//Se o boss morrer, desativbar os obstaculos, permitir o movimento do mapa

}
