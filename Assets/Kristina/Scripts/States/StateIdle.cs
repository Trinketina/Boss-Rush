using Unity.VisualScripting;
using UnityEngine;

namespace hatsune_miku
{
    public class StateIdle : BossState
    {
        public StateIdle(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }

        public override void OnEnter()
        {
            if (machine.audioHandler.timeSinceLastPlayed > 10)
                machine.audioHandler.PlayGrowl();
            machine.anim.SetTrigger("Idle");
        }

        public override void OnUpdate()
        {
            Vector3 direction = player.position - machine.transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Quaternion newRotation = Quaternion.Slerp(machine.transform.rotation, rotation, .1f);
            if (machine.transform.rotation != newRotation)
            {
                machine.transform.rotation = newRotation;
                machine.anim.SetBool("Rotating", true);
            }
            else
            {
                machine.anim.SetBool("Rotating", false);
                machine.anim.SetTrigger("Idle");
            }
            
            if (Vector3.Distance(machine.transform.position, player.transform.position) > 12f && elapsed < 2.5f)
            {
                nextState = new StateWalk(machine, player);
                isComplete = true;
            }

            if (elapsed > 5)
            {
                machine.anim.SetBool("Rotating", false);
                ReadyNextState();
            }
        }

        public override void ReadyNextState()
        {
            switch (machine.phase)
            {
                case 1:
                    nextState = new StateSlamAttack(machine, player);
                    break;
                case 2:
                    nextState = new StateSwipeAttack(machine, player);
                    break;
                case 3:
                    nextState = new StateSwipeAttack(machine, player);
                    break;
            }

            isComplete = true;
        }

        public override void OnExit()
        {
            machine.anim.ResetTrigger("Idle");
            machine.anim.SetBool("Rotating", false);
        }
    }
}