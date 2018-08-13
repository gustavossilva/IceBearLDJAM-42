using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElipticalMovement : MonoBehaviour {
	
	public float a = 2f;
	public float b = 1f;

	[HideInInspector] public float alpha;
 
	float X, Y;

	public float surroundingTime = 4f;
	[HideInInspector] public float currentSurroundingTime;
	[HideInInspector] public Animator anim;
	[HideInInspector] public GameObject[] children;


	void Awake()
	{
		anim = GetComponent<Animator>();
		children = new GameObject[transform.childCount];

		for (int i = 0; i < children.Length; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].SetActive(true);
		}
	}

	void OnEnable()
	{
		anim.enabled = true;
		for (int i = 0; i < children.Length; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].SetActive(true);
		}
	}

	void OnDisable()
	{
		anim.enabled = false;
	}

 	// public void Move (Tubarao tubarao, float velocidadeRodeio, float centerX, float centerY) 
	// {
	// 	alpha += velocidadeRodeio;

	// 	X = centerX + (a * Mathf.Cos(alpha));
	// 	Y = centerY + (b * Mathf.Sin(alpha));

	// 	// Flips image when it reaches the max points of the ellipse
	// 	if(X - centerX <= -a + 0.1f && tubarao.isFacingLeft)
	// 		tubarao.Flip();
	// 	else if(X - centerX >= a - 0.1f && !tubarao.isFacingLeft)
	// 		tubarao.Flip();

	// 	// Set as the new position
	// 	tubarao.rb2D.position = new Vector3(X,Y);
 	// }

	public IEnumerator Move (Tubarao tubarao, float velocidadeRodeio) 
	{
		currentSurroundingTime = 0f;
		
		while(currentSurroundingTime <= surroundingTime)
		{
			float centerX = tubarao.urso.position.x;
			float centerY = tubarao.urso.position.y;

			alpha += (velocidadeRodeio * Time.deltaTime);

			X = centerX + (a * Mathf.Cos(alpha));
			Y = centerY + (b * Mathf.Sin(alpha));

			// Flips image when it reaches the max points of the ellipse
			if(X - centerX <= -a + 0.1f && tubarao.isFacingLeft)
				tubarao.Flip();
			else if(X - centerX >= a - 0.1f && !tubarao.isFacingLeft)
				tubarao.Flip();

			// Set as the new position
			tubarao.rb2D.position = new Vector3(X,Y);

			currentSurroundingTime += Time.deltaTime;

			yield return null;
		}

		// Desabilita barbatana
		for (int i = 0; i < children.Length; i++)
		{
			children[i] = transform.GetChild(i).gameObject;
			children[i].SetActive(false);
		}

		int plataformasDisponiveis = 0;

		List<IceBehaviour> l = new List<IceBehaviour>();

		for(int i = 0; i< IceController.Instance.iceScripts.Length; i++)
		{
			if(IceController.Instance.iceScripts[i] != null && IceController.Instance.iceScripts[i].MyPosition != IcePosition.ICE_CENTER)
			{
				plataformasDisponiveis++;
				l.Add(IceController.Instance.iceScripts[i]);
			}
		}

		// Se ha plataformas disponiveis
		if((plataformasDisponiveis - SpawnerInimigo.dic.Count) > 0)
		{	
			// Escolhe plataforma
			IceBehaviour ice = tubarao.Attack(l);
			IcePosition position = ice.MyPosition;

			float currentChewingTime = 0f;

			// Comeca a morder...
			while(tubarao.isAlive)
			{
				currentChewingTime += Time.deltaTime;
				
				// Apica dano a plataforma
				if(currentChewingTime > 1)
				{
					currentChewingTime = 0f;

					if(ice != null)
					{
						IceController.Instance.TakeDamageByElement(-tubarao.damagePerSecond, position);
					}
					// plataforma destruida
					else
					{
						tubarao.isAlive = false;
						SpawnerInimigo.dic.Remove(position);
						break;
					}
					// if(ice.Life <= 2)
					// {
						
					// }
				}

				yield return null;
			}

			// Quando player hitar o tubarao, pare a animcao de morder
			IceController.Instance.StopSharkAnim(position);
		}

		// Desabilita tubarao (caso nao haja plataformas disponiveis, simplesmente desaparece)
		gameObject.SetActive(false);
 	}
}
