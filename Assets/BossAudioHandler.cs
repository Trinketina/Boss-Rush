using UnityEngine;

namespace hatsune_miku
{
    public class BossAudioHandler : MonoBehaviour
    {
        [SerializeField] AudioClip wingFlap;
        [SerializeField] AudioClip dragonScream;
        [SerializeField] AudioClip dragonGrowl;

        [SerializeField] AudioClip lightWoosh;
        [SerializeField] AudioClip heavyWoosh;

        [SerializeField] AudioClip chargedFireball;

        [SerializeField] AudioClip wingSmash;
        [SerializeField] AudioClip bodySlam;

        public float timeSinceLastPlayed { get; private set; } = 0;

        private void Update()
        {
            timeSinceLastPlayed += Time.deltaTime;
        }
        public void PlayWingFlap()
        {
            SoundEffectsManager.instance.PlayAudioClip(lightWoosh, true);
            SoundEffectsManager.instance.PlayAudioClip(wingFlap, true);
        }
        public void PlayHeavyFlap()
        {
            SoundEffectsManager.instance.PlayAudioClip(heavyWoosh, true);
            SoundEffectsManager.instance.PlayAudioClip(wingFlap, true);
        }
        public void PlayDragonScream()
        {
            timeSinceLastPlayed = 0;
            SoundEffectsManager.instance.PlayAudioClip(dragonScream, true);
        }
        public void PlayGrowl()
        {
            timeSinceLastPlayed = 0;
            SoundEffectsManager.instance.PlayAudioClip(dragonGrowl, true);
        }
        public void PlayChargedFireball()
        {
            SoundEffectsManager.instance.PlayAudioClip(chargedFireball, true);
        }
        public void PlayWingSmash()
        {
            SoundEffectsManager.instance.PlayAudioClip(wingSmash, true);
        }
        public void PlayBodySlam()
        {
            SoundEffectsManager.instance.PlayAudioClip(bodySlam, true);
        }
    }
}