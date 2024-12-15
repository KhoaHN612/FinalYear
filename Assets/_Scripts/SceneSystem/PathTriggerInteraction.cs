using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTriggerInteraction : MonoBehaviour
{
    public enum PathType
    {
        None,
        One,
        Two,
        Three,
        Four,
        Five,
    }

    [Header("Go To")]
    [SerializeField] private PathType pathSpawnTo;
    [SerializeField] private SceneField sceneGoTo;

    [Space(10f)]
    [Header("Go From")]
    public PathType currentPathType;

    public void Interact()
    {
        SceneSwapManager.SwapSceneFromDoorUse(sceneGoTo, pathSpawnTo);
    }
}
