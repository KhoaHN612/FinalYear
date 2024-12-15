using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initializer 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Execute()
    {
        //string currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(currentScene);

        //if (currentScene == "MainMenu")
        //{
        //    Debug.Log("Initializer skipped for Main Menu scene.");
        //    return;
        //}

        Debug.Log("Initializer.Execute");
        
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("SceneSwapCanvas")));
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
    }
}
