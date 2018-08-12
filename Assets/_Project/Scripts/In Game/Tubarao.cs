using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubarao : Enemy {

	public Transform urso;
	private bool alcancouUrso = false;
	private bool isFacingLeft = true;
	private float angle;

	[SerializeField] private float velocidadeRodeio;

	public ElipticalMovement elipticalMovement;

	private Vector2 pointInEllipsis;

	protected override void Awake()
	{
		speed = 2f;

		// Nothing so far
		base.Awake();
	}

	void Update()
	{
		if(!alcancouUrso)
		{
			Vector2 direcao = (urso.position - transform.position).normalized;

			angle = MathUtil.GetAngleBasedOnPosition(urso.position, transform.position);

			// Check whether this shark entered inside the ellipse
			// Get a point tangent to the ellipse based on a given angle
			// https://stackoverflow.com/questions/17762077/how-to-find-the-point-on-ellipse-given-the-angle
			float t = Mathf.Atan2 (elipticalMovement.a * Mathf.Tan(angle * Mathf.Deg2Rad), elipticalMovement.b);

			if(angle >= 90 && angle <= 180)
				t += Mathf.PI; 
			else if(angle >= -180 && angle <= -90)
				t -= Mathf.PI;

			float x = urso.position.x + elipticalMovement.a * Mathf.Cos(t);
			float y = urso.position.y + elipticalMovement.b * Mathf.Sin(t);
			pointInEllipsis = new Vector2(x, y);


			TurnFace(direcao);

			velocity = direcao * speed;

			// Se entrou na elipse
			if(UrsoPerto(rb2D.position, pointInEllipsis))
			{
				alcancouUrso = true;
				elipticalMovement.alpha = t;
			}
		}
		else
		{
			elipticalMovement.Move(velocidadeRodeio * Time.deltaTime, urso.position.x, urso.position.y);
		}
	}

	void FixedUpdate()
	{
		if(alcancouUrso)
			return;
			
		rb2D.position += velocity * Time.deltaTime;
	}

	private bool UrsoPerto(Vector2 posA, Vector2 posB)
	{
		return Vector2.Distance(posA, posB) < .1f;
	}

	private void TurnFace(Vector2 direcao)
	{
		if(isFacingLeft && direcao.x > 0)
		{
			Flip();
		}
		else if(!isFacingLeft && direcao.x < 0)
		{
			Flip();
		}
	}

	private void Flip()
	{
		isFacingLeft = !isFacingLeft;
		transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(urso.position, pointInEllipsis);
		Gizmos.DrawSphere(pointInEllipsis, .05f);
	}
}
