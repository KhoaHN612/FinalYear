using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillTreeMenu : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private UnityEvent onShow, onHide;
    private bool isOpen = false;

    public UISkillTree uiSkillTree;

    public static SkillTreeMenu Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        animator = GetComponent<Animator>();
        if (uiSkillTree == null)
        {
            uiSkillTree = FindObjectOfType<UISkillTree>();
            Debug.LogError("No UISkillTree found in scene");
        }
    }

    public void ShowMenu()
    {
        animator.SetBool("isOpen", true);
    }

    public void HideMenu() 
    {
        animator.SetBool("isOpen", false);
    }

    public void ToggleMenu()
    {
        if (isOpen)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
        isOpen = !isOpen;
    }   
}
