using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject persistObjects;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == "MainMenu")
        {
            if (persistObjects != null)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                Debug.Log("PERSISTOBJECTS disabled in Main Menu.");
            }
        }
        else
        {
            if (persistObjects != null)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                Debug.Log("PERSISTOBJECTS enabled in scene: " + scene.name);
            }
        }
    }
}
