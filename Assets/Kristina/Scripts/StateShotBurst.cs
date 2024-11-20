namespace hatsune_miku
{
    public class StateShotBurst : BossState
    {
        float delay = 3f;
        public StateShotBurst(BossManager m) : base(m)
        {
            machine.anim.Play("FireballBurst");
            
        }

        public override void OnUpdate()
        {
            if (time > delay)
            {
                isComplete = true;
                nextState = new StateIdle(machine);
            }       
        }
    }
}