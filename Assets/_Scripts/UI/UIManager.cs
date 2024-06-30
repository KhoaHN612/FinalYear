using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public DisplayBarUI healthBar, manaBar, timeBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.Initialize();
        manaBar.Initialize();
        timeBar.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
