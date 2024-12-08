using UnityEngine;

namespace hatsune_miku
{
    public class StateWalk : BossState
    {
        Rigidbody rb;
        public StateWalk(BossManager _machine, Transform _player) : base(_machine, _player)
        {
            rb = _machine.GetComponent<Rigidbody>();
        }
        public override void OnEnter()
        {
            machine.anim.Play("Walk");
        }
        public override void OnUpdate()
        {
            Vector3 machinePos = machine.transform.position;
            machinePos.y = 0;
            Vector3 playerPos = player.transform.position;
            playerPos.y = 0;

            Vector3 direction = playerPos - machinePos;
            direction.y = 0;

            Quaternion rotation = Quaternion.LookRotation(direction);

            machine.transform.rotation = Quaternion.Slerp(machine.transform.rotation, rotation, .1f);

            int speed = 2;
            machine.transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(playerPos, machinePos) < 8f)
            {
                ReadyNextState();
            }
        }
        public override void ReadyNextState()
        {
            nextState = new StateIdle(machine, player);
            isComplete = true;
        }
        public override void OnExit()
        {
            machine.transform.position = machine.transform.position;
        }
    }
}