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
            //throw new System.NotImplementedException();
        }

        public override void ReadyNextState()
        {
            nextState = new StateIdle(machine, player);
            isComplete = true;
        }
    }
}