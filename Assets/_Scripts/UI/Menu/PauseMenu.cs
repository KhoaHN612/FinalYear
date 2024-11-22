using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button soundButton;

    [SerializeField] private UnityEvent onHomeButtonPressed;
    [SerializeField] private UnityEvent onResumeButtonPressed;
    [SerializeField] private UnityEvent onSoundButtonPressed;

    [SerializeField] private UnityEvent onShowMenu;
    [SerializeField] private UnityEvent onHideMenu;

    [SerializeField] private bool isMenuActive = false;

    private void Awake()
    {
        if (homeButton != null)
            homeButton.onClick.AddListener(() => OnHomeButtonClick());

        if (resumeButton != null)
            resumeButton.onClick.AddListener(() => OnResumeButtonClick());

        if (soundButton != null)
            soundButton.onClick.AddListener(() => OnSoundButtonClick());
    }

    private void OnDestroy()
    {
        if (homeButton != null)
            homeButton.onClick.RemoveListener(() => OnHomeButtonClick());

        if (resumeButton != null)
            resumeButton.onClick.RemoveListener(() => OnResumeButtonClick());

        if (soundButton != null)
            soundButton.onClick.RemoveListener(() => OnSoundButtonClick());
    }

    private void OnHomeButtonClick()
    {
        onHomeButtonPressed?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnResumeButtonClick()
    {
        onResumeButtonPressed?.Invoke();
    }

    private void OnSoundButtonClick()
    {
        onSoundButtonPressed?.Invoke();
    }

    public void ShowMenu()
    {
        onShowMenu?.Invoke();
    }

    public void HideMenu()
    {
        onHideMenu?.Invoke();
    }

    public void ToggleMenu()
    {
        if (isMenuActive)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
        isMenuActive = !isMenuActive;
    }
}
