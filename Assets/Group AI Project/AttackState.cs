using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class AttackState : State
    {
        public AttackState(StateController stateController) : base(stateController) { }

        public override void CheckTransitions()
        {
            if (!stateController.CheckAttackRange("Player"))
            {
                stateController.SetState(new ChaseState(stateController));
            }
        else if (Random.Range(0, 10000) < 10)
        {
            stateController.SetState(new ScaredState(stateController));
        }
        }
        public override void Act()
        {
            if (stateController.enemyToChase != null)
            {
                stateController.destination = stateController.enemyToChase.transform;
                stateController.ai.SetTarget(stateController.destination);
            }
        }
        public override void OnStateEnter()
        {
        stateController.ChangeColor(Color.yellow);
        stateController.ai.agent.speed = 0f;
        }
    }
