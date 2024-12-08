using UnityEngine;

namespace hatsune_miku
{
    public class BossManager : MonoBehaviour
    {
        
        private Transform player;

        //[SerializeField] private Transform head;

        [SerializeField] private Shockwave shockwave1;
        [SerializeField] private Shockwave shockwave2;

        [SerializeField] private FireburstHandler fireburstHandler;

        [SerializeField] private SwingSwipe wingSwipe;

        [SerializeField] private TargettingFireball aimedFireball;
        //public Transform Head { get; private set; }
        public Animator anim { get; private set; }

        public BossAudioHandler audioHandler { get; private set; }
        public CapsuleCollider bossCollider { get; private set; }
        public int phase { get; private set; } = 1;
        
        private Damageable damageable;


        private BossState state;

        /*private void Start()
        {
            
        }*/
        private void OnEnable()
        {
            bossCollider = GetComponent<CapsuleCollider>();
            anim = GetComponent<Animator>();
            audioHandler = GetComponent<BossAudioHandler>();
            player = FindObjectOfType<PlayerLogic>().transform;

            damageable = GetComponent<Damageable>();

            state = new StateWakeUp(this, player);
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

        public void PhaseHandler()
        {
            switch (phase)
            {
                case 1:
                    ChangeState(new StateChangePhase(this, player));
                    phase = 2;
                    //StartCoroutine(StartPhaseTwo());
                    break;
                case 2:
                    ChangeState(new StateChangePhase(this, player));
                    phase = 3;
                    //StartCoroutine(StartPhaseThree());
                    break;
                default:
                    ChangeState(new StateDeath(this, player));
                    break;
            }
        }
        public void HealFull()
        {
            damageable.HealToFull();
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
        public void PlayParticlesFireBurst()
        {
            fireburstHandler.StartParticles();
        }
        public void PlayFireBurst()
        {
            fireburstHandler.PlayFireballBurst();
        }
        public void StopParticlesFireBurst()
        {
            fireburstHandler.EndParticles();
        }

        public void PlayWingSwipe()
        {
            wingSwipe.PlaySwipe();
        }

        public void PlayAimedShot()
        {
            aimedFireball.StartLockedOnAttack(true);
        }
        public void PlayNoDodgeAimedShot()
        {
            aimedFireball.StartLockedOnAttack(false);
        }
    }
}