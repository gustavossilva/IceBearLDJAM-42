using System;
using UnityEngine;

//Todas as funções com ligação no motor, ou seja, que tem influência direta na posição/velocidade do personagem

public class PlayerMotor : MonoBehaviour {

	public float speed = 5f;
	public float stamina = 100f;

	public Action<float> ChangeStamina;

	public void Movement(float x, float y)
	{
		transform.position += new Vector3(x,y,0) * (speed * Time.deltaTime);
	}

	public void Accelerate()
	{
		if(stamina > 0)
		{
			speed = 5 * 2;
			stamina -= 0.02f;
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

	public void SlowDown()
	{
		speed = 5f;
	}

	public void AddStamina(float value)
	{
		stamina += value;
		if(ChangeStamina != null)
		{
			ChangeStamina.Invoke(stamina);
		}
	}


}
