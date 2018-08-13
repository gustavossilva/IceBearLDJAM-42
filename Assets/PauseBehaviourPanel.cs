using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseBehaviourPanel : MonoBehaviour {

	[SerializeField] private GameObject _pausePanel;

	public void PauseButton(){
		_pausePanel.SetActive(true);
		Time.timeScale = 0;
	}
	public void ResumeButton(){
		_pausePanel.SetActive(false);
		Time.timeScale = 1;

	}
	public void RestartButton(){
		Time.timeScale = 1;
		SceneManager.LoadScene("MainScene");
	}
	public void QuitButton(){
		Time.timeScale = 1;
		SceneManager.LoadScene("SceneMenu");
	}
}
