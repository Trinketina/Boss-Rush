using System.Collections;
using UnityEngine;

namespace hatsune_miku
{
    public class SwingSwipe : MonoBehaviour
    {
        [SerializeField] GameObject particles;
        [SerializeField] float attackTime;

        private BoxCollider boxcollider;
        private void Start()
        {
            boxcollider = GetComponent<BoxCollider>();
        }
        public void PlaySwipe()
        {
            Instantiate(particles, this.transform);
            ShakeHandler.ScreenShake(3f);
            StartCoroutine(AttackTrigger());
        }

        private IEnumerator AttackTrigger()
        {
            boxcollider.enabled = true;

            float elapsed = 0;
            while (elapsed < attackTime)
            {
                elapsed += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            boxcollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            Damageable playerDamage = other.GetComponent<Damageable>();
            if (playerDamage == null)
                return;

            Damage damage = new Damage();
            damage.amount = 1;
            damage.direction = -playerDamage.transform.forward + Vector3.up;
            damage.knockbackForce = 2;

            playerDamage.Hit(damage);
        }
    }
}