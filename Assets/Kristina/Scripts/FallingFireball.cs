using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

namespace hatsune_miku
{
    public class FallingFireball : MonoBehaviour
    {
        [SerializeField] Transform fallFrom;
        [SerializeField] Transform fallTo;
        

        MeshRenderer mesh;
        SpriteRenderer targetSprite;
        bool stayOnGround;

        [SerializeField] float fallTime;
        float delay;

        private void Start()
        {
            mesh = GetComponent<MeshRenderer>();
            targetSprite = fallTo.GetComponent<SpriteRenderer>();
        }
        private void OnEnable()
        {
            delay = Random.value*2f; // delay from 0 - 2s

            stayOnGround = Random.value > .8f; //30% chance it stays on ground

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
            mesh.enabled = true;
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
            mesh.enabled = false;
            gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            transform.position = fallFrom.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            Damageable playerDamage = other.GetComponent<Damageable>();
            if (playerDamage == null)
                return;

            Damage damage = new Damage();
            damage.amount = 1;
            damage.direction = -playerDamage.transform.forward;
            damage.knockbackForce = 2;

            playerDamage.Hit(damage);
        }
    }
}