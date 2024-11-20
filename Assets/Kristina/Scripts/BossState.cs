namespace hatsune_miku
{
    public abstract class BossState
    {
        BossStateMachine machine;
        public BossState(BossStateMachine m)
        {
            machine = m;
        }

        public virtual void OnEnter()
        {

        }
        public abstract void OnUpdate();

        public virtual void OnExit()
        {

        }

    }
}