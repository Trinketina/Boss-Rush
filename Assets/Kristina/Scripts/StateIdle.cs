using UnityEngine;

namespace hatsune_miku
{
    public class StateIdle : BossState
    {
        public StateIdle(BossManager m) : base(m)
        {

        }

        public override void OnEnter()
        {
            machine.anim.SetTrigger("Idle");
        }

        public override void OnUpdate()
        {
            if (time > 5)
            {   
                switch (machine.phase)
                {
                    case 1:
                        nextState = new StateSlamAttack(machine);
                        break;
                    case 2:
                        nextState = new StateSwipeAttack(machine);
                        break;
                    case 3:
                        nextState = new StateBodySlam(machine);
                        break;
                }
                
                isComplete = true;
            }
        }
    }
}