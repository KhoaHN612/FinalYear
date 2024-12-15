using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;

    private static bool _isLoadFromPath;

    private GameObject _player;
    private Collider2D _playerCollider;
    private Collider2D _pathCollider;
    private Vector3 _playerSpawnPosition;

    private PathTriggerInteraction.PathType _currentPathToSpawnTo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceceLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceceLoaded;
    }

    public static void SwapSceneFromDoorUse(SceneField ToScene, PathTriggerInteraction.PathType pathToSpawnTo)
    {
        _isLoadFromPath = true;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(ToScene, pathToSpawnTo));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField ToScene, PathTriggerInteraction.PathType pathToSpawnTo = PathTriggerInteraction.PathType.None)
    {
        SceneSwapAnimationManager.instance.StartFadeOut();

        while (SceneSwapAnimationManager.instance.IsFadingOut)
        {
            yield return null;
        }

        _currentPathToSpawnTo = pathToSpawnTo;
        SceneManager.LoadScene(ToScene);
    } 

    private void OnSceceLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneSwapAnimationManager.instance.StartFadeIn();

        if (_isLoadFromPath)
        {
            findPath(_currentPathToSpawnTo);
            _player.transform.position = _playerSpawnPosition;
            _isLoadFromPath = false;

        }
    }

    private void findPath(PathTriggerInteraction.PathType pathSpawnNumber) 
    {
        PathTriggerInteraction[] pathTriggers = FindObjectsOfType<PathTriggerInteraction>();

        for (int i = 0; i < pathTriggers.Length; i++)
        {
            if (pathTriggers[i].currentPathType == pathSpawnNumber)
            {
                _pathCollider =  pathTriggers[i].GetComponent<Collider2D>();
                CalculateSpawnPosition();
                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        //float colliderHeight = _playerCollider.bounds.extents.y;

        //_playerSpawnPosition = _pathCollider.transform.position - new Vector3(0f, colliderHeight, 0f);
        float groundLevel = _pathCollider.bounds.min.y;

        float playerHalfHeight = _playerCollider.bounds.extents.y;

        _playerSpawnPosition = new Vector3(
            _pathCollider.bounds.center.x,            
            groundLevel + playerHalfHeight,          
            0f            
        );
    }
}
