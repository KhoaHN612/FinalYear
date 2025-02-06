using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIEnemy
{
    public class AIBossEnemyBrain : AIEnemy
    {
        [SerializeField]
        private AIDataBoard aiBoard;
        [SerializeField]
        private AiPlayerEnterAreaDetector playerDetector;
        [SerializeField]
        private AIMeleeAttackDetector meleeRangeDetector;
        [SerializeField]
        private AIEndPlatformDetector endPlatformDetector;

        [SerializeField]
        private bool haveMet = false;

        [SerializeField] private float attackRangeCooldown = 10f;

        [SerializeField]
        private AIBehaviour IdleBehaviour, ChargeBehaviour, MeleeAttackBehaviour, WaitBehaviour, AttackRangeBehavior;
        private void Start()
        {
            
        }
        private void Update()
        {
            aiBoard.SetBoard(AIDataTypes.PlayerDetected, playerDetector.PlayerInArea);
            aiBoard.SetBoard(AIDataTypes.InMeleeRange, meleeRangeDetector.PlayerDetected);
            aiBoard.SetBoard(AIDataTypes.PathBlocked, endPlatformDetector.PathBlocked);

            if (aiBoard.CheckBoard(AIDataTypes.PlayerDetected))
            {
                if (!haveMet) 
                {
                    CallOnPlayAnimation("Roar");
                    haveMet = true;
                    StartCoroutine(AttackRangeCooldown());
                } 
                else
                {
                    //Debug.Log(aiBoard.CheckBoard(AIDataTypes.Waiting)); 
                    if (aiBoard.CheckBoard(AIDataTypes.Waiting))
                    {
                        WaitBehaviour.PerformAction(this);
                    }
                    else
                    {
                        /*Debug.Log(aiBoard.CheckBoard(AIDataTypes.Waiting));*/
                        if (aiBoard.CheckBoard(AIDataTypes.CanUseRangeAttack))
                        {
                            AttackRangeBehavior.PerformAction(this);
                            StartCoroutine(AttackRangeCooldown());
                        }
                        else if (aiBoard.CheckBoard(AIDataTypes.InMeleeRange))
                        {
                            MeleeAttackBehaviour.PerformAction(this);
                        }
                        else
                        {
                            //Debug.Log("Charge");
                            ChargeBehaviour.PerformAction(this);
                        }
                    }
                }
                
            }
            else
            {
                IdleBehaviour.PerformAction(this);
            }
        }
        public IEnumerator AttackRangeCooldown()
        {
            yield return new WaitForSeconds(attackRangeCooldown);
            aiBoard.SetBoard(AIDataTypes.CanUseRangeAttack, true);
        }
    }
}