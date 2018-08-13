using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
//Todas as funções q controlam o personagem de alguma maneira

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	//Verificação de posições
	public Transform topLeft;
	public Transform topRight;
	public Transform bottomLeft;
	public Transform bottomRight;

	//Motor
	private PlayerMotor motor;
	private Vector2 clampPosition = Vector2.zero;
	private float xMovement = 0;
	private float yMovement = 0;

	//Indicates the last direction pressed
	private float lastDirectionX;
	private float lastDirectionY;

	//Check if the user is pressing the boost button
	private bool isPressingShift = false;

	//Animation controllers
	bool isMoving = false;
	bool canDoAnimationAgain = true;
	public SkeletonAnimation _skeletonAnimation;
	public AnimationReferenceAsset idle,remar,morrer;

	//Audio controllers
	private AudioSource rowingSFX;
	private float audioPitch = 1;

	private bool isGameOver = false;

	void Start ()
	{
		_skeletonAnimation.AnimationState.SetAnimation(0,idle,true);
		motor = GetComponent<PlayerMotor>();
		rowingSFX = GetComponent<AudioSource>();
	}
	
	
	void Update () 
	{
		if(GameManager.Instance.gameOver)
		{
			if(!isGameOver && !GameManager.Instance.winner)
			{
				_skeletonAnimation.AnimationState.AddEmptyAnimation(1,0,0);
				_skeletonAnimation.AnimationState.AddEmptyAnimation(2,0,0);
				_skeletonAnimation.AnimationState.SetAnimation(0,morrer,false);
				isGameOver = true;
			}
			return;
		}
		if(!GameManager.Instance.isBossAlive)
		{
			transform.position = new Vector2(Mathf.Lerp(transform.position.x,-6.7f,3*Time.deltaTime),Mathf.Lerp(transform.position.y,-1.37f,3*Time.deltaTime));
			return;
		}
		//Ler um comando e adicionar e avisar o personagem que deve se mover
		xMovement = Input.GetAxisRaw("Horizontal");
		yMovement = Input.GetAxisRaw("Vertical");

		//Checa e faz o movimento
		if(xMovement != 0 || yMovement != 0)
		{
			//Animation routines
			if(!isMoving && canDoAnimationAgain && !PlayerManager.Instance.attacking)
			{
				isMoving = true;
				StartCoroutine(MovementAnimation());
			}
			else if(PlayerManager.Instance.attacking)
			{
				isMoving = false;
				_skeletonAnimation.AnimationState.AddEmptyAnimation(1,0f,0.5f);
				StopAllCoroutines();
			}

			//Movements routines
			lastDirectionX = xMovement;
			lastDirectionY = yMovement;
			//Faz o motor mover o personagem
			motor.Movement(xMovement,yMovement);

			//Se o shift for pressionado, altera a velocidade do som e acelera o personagem
			if(isPressingShift)
			{
				if(xMovement > 0)
				{
					GameManager.Instance.UpdateSpeed();
				}
				audioPitch = 1.4f;
				motor.Accelerate();
			}
			else
			{
				GameManager.Instance.SpeedToInit();
				audioPitch = 1;
			}
		}
		else
		{
			//Para o movimento e desliga
			_skeletonAnimation.AnimationState.AddEmptyAnimation(1,.5f,1f);
			StopAllCoroutines();
			canDoAnimationAgain = true;
			isMoving = false;
			motor.StopMovement(lastDirectionX,lastDirectionY);
			//idle animation
		}

		//Checa se esta sendo movimento com boost
		if(Input.GetKey(KeyCode.LeftShift))
		{
			if(!isPressingShift)
			{
				isPressingShift = true;
			}
		}
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			motor.StopAcceleration();
			isPressingShift = false;
		}

		//Limita a posição do personagem na tela
		clampPosition.Set(Mathf.Clamp(transform.position.x,topLeft.position.x,topRight.position.x),
									Mathf.Clamp(transform.position.y,bottomLeft.position.y,topLeft.position.y));

		transform.position = clampPosition;
		
	}

	//Do the rowing animation and play the sound
	IEnumerator MovementAnimation()
	{
		_skeletonAnimation.AnimationState.AddEmptyAnimation(1,1f,0.1f);
		while(isMoving)
		{
			if(PlayerManager.Instance.attacking)
				break;
			Spine.TrackEntry teste = _skeletonAnimation.AnimationState.SetAnimation(1,remar,false);
			teste.timeScale = audioPitch;
			rowingSFX.pitch = audioPitch;
			rowingSFX.Play();
			yield return new WaitForSeconds(rowingSFX.clip.length - (rowingSFX.clip.length * (audioPitch-1)));
		}
	}
}
