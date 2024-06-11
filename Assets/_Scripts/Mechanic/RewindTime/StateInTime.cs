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

	public StateInTime (Vector3 _position, Quaternion _rotation, Vector3 _scale, Sprite _sprite)
	{
		position = _position;
		rotation = _rotation;
		scale = _scale;
        sprite = _sprite;
	}
}
