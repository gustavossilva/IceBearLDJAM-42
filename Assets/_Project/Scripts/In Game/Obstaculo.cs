using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {

	public int dano;

	private Vector2 direcao = Vector2.left;
	private Vector2 velocidade;
	private Rigidbody2D rb2D;

	private float offset = 2f;

	void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		SetVelocity(3f);		// 1m/s inicialmente
	}

	void FixedUpdate()
	{
		// Move obstaculo
		Vector2 newPosition = rb2D.position + (velocidade * Time.deltaTime);	
		rb2D.MovePosition(newPosition);

		if(EstaForaDaTela())
			gameObject.SetActive(false);
	}


	// Deve ser setada de acordo com a speed do urso
	public void SetVelocity(float speedUrso)
	{
		velocidade = direcao * speedUrso;
	}

	private bool EstaForaDaTela()
	{
		return transform.position.x < -(CameraUtil.halfScreenWidthInWorldUnits);
	}
}
