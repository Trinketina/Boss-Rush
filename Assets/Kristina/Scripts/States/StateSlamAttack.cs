using Unity.VisualScripting;
using UnityEngine;

namespace hatsune_miku
{
    public class StateSlamAttack : BossState
    {
        float delay = 3f;
        public StateSlamAttack(BossManager _machine, Transform _player) : base(_machine, _player)
        {
            
        }

        public override void OnEnter()
        {
            machine.anim.Play("Slam");
        }

        public override void OnUpdate()
        {
            if (elapsed > delay)
            {
                nextState = new StateShotBurst(machine, player);
                isComplete = true;
            }
        }
    }
}