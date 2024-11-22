using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;
    [SerializeField] private Vector3 globalShakeVelocity = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private CinemachineImpulseListener impulseListener;
    private CinemachineImpulseDefinition impulseDefinition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    
        }
    }
    public void CameraShake(CinemachineImpulseSource source)
    {
        source.GenerateImpulseWithVelocity(globalShakeVelocity);
    }

    private void SreenShakeFromProfile(ScreenShakeDataSO profile, CinemachineImpulseSource impulseSource)
    {
        impulseDefinition = impulseSource.m_ImpulseDefinition;
        impulseSource.m_DefaultVelocity = profile.defaultVelocity;
        impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;

        impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
        impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
    }
}
