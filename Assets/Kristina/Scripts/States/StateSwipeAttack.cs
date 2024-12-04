using UnityEngine;

namespace hatsune_miku
{
    public class StateSwipeAttack : BossState
    {
        public StateSwipeAttack(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }

        public override void OnEnter()
        {
            machine.anim.Play("Swipe");
            //ReadyNextState();
        }
        public override void OnUpdate()
        {
            if (elapsed > 5f)
                ReadyNextState();
        }

        public override void ReadyNextState()
        {
            if (machine.phase == 2)
                nextState = new StateAimedShot(machine, player);
            else //phase 3, should not reach here as phase 1
                nextState = new StateHardAimShot(machine, player);

            isComplete = true;
        }
    }
}