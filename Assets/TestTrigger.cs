using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        Agent agent = other.GetComponent<Agent>();
        var damagable = other.GetComponent<Damagable>();
        if (damagable != null){
            Debug.Log("Get hit");
            damagable.GetHit(10);
        }
    }
}
