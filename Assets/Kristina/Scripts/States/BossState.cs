using UnityEngine;

namespace hatsune_miku
{
    public abstract class BossState
    {
        protected BossManager machine;
        protected Transform player;

        public BossState nextState { get; protected set; } //should be set when isComplete becomes true
        public bool isComplete { get; protected set; } = false;

        protected float startTime;

        public float elapsed => Time.time - startTime;

        public BossState(BossManager _machine, Transform _player)
        {
            machine = _machine;
            player = _player;
            startTime = Time.time;
        }

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void ReadyNextState() { }

        public virtual void OnExit() { }
    }
}