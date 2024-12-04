using UnityEngine;

namespace hatsune_miku
{
    public class StateAimedShot : BossState
    {
        public StateAimedShot(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }
        public override void OnEnter()
        {
            machine.anim.SetTrigger("AimedAttack");
        }

        public override void OnUpdate()
        {
            if (elapsed > 7f)
                ReadyNextState();

            Vector3 direction = player.position - machine.transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Quaternion newRotation = Quaternion.Slerp(machine.transform.rotation, rotation, .1f);
            machine.transform.rotation = newRotation;
        }

        public override void ReadyNextState()
        {
            nextState = new StateIdle(machine, player);
            isComplete = true;
        }
    }
}