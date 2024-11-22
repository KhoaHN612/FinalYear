using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractiveInterface 
{
    InteractiveObject InteractiveObject { get; set; }
    void SetInteractiveObject(InteractiveObject interactiveObject);
    void ClearInteractiveObject(InteractiveObject interactiveObject);
}
