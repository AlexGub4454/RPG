using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction,ISaveable
    {
       

        [SerializeField] float timeBetweenAttack=1f;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon currentWeapon =null;
        float timeSinceLastAttack = Mathf.Infinity;
        Health target;
        private void Start()
        {
            if (currentWeapon==null)
            EquipWeapon(currentWeapon);
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
           
            if ( Vector3.Distance(gameObject.transform.position, target.transform.position) > currentWeapon.GetWeaponRange())
            { 
                GetComponent<Mover>().MoveTo(target.gameObject.transform.position,1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        
        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null) return;
            currentWeapon = weapon;
            currentWeapon.SwapWeapon(rightHandTransform,leftHandTransform, GetComponent<Animator>());
            
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                GetComponent<Animator>().ResetTrigger("someHappend");
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("someHappend");
            GetComponent<Animator>().ResetTrigger("attack");
            target = null;
        }
        public void Attack(GameObject combat)
        {
            target= combat.GetComponent<Health>();
            GetComponent<ActionSchedule>().StartAction(this);
        }
        void Hit()
        {
            if (target == null) return;
            if (currentWeapon.HasProjectile())
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            else
                target.TakeDamage(currentWeapon.GetWeaponDamage());
        }
        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}