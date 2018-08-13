using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public GameObject tutorial;

    private void Start() 
    {
        if(!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime",1);
            tutorial.SetActive(true);
        }
    }
    
    public void ChamaCena(string nome)
    {
        SceneManager.LoadScene(nome);

    }

    public void QuitGame()
    {
        Debug.Log("Jogo Fechado! ");
		Application.Quit();
    }
}

