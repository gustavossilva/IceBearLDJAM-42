using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class FishingSystem : MonoBehaviour {

	[SerializeField] private float _timeToMaxHookChance; // A chance máxima de pescar um peixe

	[SerializeField] private float _currentTimeWithoutFish = 0; // A chance de pegar um peixe aumenta de acordo com o tempo passado.

	[SerializeField] private float _timeWithFish = 0; //Quando o peixe estiver na vara, representa o tempo em que o peixe se encontra nela, qaunto maior o tempo, maior a chance de perder o peixe

	[SerializeField] private int _probabilityToCatch;
	
	[SerializeField] private SkeletonAnimation rodAnimation;
	private bool _fishIn = false;

	// Update is called once per frame
	void Update () {
		if (!_fishIn) {
			if (_currentTimeWithoutFish < _timeToMaxHookChance)
				_currentTimeWithoutFish += Time.deltaTime;
			if (FishingRoulette () <= _probabilityToCatch + _currentTimeWithoutFish) {
				_fishIn = true;
				rodAnimation.AnimationState.SetAnimation(0,"Fisgada",true);
				//Disparar animação da vara em loop
			}
		}
		if (_fishIn) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				ResetSystem ();
				
				//Dispara animação pegando o peixe
				//preenche a barra de estamina.
				//Delay para voltar o objeto
			}
			_timeWithFish += Time.deltaTime;
			if (FishingRoulette () <= _timeWithFish) {
				ResetSystem ();
				//Dispara som de perder o peixe
				rodAnimation.AnimationState.SetAnimation(0,"Perder peixe",false);
				rodAnimation.AnimationState.AddAnimation(0,"Pescar",false,0);
				//Dispara animação da vara perdendno o peixe
			}
		}
	}

	void ResetSystem () {
		_timeWithFish = 0;
		_currentTimeWithoutFish = 0;
		_fishIn = false;
	}

	int FishingRoulette () {
		return Random.Range (0, 100);
	}
}