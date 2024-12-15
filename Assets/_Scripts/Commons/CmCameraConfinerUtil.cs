using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Common
{
    public class CmCameraConfinerUtil : MonoBehaviour
    {
        public PolygonCollider2D cameraConfiner;
        public CinemachineConfiner2D cm_confiner;

        public void SetConfiner()
        {
            CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            cm_confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();

            cm_confiner.m_BoundingShape2D = cameraConfiner;
        }
    }
}