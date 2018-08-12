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
	[SerializeField] private float drainStaminaQtd = 0.02f;

	private float directionX;
	private float directionY;

	private bool isMoving = false;

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
		else
		{
			maxSpeed = initSpeed;
		}
	}

	private void Update() {
		if(speed != 0 && !isMoving)
		{
			speed = Mathf.Lerp(speed,0,2*Time.deltaTime);
			if(speed < 0.2f)
			{
				 speed = 0;
			}
			speed = Mathf.Clamp(speed,0,10);
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

	IEnumerator Desacelerate(float direction)
	{
		while(speed != 0 && !isMoving)
		{

			yield return new WaitForSeconds(0.1f);
		}
		yield return null;
	}


}
