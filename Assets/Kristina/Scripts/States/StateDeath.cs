using UnityEngine;

namespace hatsune_miku
{
    public class StateDeath : BossState
    {
        public StateDeath(BossManager _machine, Transform _player) : base(_machine, _player)
        {

        }
        public override void OnEnter()
        {
            BackgroundMusic.FindObjectOfType<BackgroundMusic>().FadeOut();
            machine.anim.SetTrigger("Die");
        }

        public override void ReadyNextState()
        {
            //there is no next state . . .
        }
    }
}