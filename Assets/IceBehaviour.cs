using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IceBehaviour : MonoBehaviour {

	IceController controlRef;

	[SerializeField] private int life = 100;

	public int Life {
		private set { }
		get {
			return this.life;
		}
	}

	[SerializeField] IcePosition myPosition;

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
		StartCoroutine (SunBurn ());
	}

	public void TakeDamage (int damage) {
		this.life = Mathf.Clamp (this.life + damage, 0, 100);
		if (DamageTaken != null) {
			DamageTaken.Invoke ();
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (controlRef.canHit) {

		}
	}

	public bool isAlive () {
		return this.life > 0 ? true : false;
	}

	private IEnumerator SunBurn () {
		while (this.life > 0) {
			yield return new WaitForSeconds (1);
			if (controlRef.coldWater) {
				TakeDamage (controlRef.coldWaterHeal);
			} else {
				TakeDamage (-controlRef.sunDamage);
			}
		}
		//Tira da lista antes de se desativar
		controlRef.iceScripts[(int) myPosition] = null;
		//Dispara couritina de animação de então desativa
		gameObject.SetActive (false);
	}
}