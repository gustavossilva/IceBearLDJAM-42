using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
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

