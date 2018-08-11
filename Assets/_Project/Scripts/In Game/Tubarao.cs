using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubarao : Enemy {

	public Transform urso;
	public float distanciaMinima;
	private bool alcancouUrso = false;

	private Sprite plataformaUrso;
	private Bounds plataformaBounds;

	private Animator animator;

	protected override void Awake()
	{
		speed = 2f;
		animator = GetComponent<Animator>();

		animator.enabled = false;
		// Nothing so far
		base.Awake();
	}

	void Start()
	{
		// plataformaUrso = GameObject.Find("Plataforma").GetComponent<SpriteRenderer>().sprite;
		// plataformaBounds = plataformaUrso.bounds;
	}

	void Update()
	{
		if(!alcancouUrso)
		{
			Vector2 direcao = (urso.position - transform.position).normalized;
			velocity = direcao * speed;

			// Se alcancou urso
			if(UrsoPerto())
			{
				alcancouUrso = true;

				// ...seta o tubarao como filho do urso
				transform.SetParent(urso);


				animator.enabled = true;
				animator.SetTrigger("isSurrounding");
			}
		}
	}

	void FixedUpdate()
	{
		rb2D.velocity = velocity;
	}

	private bool UrsoPerto()
	{
		return Vector2.Distance(urso.position, transform.position) <= distanciaMinima;
	}
}
