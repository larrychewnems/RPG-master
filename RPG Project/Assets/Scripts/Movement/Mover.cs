using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{

    public class Mover : MonoBehaviour
    {
        //private float mouseKeyDownSec = 0f;            //how long is the mouse key being held down.

        //[SerializeField] float stopDistance = 0.5f;
        //[SerializeField] float tabDownSpeed = 0.5f;    //mouse key tab down speed in sec.

        //void LateUpdate()
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        mouseKeyDownSec += Time.deltaTime;
        //        //Debug.Log("mouse held time: " + mouseKeyDownSec);
        //        MoveToCursor(); 
        //    } 

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        if (mouseKeyDownSec > tabDownSpeed)
        //        {
        //            StopMove();
        //        }
        //        mouseKeyDownSec = 0f;

        //    }

        //    UpdateAnimator();
        //}

        //private void MoveToCursor()
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    bool hasHit = false;

        //    hasHit = Physics.Raycast(ray, out hit);

        //    if (hasHit)
        //    {
        //        MoveTo(hit.point);
        //    }

        //}

        private void LateUpdate()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination, float stopDistance)
        {
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
