using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour {

    [HideInInspector] public Rigidbody2D rb2D;
	protected float speed;
    protected Vector2 velocity;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

}
