using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    public class WeaponPickUp : MonoBehaviour
    {
        Fighter fighter;
        [SerializeField] Weapon weapon;

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider objectCollider)
        {

            if (objectCollider.gameObject.tag != "Player") return;
            fighter = objectCollider.gameObject.GetComponent<Fighter>();
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(4f));
            //Destroy(gameObject);
        }
        IEnumerator HideForSeconds(float seconds)
        {
            Hiding();
            yield return new WaitForSeconds(seconds);
            Showing();
        }

        private void Showing()
        {
            gameObject.GetComponent<Collider>().enabled = true;
            foreach (Transform gameObject in transform)
            {
                gameObject.gameObject.SetActive(true);
            }
        }

        private void Hiding()
        {
            gameObject.GetComponent<Collider>().enabled = false;
            foreach (Transform gameObject in transform)
            {
                gameObject.gameObject.SetActive(false);
            }
        }
    }
}
