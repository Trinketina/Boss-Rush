using UnityEngine;

namespace hatsune_miku
{
    public class StateShotBurst : BossState
    {
        float delay = 3f;
        public StateShotBurst(BossManager _machine, Transform _player) : base(_machine, _player)
        {
            machine.anim.Play("FireballBurst");
        }

        public override void OnUpdate()
        {
            if (elapsed > delay)
            {
                isComplete = true;
                nextState = new StateIdle(machine, player);
            }       
        }
    }
}