using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoryManager : MonoBehaviour {

	public GameObject[] history;
	public GameObject[] historyText;
	public GameObject box;
	public GameObject[] finalTexts;
	public int historyIndex = 1;

	public bool isFading = false;

	public Color32 initialColor;
	public Color32 finalColor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isFading)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				UpdateHistory();
			}
			if(Input.GetKeyDown(KeyCode.Q)) //Skip
			{
				Destroy (GameObject.Find("MenuMusic"));
				SceneManager.LoadScene("MainScene");
			}
		}
	}

	void UpdateHistory()
	{
		if(historyIndex == history.Length)
		{
			box.SetActive(false);
			historyText[historyIndex-1].SetActive(false);
			for(int i =0;i<history.Length-1;i++)
			{
				history[i].SetActive(false);
			}
			StartCoroutine(FadingImage());
			finalTexts[0].SetActive(true);
			finalTexts[1].SetActive(true);
			isFading = true;
		}
		else
		{
			history[historyIndex].SetActive(true);
			historyText[historyIndex-1].SetActive(false);
			historyText[historyIndex].SetActive(true);
			historyIndex++;
		}
	}

	IEnumerator FadingImage()
	{
		SpriteRenderer imageRender = history[historyIndex-1].GetComponent<SpriteRenderer>();
		WaitForSeconds wait = new WaitForSeconds(0.0167f);
		while(imageRender.color != finalColor)
		{
			imageRender.color = Color32.Lerp(imageRender.color,finalColor,Time.deltaTime);
			yield return wait;
		}
		StopAllCoroutines();
		Destroy (GameObject.Find("MenuMusic"));
		SceneManager.LoadScene("MainScene");
	}
}
