using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{


    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour,IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 5.66f;
        NavMeshAgent meshAgent;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            meshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
           
                meshAgent.enabled = !health.IsDead();
            
                UpdateAnimation();
        }
        void UpdateAnimation()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = gameObject.transform.InverseTransformVector(velocity);
            GetComponent<Animator>().SetFloat("speed", localVelocity.z);
        }
        
        public void Cancel()
        {
            meshAgent.isStopped = true;
        }
        public void StartMoveTo(Vector3 dest,float speedRatio)
        {
            GetComponent<ActionSchedule>().StartAction(this);
            MoveTo(dest, speedRatio);
        }
        public void MoveTo(Vector3 point, float speedRatio)
        {
            meshAgent.speed = maxSpeed * Mathf.Clamp01(speedRatio);
            meshAgent.destination = point;
            meshAgent.isStopped = false;   
        }

        object  ISaveable.CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        void ISaveable.RestoreState(object state)
        {
            SerializableVector3 vector = (SerializableVector3) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = vector.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}