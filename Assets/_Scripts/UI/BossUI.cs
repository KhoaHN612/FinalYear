using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.UI
{
    public class BossUI : MonoBehaviour
    {
        public GameObject healthPanel;
        public MicroBar healthBar;

        public void Initialize(int val)
        {
            healthBar.Initialize(val);
        }

        public void SetHealth(int val)
        {
            healthBar.UpdateBar(val);
        }

        public void ToggleHealthPanel(bool val)
        {
            healthPanel.SetActive(val);
        }
    }
}