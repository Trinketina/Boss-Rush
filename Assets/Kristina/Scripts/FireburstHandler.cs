using System.Collections.Generic;
using UnityEngine;

namespace hatsune_miku
{
    public class FireburstHandler : MonoBehaviour
    {
        [SerializeField] List<FallingFireball> fireballs = new List<FallingFireball>();
        [SerializeField] GameObject particles;
        public void PlayFireballBurst()
        {
            foreach (var fireball in fireballs)
            {
                fireball.StartFall();
            }
        }

        public void StartParticles()
        {
            particles.SetActive(true);
        }
        public void EndParticles()
        {
            particles.SetActive(false);
        }
    }


}