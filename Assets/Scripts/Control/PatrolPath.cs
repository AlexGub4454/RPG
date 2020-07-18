using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount ; i++)
            {
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(GetNext(i)));
            }
        }

        public int GetNext(int i)
        {
            return i == transform.childCount - 1 ? 0 : i+1;
        }
        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
