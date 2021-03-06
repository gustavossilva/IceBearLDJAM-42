﻿using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class IceBehaviour : MonoBehaviour {

	IceController controlRef;

	[SerializeField] private int life = 200;

	public int Life {
		private set { }
		get {
			return this.life;
		}
	}

	[SerializeField] IcePosition myPosition;

	[SerializeField] SkeletonAnimation iceSkeleton;

	public IcePosition MyPosition {
		private set { }
		get {
			return this.myPosition;
		}
	}

	public Action DamageTaken;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake () {
		controlRef = IceController.Instance;
	}

	private void Start () {
		this.life = 200;
		StartCoroutine (SunBurn ());
	}

	public void TakeDamage (int damage) {
		this.life = Mathf.Clamp (this.life + damage, 0, 400);
		if (DamageTaken != null) {
			DamageTaken.Invoke ();
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (controlRef.canHit) {

		}
	}

	public void StartSharkAnimation () {
		if (iceSkeleton != null) {
			iceSkeleton.AnimationState.SetAnimation (0, "Saltar", false);
			iceSkeleton.AnimationState.AddAnimation (0, "Morder", true, 0);
		}
	}
	public void StopSharkAnimation () {
		if (iceSkeleton != null) {
			iceSkeleton.AnimationState.SetAnimation (0, "Death", false);
			iceSkeleton.AnimationState.AddAnimation (0, "Gelo", true, 0);
		}
	}
	public void StartImmortalState () {
		if (iceSkeleton != null) {
			iceSkeleton.AnimationState.SetAnimation (0, "hit", true);
		}
	}
	public void StopImmortalState () {
		if (iceSkeleton != null) {
			iceSkeleton.AnimationState.SetAnimation (0, "Gelo", true);
		}
	}

	public bool isAlive () {
		return this.life > 0 ? true : false;
	}

	private void Update () {
		if (this.life <= 0) {
			//Tira da lista antes de se desativar
			controlRef.iceScripts[(int) myPosition] = null;
			//Dispara couritina de animação de então desativa
			gameObject.SetActive (false);
		}
	}

	private IEnumerator SunBurn () {
		while (this.life > 0) {
			yield return new WaitForSeconds (1);
			if (GameManager.Instance.isBossAlive) {
				if (controlRef.coldWater) {
					TakeDamage (controlRef.coldWaterHeal);
				} else {
					TakeDamage (-controlRef.sunDamage);
				}
			}
		}
	}
}