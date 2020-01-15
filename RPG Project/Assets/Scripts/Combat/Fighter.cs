using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour
    {
        private Movement.Mover mover;
        [SerializeField] private Transform target;

        [SerializeField] float weaponRange = 2f;

        void LateUpdate() {
            
        }

       public void Attack(CombatTarget combatTarget){
            Debug.Log("Take that you short");
            target = combatTarget.GetComponent<Transform>();
            if (target)
            {
                //Debug.Log("Fighter Update");
                GetComponent<Mover>().MoveTo(target.transform.position, weaponRange, "Fighter.Update");
            }
            
       }
    }
}


