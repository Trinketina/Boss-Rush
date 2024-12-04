using Cinemachine;
using System.Collections;
using System.Reflection;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        public int phase { get; private set; } = 1;
        
        private Damageable damageable;

        private BossState state;

        private void Start()
        {
            anim = GetComponent<Animator>();
            player = FindObjectOfType<PlayerLogic>().transform;

            damageable = GetComponent<Damageable>();

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

        public void PhaseHandler()
        {
            switch (phase)
            {
                case 1:
                    damageable.HealToFull();
                    phase = 2;
                    //StartCoroutine(StartPhaseTwo());
                    break;
                case 2:
                    damageable.HealToFull();
                    phase = 3;
                    //StartCoroutine(StartPhaseThree());
                    break;
                case 3:

                default:
                    break;
            }
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
        public IEnumerator StartPhaseTwo()
        {
            yield return new WaitForSeconds(.6f);

            Damage healthBoost = new Damage();
            healthBoost.amount = -300;
            healthBoost.direction = Vector3.zero;
            healthBoost.knockbackForce = 0;

            phase = 2;
        }

        public void PlayWingSwipe()
        {
            wingSwipe.PlaySwipe();
        }

        public void PlayAimedShot()
        {
            aimedFireball.StartLockedOnAttack(true);
        }

        public IEnumerator StartPhaseThree()
        {
            yield return new WaitForSeconds(.6f);

            Damage healthBoost = new Damage();
            healthBoost.amount = -300;
            healthBoost.direction = Vector3.zero;
            healthBoost.knockbackForce = 0;

            damageable.Hit(healthBoost);

            phase = 3;
        }
        public void PlayNoDodgeAimedShot()
        {
            aimedFireball.StartLockedOnAttack(false);
        }
    }
}