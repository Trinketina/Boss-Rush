using UnityEngine;

namespace hatsune_miku
{
    public abstract class BossState
    {
        protected BossManager machine;

        public BossState nextState { get; protected set; } //should be set when isComplete becomes true
        public bool isComplete { get; protected set; } = false;

        protected float startTime;

        public float time => Time.time - startTime;

        public BossState(BossManager m)
        {
            machine = m;
            startTime = Time.time;
        }

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnExit() { }
    }
}