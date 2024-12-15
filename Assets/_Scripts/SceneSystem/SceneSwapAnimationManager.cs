using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwapAnimationManager : MonoBehaviour
{
    public static SceneSwapAnimationManager instance;

    [SerializeField] private Image _fadeOutImage;
    [Range(0.1f, 10f)][SerializeField] private float _fadeOutSpeed = 0.5f;
    [Range(0.1f, 10f)][SerializeField] private float _fadeInSpeed = 0.5f;

    [SerializeField] private Color _fadeOutStartColor;
    public bool IsFadingOut { get; private set; }
    public bool IsFadingIn { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        _fadeOutStartColor.a = 0f;
    }

    private void Update()
    {
        if (IsFadingOut)
        {
            if (_fadeOutImage.color.a <= 1)
            {
                _fadeOutStartColor.a += _fadeOutSpeed * Time.deltaTime;
                _fadeOutImage.color = _fadeOutStartColor;
            }
            else
            {
                IsFadingOut = false;
            }
        }

        if (IsFadingIn)
        {
            if (_fadeOutImage.color.a >= 0)
            {
                _fadeOutStartColor.a -= _fadeInSpeed * Time.deltaTime;
                _fadeOutImage.color = _fadeOutStartColor;
            }
            else
            {
                IsFadingIn = false;
            }
        }
    }

    public void StartFadeOut()
    {
        _fadeOutImage.color = _fadeOutStartColor;
        IsFadingOut = true;
    }

    public void StartFadeIn()
    {
        if (_fadeOutImage.color.a >= 1)
        {
            _fadeOutImage.color = Color.clear;
            IsFadingIn = true;
        }

    }
}
