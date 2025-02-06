using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIEnemy
{
    public class AIBehaviourBossRangeAttack : AIBehaviour
    {
        [SerializeField] private int numAttacks = 2;

        [SerializeField]
        private AIDataBoard aiBoard;
        public override void PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = Vector2.zero;
            enemyAI.CallAttack(2);

            aiBoard.SetBoard(AIDataTypes.CanUseRangeAttack, false);
            aiBoard.SetBoard(AIDataTypes.Arrived, true);
            aiBoard.SetBoard(AIDataTypes.Waiting, true);

            //Debug.Log("..." + aiBoard.CheckBoard(AIDataTypes.Waiting));

        }

    }
}