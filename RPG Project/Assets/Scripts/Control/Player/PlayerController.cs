using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {

    public class PlayerController : MonoBehaviour
    {
        private float mouseKeyDownSec = 0f;            //how long is the mouse key being held down.
        private RPG.Movement.Mover mover;
        private bool clicked = false;
        private bool isEnemy = false;

        [SerializeField] float stopDistance = 0.5f;
        [SerializeField] float tabDownSpeed = 0.5f;    //mouse key tab down speed in sec.

        private void Awake()
        {
            mover = GetComponent<RPG.Movement.Mover>();
        }

        void LateUpdate()
        {
            MouseClickControl();
        }

        private void MouseClickControl()
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach (RaycastHit item in hits)
                {
                    if (item.transform.tag == "Enemy")
                    {
                        isEnemy = true;
                    } else {isEnemy = false;}
                }
            }

            if (isEnemy)
            {
                mover.StopMove();
                InteractWithCombat();
            }
            else { InteractWithMovement(); }
        }

        private void InteractWithCombat()
        {
            Debug.Log("clicked on an Enemy!");

        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButtonUp(0))
            {
                //mouseKeyDownSec += Time.deltaTime;
                if (mouseKeyDownSec > tabDownSpeed && clicked){
                    mover.StopMove();

                } else if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    //clicked = true;
                    MoveToCursor();
                }
                mouseKeyDownSec = 0f;
            }

            if (Input.GetMouseButton(0))
            {
                mouseKeyDownSec += Time.deltaTime;
                //if (mouseKeyDownSec > tabDownSpeed && clicked)
                //{
                    clicked = true;

                //}
                //mouseKeyDownSec = 0f;

                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    //clicked = true;
                    MoveToCursor();
                }

            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = false;

            hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                mover.MoveTo(hit.point, stopDistance);
            }

        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}