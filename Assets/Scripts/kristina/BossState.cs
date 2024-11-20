using UnityEngine;

namespace hatsune_miku
{
    public abstract class BossState : MonoBehaviour
    {
        public virtual void ChangeState(BossState state)
        {

        }

        public abstract void RunState();

    }
}