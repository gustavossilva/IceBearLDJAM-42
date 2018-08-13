using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class painel : MonoBehaviour {

	 void ativaPainel()
	 {
		 gameObject.SetActive(true);
	 }

	 void fecharPainel()
	 {
		 gameObject.SetActive(false);
	 }
}
