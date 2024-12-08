using UnityEngine;

namespace hatsune_miku
{
    public class StateChangePhase : BossState
    {
        public StateChangePhase(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }
        public override void OnEnter()
        {
            machine.anim.SetTrigger("ChangePhase");
        }

        public override void OnUpdate()
        {
            if (elapsed > 5)
                ReadyNextState();
        }

        public override void ReadyNextState()
        {
            nextState = new StateIdle(machine, player);
            isComplete = true;
        }
        public override void OnExit()
        {
            machine.HealFull();
        }
    }
}