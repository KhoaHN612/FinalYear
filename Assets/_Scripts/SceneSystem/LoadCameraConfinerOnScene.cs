using Cinemachine;
using SVS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCameraConfinerOnScene : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public PolygonCollider2D confinerBounds;

    private void Start()
    {
        LoadCameraConfiner();
    }

    private void LoadCameraConfiner()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        CinemachineConfiner2D confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();


        if (confiner != null && confinerBounds != null)
        {
            confiner.m_BoundingShape2D = confinerBounds;
            //confiner.InvalidatePathCache(); 
            //Debug.Log("Confiner set successfully!");
        }
        else
        {
            //Debug.LogError("Virtual Camera or Confiner Bounds not found.");
        }
    }
}
