using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;

namespace RPG.Control {

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float mouseKeyDownSec = 0f;             //how long is the mouse key being held down.
        [SerializeField] private Combat.CombatTarget target;
        [SerializeField] float stopDistance = 0.5f;     //mouse holddown to move
        [SerializeField] float tabDownSpeed = 0.5f;     //mouse key tab down speed in sec.

        public bool inAttackMode = false;    
        public AttackBtn attackBtn;

        private Movement.Mover mover;
        private string pointedAt;
        private bool isMouseKeyDown = false;
        private bool isClicked = false;
        private bool isEnemy = false;
        private bool isUIObject = false;
        private bool isDraging = false;


        private void Awake()
        {
            mover = GetComponent<Movement.Mover>();
        }

        void Update()
        {
            MouseClickControl();
        }

        private void MouseClickControl()
        {

        //Set Control logic values*******************************************************
            //IsPointerOverGameObject, make sure we are not clicking on the UI item.
            if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
                isUIObject = true;
                isMouseKeyDown = false;
            } else if (Input.GetMouseButtonDown(0)){
                isUIObject = false;
                isMouseKeyDown = true;
            }
            //Debug.Log("PlayerController.MouseClickControl.isMouseKeyDown: " + isMouseKeyDown);

            
            //When clicked on the map or map object
            if (isMouseKeyDown) {

                //When Left Click is holding
                if (Input.GetMouseButton(0))
                {
                    mouseKeyDownSec += Time.deltaTime;

                    if (mouseKeyDownSec > tabDownSpeed)
                    {
                        isDraging = true;
                    }
                }

                Debug.Log("PlayerController.MouseClickControl.isMouseKeyDown:" + isMouseKeyDown.ToString());
                //Is Left clicked on a Monster
                if (Input.GetMouseButtonUp(0)) {
                    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                    foreach (RaycastHit hit in hits)
                    {
                        Debug.Log("hits: " + hit.transform.tag);
                        if (hit.transform.tag == "Enemy")
                        {
                            target = hit.transform.GetComponent<Combat.CombatTarget>();
                            isEnemy = true;
                        } else {isEnemy = false;}
                    }

                    //If clicked to move
                    if (!isEnemy) {
                        isClicked = true;
                    } else {
                        isClicked = false;
                    }
                    //reset variables on Key up
                    isDraging = false;
                    mouseKeyDownSec = 0f;
                    isMouseKeyDown = false;
                    isUIObject = false;
                }
            }
            
        //Run control Logic ************************************
            if (isUIObject){
                return;
            } else if (target != null && inAttackMode) {
                InteractWithCombat();
            } else if (isDraging || isClicked) {
                InteractWithMovement();
            }

            // if (InteractWithCombat()) return;
            // //Debug.Log("PlayerController.MouseClickControl: ");
            // else { InteractWithMovement(); }

            
        }

        private void InteractWithCombat()
        {
            // if (Input.GetMouseButtonDown(0)) // || Input.GetMouseButton(0))
            // {
            //     RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            //     foreach (RaycastHit hit in hits)
            //     {
            //         Debug.Log("hits: " + hit.transform.tag);
            //         if (hit.transform.tag == "Enemy")
            //         {
            //             //pointedAt = hit.transform.tag;
            //             target = hit.transform.GetComponent<Combat.CombatTarget>();
            //             break;
            //         }
            //         else { 

            //             //pointedAt = null; 
            //         }
            //     }
            //     attackBtn.setAttackModeOff();
            //     return false;  //Player pointed to another location, walking away from target, but keep selected target
            // }

            if (target != null && inAttackMode)
            {
                Debug.Log("Attack");
                mover.StopMove();
                GetComponent<Combat.Fighter>().Attack(target);
                //return true; //selected a target
            } else if (target != null)
            {
                Debug.Log("selected a target: " + target.name);
                //return true;
            }

            //return false; //no target
        }

        private void InteractWithMovement()
        {
            /*
            if (Input.GetMouseButton(0))
            {
                mouseKeyDownSec += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                //mouseKeyDownSec += Time.deltaTime;
                if (mouseKeyDownSec > tabDownSpeed && isClicked){
                    mover.StopMove();

                } else if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    //clicked = true;
                    //Debug.Log("mouse key Up");
                    MoveToCursor();
                }
                mouseKeyDownSec = 0f;
            }

            if (Input.GetMouseButton(0) && mouseKeyDownSec > 0.1f)
            {
                //Adding mouse drag time

                isClicked = true;

                //IsPointerOverGameObject, make sure we are not clicking on the UI item.
                //if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                //{
                    //clicked = true;
                    Debug.Log("mouse key draging");
                    MoveToCursor();
                //}

            }
            */

            //clicked but not draging, player walk to clicked location
            if(isClicked && !isDraging) {
                Debug.Log("mouse key Up");
                MoveToCursor();
            }//draging, play follows draging
            else if(isDraging) {
                Debug.Log("mouse key draging");
                MoveToCursor();
            } else { //clicked and was draged, stop player
                mover.StopMove();
            }

            //reset isClicked
            isClicked=false;
        }

        //move player to clicked location
        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = false;

            hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                mover.MoveTo(hit.point, stopDistance, "PlayerController.MoveToCursor");
            }

        }

        //make a ray cast to the click location to find the position.
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}