using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour {

	protected float speed;
    protected Rigidbody2D rb2D;
    protected Vector2 velocity;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

}
