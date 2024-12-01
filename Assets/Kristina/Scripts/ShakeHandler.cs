using Cinemachine;
using UnityEngine;

namespace hatsune_miku
{
    public class ShakeHandler : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera vcam;
        private CinemachineBasicMultiChannelPerlin perlin;

        private void Start()
        {
            perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        // Update is called once per frame
        void Update()
        {
            if (perlin.m_AmplitudeGain > 0)
                perlin.m_AmplitudeGain -= Time.deltaTime;
        }

        public void ScreenShake(float amp)
        {
            perlin.m_AmplitudeGain = amp;
        }
    }
}