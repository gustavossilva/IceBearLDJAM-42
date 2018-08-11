using System;
using UnityEngine;

//Todas as funções com ligação no motor, ou seja, que tem influência direta na posição/velocidade do personagem

public class PlayerMotor : MonoBehaviour {

	//Event called when stamina changes
	public Action<float> ChangeStamina;

	//Speed variables
	private float initSpeed;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float boostSpeedModifier = 2f;

	//Stamina
	[SerializeField] private float stamina = 100f;
	[SerializeField] private float drainStaminaQtd = 0.02f;

	private void Start() 
	{
		initSpeed = speed;
	}

	public void Movement(float x, float y)
	{
		transform.position += new Vector3(x,y,0) * (speed * Time.deltaTime);
	}

	/// <summary>
	/// Altera a velocidade do personagem e utiliza a stamina dele
	/// </summary>
	public void Accelerate()
	{
		if(stamina > 0)
		{
			speed = initSpeed * 2;
			stamina -= drainStaminaQtd;
			stamina = Mathf.Clamp(stamina,0,100);

			if(ChangeStamina != null)
			{
				ChangeStamina.Invoke(stamina);
			}
		}
		else
		{
			SlowDown();
		}
	}

	/// <summary>
	/// Volta a velocidade do personagem a inicial
	/// </summary>
	public void SlowDown()
	{
		speed = initSpeed;
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
