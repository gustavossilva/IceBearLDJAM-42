using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private float lastDirectionX;
	private float lastDirectionY;

	private bool isPressingShift = false;

	void Start ()
	{
		motor = GetComponent<PlayerMotor>();
	}
	
	
	void Update () 
	{
		//Ler um comando e adicionar e avisar o personagem que deve se mover
		xMovement = Input.GetAxisRaw("Horizontal");
		yMovement = Input.GetAxisRaw("Vertical");

		//Checa e faz o movimento
		if(xMovement != 0 || yMovement != 0)
		{
			lastDirectionX = xMovement;
			lastDirectionY = yMovement;
			//Faz o motor mover o personagem
			motor.Movement(xMovement,yMovement);
			if(isPressingShift)
			{
				motor.Accelerate();
			}
			//Else desalecerate and change speed to 5;
		}
		else
		{
			motor.StopMovement(lastDirectionX,lastDirectionY);
			//idle animation
		}

		//Checa se esta sendo movimento com boost
		if(Input.GetKey(KeyCode.LeftShift))
		{
			if(!isPressingShift)
			{
				//Mudar animação
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
}
