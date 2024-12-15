using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform boss;
    public float zoomDuration = 3f;
    public float zoomSizeOffset = 5f;

    [SerializeField]private Transform initialFollow;
    [SerializeField]private Transform initialLookAt;
    private float defaultSize;
    private bool isZooming = false;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        defaultSize = virtualCamera.m_Lens.OrthographicSize;
        initialFollow = virtualCamera.Follow;
        initialLookAt = virtualCamera.LookAt;
    }

    public void SwitchToBossView()
    {
        if (isZooming) return;
        StartCoroutine(SwitchToBossViewCoroutine());
    }

    private IEnumerator SwitchToBossViewCoroutine()
    {
        isZooming = true;

        virtualCamera.Follow = boss;
        virtualCamera.LookAt = boss;

        float targetSize = defaultSize - zoomSizeOffset;
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(defaultSize, targetSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = targetSize;

        yield return new WaitForSeconds(zoomDuration);

        elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(targetSize, defaultSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = defaultSize;

        virtualCamera.Follow = initialFollow;
        virtualCamera.LookAt = initialLookAt;

        isZooming = false;
    }
}
