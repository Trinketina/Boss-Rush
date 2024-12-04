using UnityEngine;

namespace hatsune_miku
{
    public class StateBodySlam : BossState
    {
        public StateBodySlam(BossManager _machine, Transform _player) : base(_machine, _player)
        {
            
        }

        public override void OnUpdate()
        {
            //give the animation enough time to play and enter idle
            if (elapsed > 4f)
                ReadyNextState();
        }

        public override void ReadyNextState()
        {
            nextState = new StateIdle(machine, player);
            isComplete = true;
        }
    }
}