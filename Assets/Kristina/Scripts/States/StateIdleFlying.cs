using UnityEngine;

namespace hatsune_miku
{
    public class StateIdleFlying : BossState
    {
        public StateIdleFlying(BossManager _machine, Transform _player) : base(_machine, _player)
        {
            
        }

        public override void OnEnter()
        {
            machine.anim.SetTrigger("IdleFly");
        }

        public override void OnUpdate()
        {
            Vector3 direction = player.position - machine.transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Quaternion newRotation = Quaternion.Slerp(machine.transform.rotation, rotation, .1f);
            machine.transform.rotation = newRotation;

            if (elapsed > 5)
            {
                machine.anim.SetBool("Rotating", false);
                ReadyNextState();
            }
        }

        public override void ReadyNextState()
        {
            nextState = new StateSwipeAttack(machine, player);
            
            isComplete = true;
        }
    }
}