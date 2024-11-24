using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBeTimeAffect
{
    public void StopTime();
    public void ResumeTime();
    public void AdjustSpeed(float speed);
}
