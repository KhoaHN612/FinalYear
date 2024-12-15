using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    [SerializeField] private UnityEvent onContinueButtonPressed;
    [SerializeField] private UnityEvent onMenuButtonPressed;

    [SerializeField] private UnityEvent onShowMenu;
    [SerializeField] private UnityEvent onHideMenu;

    [SerializeField] private bool isMenuActive = false;

    private void Awake()
    {
        if (continueButton != null)
            continueButton.onClick.AddListener(() => OnContinueButtonClick());

        if (menuButton != null)
            menuButton.onClick.AddListener(() => OnMainMenuButtonClick());

    }

    private void OnDestroy()
    {
        if (continueButton != null)
            continueButton.onClick.RemoveListener(() => OnContinueButtonClick());

        if (menuButton != null)
            menuButton.onClick.RemoveListener(() => OnMainMenuButtonClick());

    }

    private void OnContinueButtonClick()
    {
        onContinueButtonPressed?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        HideMenu();

    }

    private void OnMainMenuButtonClick()
    {
        onMenuButtonPressed?.Invoke();
        SceneManager.LoadScene("MainMenu");
        HideMenu();
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
