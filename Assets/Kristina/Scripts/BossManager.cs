using hatsune_miku;
using UnityEngine;
public class BossManager : MonoBehaviour 
{
    private BossState state;

    public Animator anim {get; private set;}
    public int phase { get; private set; } = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        state = new StateIdle(this);
        state.OnEnter();
    }

    public void Update()
    {
        state.OnUpdate();
        if (state.isComplete)
            ChangeState(state.nextState);
    }
    public void FixedUpdate()
    {
        state.OnFixedUpdate();
    }

    public void ChangeState(BossState newState)
    {
        state.OnExit();
        state = newState;
        state.OnEnter();
    }
}
