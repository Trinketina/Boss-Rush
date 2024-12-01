using Cinemachine;
using System.Collections;
using TreeEditor;
using UnityEngine;
using UnityEngine.Splines;

namespace hatsune_miku
{
    public class Shockwave : MonoBehaviour
    {
        [SerializeField] SplineContainer spline;
        [SerializeField] SphereCollider attacker;
        [SerializeField] GameObject particles;
        Transform player;
        ShakeHandler screenshaker;

        [SerializeField] float timeLength;

        private void Start()
        {
            player = FindObjectOfType<PlayerLogic>().transform;
            screenshaker = FindObjectOfType<ShakeHandler>();
        }
        public void PlayShockwave()
        {
            StartCoroutine(MoveAttacker());
            screenshaker.ScreenShake(3f);
            Instantiate(particles, spline.transform);
        }

        IEnumerator MoveAttacker()
        {
            float time = 0;
            attacker.enabled = true;
            while (time < timeLength)
            {
                
                time += Time.deltaTime;

                Vector3 playerPos = player.position;
                playerPos.y = 0;

                Quaternion lookRot = Quaternion.LookRotation(playerPos - spline.transform.position);
                spline.transform.rotation = lookRot;

                attacker.transform.position = spline.EvaluatePosition(time / timeLength);

                yield return new WaitForFixedUpdate();
            }

            attacker.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Damageable playerDamage = other.GetComponent<Damageable>();
            if (playerDamage == null)
                return;

            Damage damage = new Damage();
            damage.amount = 1;
            damage.direction = spline.transform.forward + Vector3.up;
            damage.knockbackForce = 2;

            playerDamage.Hit(damage);
        }
    }
}