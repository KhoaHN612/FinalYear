using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSceneChanger: MonoBehaviour
{
    public PlayableDirector playableDirector;   
    public string nextSceneName;               

    void Start()
    {
        playableDirector.stopped += OnTimelineStopped;
    }

    void OnDestroy()
    {
        playableDirector.stopped -= OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            Debug.Log("Timeline finished. Loading next scene...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
