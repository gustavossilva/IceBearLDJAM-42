﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {

	public int dano;
	private Vector2 velocidade;
	private Rigidbody2D rb2D;

	private float offset = 2f;

	public bool wavy;
	[SerializeField] private float _speed;

	public AudioSource iceBreakSound;

    private float index;
	float omegaY;


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

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>

	void OnEnable()
	{
		omegaY = Random.Range(.05f, 1.5f);
		GetComponent<Collider2D>().enabled = true;
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
		return transform.position.x < -(CameraUtil.halfScreenWidthInWorldUnits + offset);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Player"))
		{
			iceBreakSound = GameObject.FindGameObjectWithTag("IceBreakSound").GetComponent<AudioSource>();
			iceBreakSound.Play();
			print("obstaculo colidiu");
			IcePosition pos = collider.GetComponent<IceBehaviour>().MyPosition;

			// Danifica a plataforma
			IceController.Instance.TakeDamageByElement(-dano, pos);
			IceController.Instance.ImmortalFunction();

			gameObject.SetActive(false);
		}
	}
}
