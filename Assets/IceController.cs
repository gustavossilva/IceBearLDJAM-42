using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe responsável por cuidar de todos os IceBehaviours
/// </summary>
/// <typeparam name="IceController"></typeparam>
public class IceController : Singleton<IceController> {
	public bool canHit = true;
	public bool coldWater = false;

	public int coldWaterHeal = 1;
	public int sunDamage = 1;
	float timeForNextHit = 1;

	[SerializeField] Collider2D[] iceList;

	[SerializeField] public IceBehaviour[] iceScripts;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	protected override void Awake () {

		IsPersistentBetweenScenes = false;
		base.Awake ();
	}
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start () {
		iceList = GetComponentsInChildren<Collider2D> ();
		iceScripts = GetComponentsInChildren<IceBehaviour> ();
	}

	void TakeDamageByElement (int damage, IcePosition position) {
		iceScripts[(int) position].TakeDamage (damage);
		//Disparar animação necessária;
	}

	IEnumerator StartImmortality () {
		canHit = false;
		ChangeIceCollidersState ();
		//Dispara animação de hit
		//Active hit Animation and Imortal Animation
		yield return new WaitForSeconds (timeForNextHit);
		ChangeIceCollidersState ();
		canHit = true;
	}

	//Função responsável por desabilitar e abilitar os colisores dos gelos
	private void ChangeIceCollidersState () {
		for (int i = 0; i < iceList.Length; i++) {
			if (iceList[i] != null) {
				if (iceScripts[i])
					iceList[i].enabled = !iceList[i].enabled;
			}
		}
	}

}
public enum IcePosition { ICE_TOPLEFT, ICE_TOPRIGHT, ICE_BOTTOMLEFT, ICE_BOTTOMRIGHT, ICE_CENTER }