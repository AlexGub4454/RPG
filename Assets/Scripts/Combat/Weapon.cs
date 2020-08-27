using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController weaponAnimControllerOverride = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";
        public float GetWeaponRange() => weaponRange;
        public float GetWeaponDamage() => weaponDamage;
        public void SwapWeapon(Transform rightHand,Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            Transform transformHand = GetTransformHand(rightHand, leftHand);
            if (weaponPrefab != null)
            {
             GameObject weapon =   Instantiate(weaponPrefab, transformHand);
             weapon.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (weaponAnimControllerOverride != null)
                animator.runtimeAnimatorController = weaponAnimControllerOverride;
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }
        void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
                Debug.Log("Work!&!");
            }
            if (oldWeapon == null) return;
            Debug.Log("a TUT Work!&!");
            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
            
           
        }
        public void LaunchProjectile(GameObject instigator, Transform rightHand,Transform leftHand,Health target,float rate)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransformHand(rightHand, leftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(instigator,target,weaponDamage,rate);
        } 
        private Transform GetTransformHand(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile()
        {
            return projectile != null ;
        }
    }

}