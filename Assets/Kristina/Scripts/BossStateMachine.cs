using hatsune_miku;
public class BossStateMachine
{
    public BossState state;
    public BossStateMachine()
    {
        
    }

    public void Update()
    {
        state.OnUpdate();
    }

    public void ChangeState(BossState newState)
    {
        state.OnExit();
        state = newState;
        state.OnEnter();
    }
}
