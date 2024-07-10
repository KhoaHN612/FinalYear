using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIEnemy
{
    public abstract class AIBehaviour : MonoBehaviour
    {
        public abstract void PerformAction(AIEnemy enemyAI);
    }
}