using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        RaycastHit raycastHit;
        Ray ray;
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            
            if(InteractWithCombat()) return;
           if(InteractWithMovement()) return;
           
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());

            foreach (var hit in hits) {
               
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (target == null) continue;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;    
            }
            return false;
        }
        bool InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                bool isRay = Physics.Raycast(GetRay(), out raycastHit);
                if (isRay)
                {
                    GetComponent<Movement.Mover>().StartMoveTo(raycastHit.point,1f);
                }
                return true;
            }
            return false;
        }
       
        public static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition); 
        }
    }
}
