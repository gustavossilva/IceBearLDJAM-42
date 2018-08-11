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

	void Start ()
	{
		motor = GetComponent<PlayerMotor>();
	}
	
	
	void Update () 
	{
		//Ler um comando e adicionar e avisar o personagem que deve se mover
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		if(x != 0 || y != 0)
		{
			//Faz o motor mover o personagem
			motor.Movement(x,y);
		}

		if(Input.GetKey(KeyCode.LeftShift))
		{
			motor.Accelerate();
		}
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			motor.SlowDown();
		}

		//Limita a posição do personagem na tela
		Vector2 limitMovement;
		limitMovement = new Vector2(Mathf.Clamp(transform.position.x,topLeft.position.x,topRight.position.x),
									Mathf.Clamp(transform.position.y,bottomLeft.position.y,topLeft.position.y));
		transform.position = limitMovement;
		
	}
}
