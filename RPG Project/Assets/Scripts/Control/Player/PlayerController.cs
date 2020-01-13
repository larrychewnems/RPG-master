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
        private RPG.Combat.CombatTarget target;

        [SerializeField] float stopDistance = 0.5f;    //mouse holddown to move
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
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        target = hit.transform.GetComponent<RPG.Combat.CombatTarget>();
                        break;
                    } else {target= null;}
                }
            }

            if (target)
            {
                mover.StopMove();
                InteractWithCombat(target);
            }
            else { InteractWithMovement(); }
        }

        private void InteractWithCombat(RPG.Combat.CombatTarget target)
        {
            GetComponent<RPG.Combat.Fighter>().Attack(target);

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
                //Adding mouse drag time
                mouseKeyDownSec += Time.deltaTime;
                clicked = true;

                //IsPointerOverGameObject, make sure we are not clicking on the UI item.
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    //clicked = true;
                    MoveToCursor();
                }

            }
        }

        //move player to clicked location
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

        //make a ray cast to the click location to find the position.
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}