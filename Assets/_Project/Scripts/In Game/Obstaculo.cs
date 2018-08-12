using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {

	public int dano;

	private Vector2 direcao = Vector2.left;
	private Vector2 velocidade;
	private Rigidbody2D rb2D;

	private float offset = 2f;

	public bool wavy;
	[SerializeField] private float _speed;

    private float index;
	[SerializeField] float omegaY = 5.0f;


	/// <summary> Deve ser setada de acordo com a speed do urso </summary>
	public float speed
	{
		get { return this._speed; }
		set { this._speed = Mathf.Clamp(value, .5f, int.MaxValue); }
	}

    void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		speed = 1f;		// 1m/s inicialmente
	}

	void FixedUpdate()
	{
		// Movimento ondular, pensar em uma forma melhor se sobrar tempo
		if(wavy)
		{
			// -1 a 1
			float up = -1f + Mathf.PingPong(Time.time, 2f);
			velocidade = (Vector2.left * speed) + (Vector2.up * up * omegaY);
		}
		else
			velocidade = Vector2.left * speed;

		// Move obstaculo
		Vector2 newPosition = rb2D.position + (velocidade * Time.deltaTime);	
		rb2D.MovePosition(newPosition);

		if(EstaForaDaTela())
			gameObject.SetActive(false);
	}

	private bool EstaForaDaTela()
	{
		return transform.position.x < -(CameraUtil.halfScreenWidthInWorldUnits);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		print("obstaculo colidiu");
		IcePosition pos = collider.GetComponent<IceBehaviour>().MyPosition;

		// Danifica a plataforma
		IceController.Instance.TakeDamageByElement(dano, pos);

		gameObject.SetActive(false);
	}
}
