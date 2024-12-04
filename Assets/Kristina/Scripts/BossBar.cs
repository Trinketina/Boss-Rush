using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace hatsune_miku
{
    public class BossBar : MonoBehaviour
    {
        [SerializeField] Image barFill;

        int maximumValue;

        public void SetMax(int max)
        {
            maximumValue = max - 1000; //subtract the hidden 1000 value
        }

        public void UpdateBar(int change, int newCurrent)
        {
            barFill.fillAmount = (newCurrent - 1000 ) / (float)maximumValue;
        }
    }
}