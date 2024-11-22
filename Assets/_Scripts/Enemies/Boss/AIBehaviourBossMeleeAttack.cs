using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIEnemy
{
    public class AIBehaviourBossMeleeAttack : AIBehaviour
    {
        [SerializeField]
        private AIDataBoard aiBoard;
        public override void PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = Vector2.zero;
            enemyAI.CallAttack();

            aiBoard.SetBoard(AIDataTypes.Arrived, true);
            aiBoard.SetBoard(AIDataTypes.Waiting, true);

            Debug.Log("..." + aiBoard.CheckBoard(AIDataTypes.Waiting));
        }
    }
}