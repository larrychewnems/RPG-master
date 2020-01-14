using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{

    public class Mover : MonoBehaviour
    {
        void LateUpdate()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination, float stopDistance, string testMessage)
        {
            //Debug.Log("Mover.MoveTo.testMessage: " + testMessage + ", destination: " + destination);
            GetComponent<NavMeshAgent>().stoppingDistance = stopDistance;
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().destination = destination;
        }

        public void StopMove()
        {

            GetComponent<NavMeshAgent>().isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
