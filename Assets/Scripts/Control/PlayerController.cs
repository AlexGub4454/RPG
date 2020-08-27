using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using UnityEngine.EventSystems;
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        RaycastHit raycastHit;
        Ray ray;
        Health health;
        [SerializeField]
        CursorMapping[] cursorMappings;
        [System.Serializable]
        public struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        public enum CursorType
        {
            None,
            Movement,
            Combat
        }

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead()) return;
          
            if(InteractWithCombat()) return;
           if(InteractWithMovement()) return;

            SetCursor(CursorType.Movement);
        }
        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {

                return true;
            }
            return false;
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
                SetCursor(CursorType.Combat);
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
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }
        private void SetCursor(CursorType type)
        {
            CursorMapping cursorMapping = GetCursorMapping(type);
            Cursor.SetCursor(cursorMapping.texture,cursorMapping.hotspot,CursorMode.Auto);
        }
        private CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach(CursorMapping cursorMapping in cursorMappings)
            {
                if (cursorMapping.cursorType == cursorType)
                {
                    return cursorMapping;
                }
            }
            return cursorMappings[0];
        }
        public static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition); 
        }
    }
}
