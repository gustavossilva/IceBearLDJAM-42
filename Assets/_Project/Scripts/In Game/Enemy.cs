﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour {

    [HideInInspector] public Rigidbody2D rb2D;
	protected float _speed;

    public float speed
    {
        get { return this._speed; }
		set { this._speed = Mathf.Clamp(value, .5f, int.MaxValue); }
    }

    protected Vector2 velocity;

    public int damagePerSecond = 10;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

}
