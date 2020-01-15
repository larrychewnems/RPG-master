using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{

    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        
        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void LateUpdate()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination, float stopDistance, string testMessage)
        {
            //Debug.Log("Mover.MoveTo.testMessage: " + testMessage + ", destination: " + destination);
            navMeshAgent.stoppingDistance = stopDistance;
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
        }

        public void StopMove()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
