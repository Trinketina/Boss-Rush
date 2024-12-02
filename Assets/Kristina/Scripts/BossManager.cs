using Cinemachine;
using TreeEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace hatsune_miku
{
    public class BossManager : MonoBehaviour
    {
        
        private Transform player;

        [SerializeField] private Transform head;

        [SerializeField] private Shockwave shockwave1;
        [SerializeField] private Shockwave shockwave2;

        [SerializeField] private FireburstHandler fireburstHandler;

        [SerializeField] private SwingSwipe wingSwipe;
        public Transform Head { get; private set; }
        public Animator anim { get; private set; }
        public int phase { get; private set; } = 2;

        private BossState state;

        private void Start()
        {
            anim = GetComponent<Animator>();
            player = FindObjectOfType<PlayerLogic>().transform;
            Head = head;
            state = new StateIdle(this, player);
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

        void ChangeState(BossState newState)
        {
            state.OnExit();
            state = newState;
            state.OnEnter();
        }
        public void PlayShockwaveOne()
        {
            shockwave1.PlayShockwave();
        }
        public void SetAnimationSpeed(float speed)
        {
            anim.speed = speed;
        }
        public void PlayShockwaveTwo()
        {
            shockwave2.PlayShockwave();
        }

        public void PlayFireBurst()
        {
            fireburstHandler.PlayFireballBurst();
        }

        public void PlayWingSwipe()
        {
            wingSwipe.PlaySwipe();
        }
    }
}