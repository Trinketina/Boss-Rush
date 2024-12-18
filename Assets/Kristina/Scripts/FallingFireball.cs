using System.Collections;
using UnityEngine;

namespace hatsune_miku
{
    public class FallingFireball : MonoBehaviour
    {
        [SerializeField] AudioClip fireballHitSound;
        [SerializeField] Transform fallFrom;
        [SerializeField] Transform fallTo;

        Transform boss;

        [SerializeField] GameObject fireballMesh;
        SphereCollider scollider;
        SpriteRenderer targetSprite;
        bool stayOnGround;
        bool collided = false;

        [SerializeField] float fallTime;
        float delay;

        private void Start()
        {
            scollider = GetComponent<SphereCollider>();
            targetSprite = fallTo.GetComponent<SpriteRenderer>();
            boss = FindObjectOfType<BossManager>().transform;
        }

        public void StartFall()
        {
            delay = Random.value * 2f; // delay from 0 - 2s

            stayOnGround = Random.value > .8f; //30% chance it stays on ground
            collided = false;
            transform.position = fallFrom.position;
            StartCoroutine(Fall());
        }

        private IEnumerator Fall()
        {
            float elapsed = 0;
            while (elapsed < delay)
            {
                elapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
                
            }
            elapsed = 0;
            fireballMesh.SetActive(true);
            scollider.enabled = true;
            targetSprite.enabled = true;
            while (elapsed < fallTime)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(fallFrom.position, fallTo.position, elapsed / fallTime);
                yield return new WaitForEndOfFrame();
            }

            targetSprite.enabled = false;

            if (stayOnGround)
            {
                elapsed = 0;
                while (elapsed < fallTime)
                {
                    elapsed += Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }
            }
            ResetFall();

        }
        private IEnumerator FollowBoss()
        {
            while (!collided)
            {
                float speed = 15f;

                transform.LookAt(boss);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            ResetFall();
        }

        private void ResetFall()
        {
            targetSprite.enabled = false;

            fireballMesh.SetActive(false);
            scollider.enabled = false;

            gameObject.layer = 9;
            
            transform.position = fallFrom.position;
            transform.rotation = fallTo.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable.GetComponent<PlayerLogic>() != null)
            {
                Damage damage = new Damage();
                damage.amount = 1;
                damage.direction = -damageable.transform.forward;
                damage.knockbackForce = 2;

                if (damageable.Hit(damage))
                    SoundEffectsManager.instance.PlayAudioClip(fireballHitSound, true);
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
        public void HitBack()
        {
            transform.position += Vector3.up;
                
            gameObject.layer = 8; //swap to PlayerDamage

            StopAllCoroutines();
            targetSprite.enabled = false;
            StartCoroutine(FollowBoss());
        }
    }
}