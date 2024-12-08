using System.Collections;
using UnityEngine;

namespace hatsune_miku
{
    public class TargettingFireball : MonoBehaviour
    {
        [SerializeField] AudioClip fireballHitSound;
        bool canDodge = true;
        [SerializeField] Transform initialLocation;

        Transform player;
        CapsuleCollider boss;
        [SerializeField] GameObject dodgeableMesh;
        [SerializeField] GameObject lockOnMesh;
        SphereCollider scollider;
        Damager damager;

        //bool dodged = false;
        bool collided = false;

        private void Start()
        {
            player = FindObjectOfType<PlayerLogic>().transform;
            damager = GetComponent<Damager>();
            boss = FindObjectOfType<BossManager>().bossCollider;
            scollider = GetComponent<SphereCollider>();
        }

        public void StartLockedOnAttack(bool dodgeable)
        {
            canDodge = dodgeable;
            transform.position = initialLocation.position;
            transform.rotation = initialLocation.rotation;

            if (canDodge)
                dodgeableMesh.SetActive(true);
            else
                lockOnMesh.SetActive(true);
            scollider.enabled = true;
            //dodged = false;
            collided = false;

            ShakeHandler.ScreenShake(2f);
            StartCoroutine(FollowPlayer());
        }

        private IEnumerator FollowPlayer()
        {
            transform.LookAt(player);

            while (!collided)
            {
                float speed = 10f;

                if (!canDodge)
                    transform.LookAt(player);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                if (transform.position.y < 0)
                {
                    ResetFireball();
                    //dodged = false;
                    break;
                }

                yield return new WaitForEndOfFrame();
            }
            /*if (dodged)
            {
                float elapsed = 0;
                Vector3 crashPos = transform.position;
                crashPos.y = .2f;
                Vector3 currPos = transform.position;
                float length = 1f;
                while (elapsed < length)
                {
                    Vector3 newPos = Vector3.Slerp(currPos, crashPos, elapsed / length);
                    transform.rotation = Quaternion.LookRotation(newPos - transform.position);
                    transform.position = crashPos;

                    elapsed += Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }
            }*/
            //else
            {
                ResetFireball();
            }
        }

        private IEnumerator FollowBoss()
        {
            if (boss == null)
                boss = FindObjectOfType<BossManager>().bossCollider;
            while (!collided)
            {
                float speed = 15f;

                transform.LookAt(boss.center + boss.transform.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            ResetFireball();
        }

        private void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable.GetComponent<PlayerLogic>() != null)
            {
                Damage damage = new Damage();
                damage.amount = 1;
                damage.direction = transform.forward + Vector3.up;
                damage.knockbackForce = 2;

                damageable.Hit(damage);
                /*if (!damageable.Hit(damage) && canDodge)
                    dodged = true;*/
                SoundEffectsManager.instance.PlayAudioClip(fireballHitSound, true);
                collided = true;
            }
            else if (damageable.GetComponent<BossManager>() != null)
            {
                collided = true;

                gameObject.layer = 9; //return to enemyDamage

                Damage damage = new Damage();
                damage.amount = 100;
                damage.direction = Vector3.zero;
                damage.knockbackForce = 0;

                damageable.Hit(damage);
                SoundEffectsManager.instance.PlayAudioClip(fireballHitSound, true);
            }
            ShakeHandler.ScreenShake(2f);
        }

        private void ResetFireball()
        {
            dodgeableMesh.SetActive(false);
            lockOnMesh.SetActive(false);
            scollider.enabled = false;

            transform.position = initialLocation.position;
            transform.rotation = initialLocation.rotation;
        }

        public void HitBack()
        {
            collided = false;
            if (!scollider.enabled)
                return;

            if (dodgeableMesh.activeInHierarchy)
                transform.position += Vector3.up;
            gameObject.layer = 8; //swap to PlayerDamage

            StopAllCoroutines();
            StartCoroutine(FollowBoss());
        }
    }
}