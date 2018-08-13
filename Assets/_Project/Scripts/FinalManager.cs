using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Destroy (GameObject.Find("FinalMusic"));
			SceneManager.LoadScene("SceneMenu");
		}
	}
}
