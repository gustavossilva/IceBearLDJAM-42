using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeSliderController : MonoBehaviour {

	[SerializeField] GameObject iceController;
	[SerializeField] public IceBehaviour[] iceScripts;
	[SerializeField] private Slider iceLifeSlider;
	int totalLife; 
	// Use this for initialization
	void Start () {
		iceScripts = iceController.GetComponentsInChildren<IceBehaviour> ();
		for (int i = 0; i < iceScripts.Length; i++) {
			iceScripts[i].DamageTaken += updateSliderValue;
		}
	}

	void updateSliderValue () {
		totalLife = 0;
		for(int i =0; i< iceScripts.Length;i++){
			totalLife += iceScripts[i].Life;
		}
		iceLifeSlider.value = totalLife/5;
	}

}