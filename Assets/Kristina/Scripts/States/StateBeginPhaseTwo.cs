using UnityEngine;

namespace hatsune_miku
{
    public class StateBeginPhaseTwo : BossState
    {
        public StateBeginPhaseTwo(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }

        public override void OnEnter()
        {
            machine.anim.Play("BeginFly");
            ReadyNextState();
        }

        public override void OnUpdate()
        {
            
        }

        public override void ReadyNextState()
        {
            nextState = new StateIdleFlying(machine, player);
            isComplete = true;
        }
    }
}