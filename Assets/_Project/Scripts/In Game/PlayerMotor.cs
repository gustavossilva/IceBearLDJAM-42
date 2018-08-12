using System;
using System.Collections;
using UnityEngine;

//Todas as funções com ligação no motor, ou seja, que tem influência direta na posição/velocidade do personagem

public class PlayerMotor : MonoBehaviour {

	//Event called when stamina changes
	public Action<float> ChangeStamina;

	//Speed variables
	private float initSpeed;
	[SerializeField] private float maxSpeed = 5f;
	[SerializeField] private float speed = 0;
	[SerializeField] private float boostSpeedModifier = 2f;

	//Stamina
	[SerializeField] private float stamina = 100f;
	[SerializeField] private float drainStaminaQtd = 1f;

	private float directionX;
	private float directionY;

	private bool isMoving = false;
	private bool wasAccelerate = false;
	

	
	private void Start() 
	{
		initSpeed = maxSpeed;
	}

	public void Movement(float x, float y)
	{
		isMoving = true;
		StopAllCoroutines();
		speed += 5 * Time.deltaTime;
		speed = Mathf.Clamp(speed,0,maxSpeed);
		transform.position += new Vector3(x,y,0) * (speed * Time.deltaTime);
	}

	public void StopMovement(float _directionX, float _directionY)
	{
		isMoving = false;
		directionX = _directionX;
		directionY = _directionY;
	}

	/// <summary>
	/// Altera a velocidade do personagem e utiliza a stamina dele
	/// </summary>
	public void Accelerate()
	{
		if(stamina > 0)
		{
			maxSpeed = initSpeed * boostSpeedModifier;
			stamina -= drainStaminaQtd;
			stamina = Mathf.Clamp(stamina,0,100);

			if(ChangeStamina != null)
			{
				ChangeStamina.Invoke(stamina);
			}
		}
	}

	public void StopAcceleration()
	{
		wasAccelerate = true;
	}

	private void Update() {
		if(wasAccelerate)
		{
			maxSpeed = Mathf.Lerp(maxSpeed,5,3*Time.deltaTime);
			if(maxSpeed < 5.1)
			{
				maxSpeed = 5;
				wasAccelerate = false;
			}
		}
		if(speed != 0 && !isMoving)
		{
			speed = Mathf.Lerp(speed,0,3*Time.deltaTime);
			if(speed < 0.2f)
			{
				 speed = 0;
			}
			speed = Mathf.Clamp(speed,0,initSpeed * boostSpeedModifier);
			transform.position += new Vector3(directionX,directionY,0) * (speed * Time.deltaTime);
		}
		
	}

	/// <summary>
	/// Adiciona uma certa quantidade a sua stamina atual
	/// </summary>
	/// <param name="value">Qunatidade que será adicionada</param>
	public void AddStamina(float value)
	{
		stamina += value;
		if(ChangeStamina != null)
		{
			ChangeStamina.Invoke(stamina);
		}
	}
}
