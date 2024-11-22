using System.Collections;
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
    public bool isSlowTime = false;

    void Update()
    {
        if (!shouldRecoverTime) return;

        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime = Mathf.Clamp(Time.timeScale * maxFixedDeltaTime,0, maxFixedDeltaTime);
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


}