using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StateInTime 
{
	public Vector3 position;
	public Quaternion rotation;
    public Vector3 scale;
    public Sprite sprite;
	public Vector2 velocity;
	public float angularVelocity;

    public StateInTime (Vector3 _position, Quaternion _rotation, Vector3 _scale, Sprite _sprite, Vector2 _velocity, float _angularVelocity)
	{
		position = _position;
		rotation = _rotation;
		scale = _scale;
        sprite = _sprite;
        velocity = _velocity;
        angularVelocity = _angularVelocity;
    }
}
