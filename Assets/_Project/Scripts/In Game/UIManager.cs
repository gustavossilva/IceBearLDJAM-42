using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

	public Slider staminaSlider;

	//Background to track
	public Transform scrollingBackground;

	//Track Images
	public Image bearTrackImage;

	//Track positions
	public RectTransform[] trackPositions = new RectTransform[6];
	public RectTransform startPosition;
	public RectTransform endPosition;
	/*public RectTransform position2;
	public RectTransform position3;
	public RectTransform position4;
	public RectTransform position5;
	*/

	public float newTrackPos;
	public float distance;
	Vector2 newPos = Vector2.zero;

	protected override void Awake()
	{
		distance = -0.7f-173.2f;
		IsPersistentBetweenScenes = false;
		base.Awake();
	}

	private void Start() 
	{
		PlayerManager.Instance.playerMotor.ChangeStamina += ChangeUIStamina;
	}
	private void Update() {
		//-430 - 0
		//420 - 2000
		//850 = pos inicial + pos final (distancia)
		newTrackPos = (((scrollingBackground.position.x * (Mathf.Abs(startPosition.anchoredPosition.x) + Mathf.Abs(endPosition.anchoredPosition.x)))/distance));
		newPos.Set(startPosition.anchoredPosition.x + newTrackPos,bearTrackImage.rectTransform.anchoredPosition.y);
		//430 = posinicial
		bearTrackImage.rectTransform.anchoredPosition = newPos;
	}


	void ChangeUIStamina(float qtd)
	{
		staminaSlider.value = qtd;
	}
}
