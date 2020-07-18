using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
namespace RPG.Control {
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float timeToCheck = 5f;
        [SerializeField] float timeToDwelling = 2f;
        [Range(0f,1f)]
        [SerializeField] float speedWalkRatio = 0.5f;
        [SerializeField] PatrolPath patrolPath;
        GameObject player;
        Fighter fighter;
        Health health;
        Vector3 guardPosition;
        float timeSinceSawPlayer = Mathf.Infinity;
        float timeSinceWentToPoint = Mathf.Infinity;
        float wayPointTolerance = 1f;
        int currentWayPiontIndex = 0;
        private void Start()

        {
            guardPosition = transform.position;
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
        }
        private void Update()
        {
            
            ShouldChase();
            timeSinceSawPlayer += Time.deltaTime;
            timeSinceWentToPoint += Time.deltaTime;
        }
        void ShouldChase()
        {

            if (health.IsDead())
            {
                GetComponent<ActionSchedule>().SetCurrentActionNull();
                return;
            }
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < chaseDistance && fighter.CanAttack(player))
            {
                timeSinceSawPlayer = 0f;
               fighter.Attack(player);
            }else if (timeSinceSawPlayer<timeToCheck)
            {
                GetComponent<ActionSchedule>().SetCurrentActionNull();
            }
            else
            {
                PatrolBehaviour(); 
            }
        }
        void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {

                if (Vector3.Distance(transform.position, patrolPath.GetWaypointPosition(currentWayPiontIndex)) < wayPointTolerance)
                {
                    timeSinceWentToPoint = 0;
                    currentWayPiontIndex = patrolPath.GetNext(currentWayPiontIndex);
                }
                nextPosition = patrolPath.GetWaypointPosition(currentWayPiontIndex);
                if (timeSinceWentToPoint > timeToDwelling)
                {
                    GetComponent<Movement.Mover>().StartMoveTo(nextPosition,speedWalkRatio);
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
