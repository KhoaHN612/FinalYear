using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void CopyFrom(this Transform target, Transform source)
    {
        if (source != null && target != null)
        {
            target.position = source.position;
            target.rotation = source.rotation;
            target.localScale = source.localScale;
        }
    }
}