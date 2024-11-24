using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private void Awake()
    {
        maxFixedDeltaTime = Time.fixedDeltaTime;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("TimeManager exist, newone is destroyed");
            return;
        }
    }

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 0.1f;
    [SerializeField] private float maxFixedDeltaTime;
    public bool shouldRecoverTime = false;
    public bool isSlowTime { get; private set; } = false;
    public bool isStopTime { get; private set; } = false;
    public bool isAdjustTime { get; private set; } = false;
    public bool isSpeedUpPlayer { get; private set; } = false;
    private Agent player;
    private List<ICanBeTimeAffect> canBeTimeAffectObjects;

    private void Start()
    {
        canBeTimeAffectObjects = new List<ICanBeTimeAffect>(FindObjectsOfType<MonoBehaviour>(true)
            .OfType<ICanBeTimeAffect>());

        player = FindObjectsOfType<Agent>()
            .FirstOrDefault(a => a.CompareTag("Player"));
    }

    void Update()
    {
        if (!shouldRecoverTime) return;

        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime = Mathf.Clamp(Time.timeScale * maxFixedDeltaTime, 0, maxFixedDeltaTime);
        if (Time.timeScale >= 1) shouldRecoverTime = false;
    }

    public void DoSlowmotion()
    {
        shouldRecoverTime = true;
        maxFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * maxFixedDeltaTime;
    }

    public void DoSlowTime(float slowTo, float backIn)
    {
        isSlowTime = true;
        maxFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = slowTo;
        Time.fixedDeltaTime = Time.timeScale * maxFixedDeltaTime;
        StartCoroutine(BackSlowTime(backIn));
    }

    private IEnumerator BackSlowTime(float backIn)
    {
        yield return new WaitForSeconds(backIn);
        Time.timeScale = 1;
        Time.fixedDeltaTime = maxFixedDeltaTime;
        isSlowTime = false;
    }

    public void StopTime()
    {
        isStopTime = true;
        foreach (var obj in canBeTimeAffectObjects)
        {
            obj.StopTime();
        }
    }
    public void ResumeTime()
    {
        isStopTime = false;
        foreach (var obj in canBeTimeAffectObjects)
        {
            obj.ResumeTime();
        }
    }

    public void AdjustTime(float r)
    {
        if (r != 1)
        {
            isAdjustTime = false;
        }
        else
        {
            isAdjustTime = true;
        }
        foreach (var obj in canBeTimeAffectObjects)
        {
            obj.AdjustSpeed(r);
        }
    }

    private void AdjustTimeObject(ICanBeTimeAffect obj, float r)
    {
        if (obj == null)
        {
            return;
        }
        else
        {
            Debug.Log(obj);
            obj.AdjustSpeed(r);
        }
    }

    public void SpeedUpPlayer()
    {
        isSpeedUpPlayer = true;
        if (player != null)
        {
            AdjustTimeObject(player, 2);
        }
    }

    public void StopSpeedUpPlayer()
    {
        isSpeedUpPlayer = false;
        if (player != null)
        {
            AdjustTimeObject(player, 1);
        }
    }

}