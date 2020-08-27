using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{

    public class Health : MonoBehaviour,ISaveable
    {
        float healthLevelUp = 0.7f;
        [SerializeField]float health = 10f;
        bool isDead= false;
        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += HealthLevelUp;
          // if (health<0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
        }
        public void TakeDamage(GameObject instigator, float damage,float rate)
        {
            health = Mathf.Max(health - damage*rate, 0);
            if (health == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }
        private void HealthLevelUp()
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Health)*healthLevelUp;
        }
        public float GetPersentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
        public bool IsDead()
        {
            return isDead;
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionSchedule>().SetCurrentActionNull();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0)
            {
                Die();
            }
        }
    }
}
