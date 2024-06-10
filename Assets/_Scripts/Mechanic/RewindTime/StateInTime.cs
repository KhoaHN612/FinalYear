using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInTime 
{
	public Vector3 position;
	public Quaternion rotation;

    public SpriteRenderer spriteRenderer;

	public StateInTime (Vector3 _position, Quaternion _rotation, SpriteRenderer _spriteRenderer)
	{
		position = _position;
		rotation = _rotation;
        spriteRenderer = _spriteRenderer;
	}
}
