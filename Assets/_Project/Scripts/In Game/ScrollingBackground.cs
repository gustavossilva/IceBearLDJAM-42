using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public int map = 1;

	private float newMap = 0f;
	private int lastMap = 5;
	private float speed = 5f;

	private float finalTimer = 0f;

	public GameObject gameMusic;
	public GameObject bossMusic;
	public GameObject finalMusic;

	// Use this for initialization
	void Start () {
		//0 - -38.4 em 60s (1 minuto por fase)
		GameManager.Instance.map = map;
		DontDestroyOnLoad(finalMusic);
	}
	
	// Update is called once per frame
	void Update () 
	{
		speed = GameManager.Instance.gameSpeed;
		if(GameManager.Instance.gameOver)
		{
			return;
		}
		//check if there is a game over
		//Init of map - 20.6 to get the final pos
		//Check boss fight
		transform.position += Vector3.left *(speed * Time.deltaTime);
		newMap = transform.position.x;
		if(newMap < -38.4 * map)
		{
			Debug.Log("new map start");
			map++;
			GameManager.Instance.map = map;
			//Start new map
		}
		if(map == 5 && GameManager.Instance.isBossAlive)
		{
			gameMusic.GetComponent<AudioSource>().Stop();
			if(!bossMusic.GetComponent<AudioSource>().isPlaying)
				bossMusic.GetComponent<AudioSource>().Play();
			GameManager.Instance.startBossBatle = true;
			GameManager.Instance.gameSpeed = 0; //boss fight
		}
		else if(map == 5 && !GameManager.Instance.isBossAlive)
		{
			bossMusic.GetComponent<AudioSource>().Stop();
			GameManager.Instance.gameSpeed = GameManager.Instance.initSpeed;

		}
		if(transform.position.x < -173.2) //(-38.4f * (lastMap-1) - 20.6f)
		{
			if(!finalMusic.GetComponent<AudioSource>().isPlaying)
			{
				finalMusic.GetComponent<AudioSource>().Play();
			}
			GameManager.Instance.winner = true;
			GameManager.Instance.UpdateFilhoAnimation();
			GameManager.Instance.gameSpeed = 0;
			finalTimer += Time.deltaTime;
		}
		if(finalTimer > 4)
		{
			GameManager.Instance.gameOver = true;
		}
	}
}
