using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractBase : MonoBehaviour
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");   
    }

    public virtual void Interact() { }
}
