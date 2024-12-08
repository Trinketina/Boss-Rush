using UnityEngine;

namespace hatsune_miku
{
    public class StateWakeUp : BossState
    {
        BackgroundMusic bgm;
        public StateWakeUp(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }
        public override void OnEnter()
        {
            machine.anim.SetTrigger("Idle");
        }

        public override void OnUpdate()
        {
            if (elapsed > 3)
            {
                ReadyNextState();
            }
        }

        public override void ReadyNextState()
        {
            nextState = new StateIdle(machine, player);
            isComplete = true;
        }
    }
}