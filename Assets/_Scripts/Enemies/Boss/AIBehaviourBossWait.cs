using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIEnemy
{
    public class AIBehaviourBossWait : AIBehaviour
    {
        [SerializeField]
        private AIDataBoard aiBoard;
        [SerializeField]
        private float waitTime = 1;

        [SerializeField]
        private bool hasWaiting = false;

        public override void PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = Vector2.zero;
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            if (hasWaiting)
            {
                yield break;
            }
            
            hasWaiting = true;
            yield return new WaitForSeconds(waitTime);
            aiBoard.SetBoard(AIDataTypes.Waiting, false);
            hasWaiting = false;
        }
    }
}