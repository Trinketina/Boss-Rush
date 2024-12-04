using hatsune_miku;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Splines;

public class TargettingFireball : MonoBehaviour
{
    bool canDodge = true;
    [SerializeField] Transform initialLocation;

    Transform player;
    Transform boss;
    [SerializeField] MeshRenderer dodgeableMesh;
    [SerializeField] MeshRenderer hitOnlyMesh;
    SphereCollider scollider;

    bool dodged = false;
    bool collided = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerLogic>().transform;
        boss = FindObjectOfType<BossManager>().transform;
        scollider = GetComponent<SphereCollider>();
    }

    public void StartLockedOnAttack(bool dodgeable)
    {
        canDodge = dodgeable;
        transform.position = initialLocation.position;
        transform.rotation = initialLocation.rotation;

        if (canDodge)
            dodgeableMesh.enabled = true;
        else
            hitOnlyMesh.enabled = true;
        scollider.enabled = true;
        dodged = false;
        collided = false;
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        float elapsed = 0;

        float length = 1.5f;
        while (!collided)
        {
            elapsed += Time.deltaTime;
            float speed = 10f;
            /*Vector3 newPos = Vector3.Slerp(initialLocation.position, player.position, elapsed / length);
            transform.rotation = Quaternion.LookRotation(newPos - transform.position);
            transform.position = newPos;*/

            transform.LookAt(player);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        if (dodged)
        {
            elapsed = 0;
            Vector3 crashPos = transform.position;
            crashPos.y = .2f;
            Vector3 currPos = transform.position;
            length = 1f;
            while (elapsed < length)
            {
                Vector3 newPos = Vector3.Slerp(currPos, crashPos, elapsed / length);
                transform.rotation = Quaternion.LookRotation(newPos - transform.position);
                transform.position = crashPos;

                elapsed += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            ResetFireball();
        }
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

            if (!damageable.Hit(damage) && canDodge)
                dodged = true;

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
        }
        
    }

    private void ResetFireball()
    {
        dodgeableMesh.enabled = false;
        hitOnlyMesh.enabled = false;
        scollider.enabled = false;

        transform.position = initialLocation.position;
        transform.rotation = initialLocation.rotation;
    }

    public void HitBack()
    {
        collided = false;
        if (!scollider.enabled)
            return;

        if (dodgeableMesh.enabled)
            transform.position += Vector3.up;
        gameObject.layer = 8; //swap to PlayerDamage

        StopAllCoroutines();
        StartCoroutine(FollowBoss());
    }
}
