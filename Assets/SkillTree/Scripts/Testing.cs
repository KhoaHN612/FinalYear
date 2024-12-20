﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    [SerializeField] private SkillTreePlayer player;
    [SerializeField] private OriginalUI_SkillTree uiSkillTree;

    private void Start() {
        uiSkillTree.SetPlayerSkills(player.GetPlayerSkills());
    }

}
