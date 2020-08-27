using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Core
{
    public class Experience : MonoBehaviour,ISaveable
    {
        [SerializeField] float experience = 0f;
        public event Action onGainedExperience;
        public object CaptureState()
        {
            return experience;
        }

        public void GainExperience(float exp)
        {
            experience += exp;
            onGainedExperience();
        }

        public void RestoreState(object state)
        {
            experience = (float)state;
        }
    }
}
