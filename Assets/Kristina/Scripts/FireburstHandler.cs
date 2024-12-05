using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hatsune_miku
{
    public class FireburstHandler : MonoBehaviour
    {
        [SerializeField] List<FallingFireball> fireballs = new List<FallingFireball>();
        public void PlayFireballBurst()
        {
            foreach (var fireball in fireballs)
            {
                fireball.StartFall();
            }
        }
    }
}