using Unity.VisualScripting;
using UnityEngine;

namespace hatsune_miku
{
    public class StateSlamAttack : BossState
    {
        float delay = 3f;
        public StateSlamAttack(BossManager m) : base(m)
        {
            
        }

        public override void OnEnter()
        {
            machine.anim.Play("Slam");
        }

        public override void OnUpdate()
        {
            if (time > delay)
            {
                nextState = new StateShotBurst(machine);
                isComplete = true;
            }
        }
    }
}