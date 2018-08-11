using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

	public Slider staminaSlider;

	protected override void Awake()
	{
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	private void Start() 
	{
		PlayerManager.Instance.playerMotor.ChangeStamina += ChangeUIStamina;
	}


	void ChangeUIStamina(float qtd)
	{
		staminaSlider.value = qtd;
	}
}
