﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mouseKeyDownSec = 0f;            //how long is the mouse key being held down.
    private Mover mover;

    [SerializeField] float stopDistance = 0.5f;
    [SerializeField] float tabDownSpeed = 0.5f;    //mouse key tab down speed in sec.

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            mouseKeyDownSec += Time.deltaTime;
            //Debug.Log("mouse held time: " + mouseKeyDownSec);
            MoveToCursor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (mouseKeyDownSec > tabDownSpeed)
            {
                mover.StopMove();
            }
            mouseKeyDownSec = 0f;

        }
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = false;

        hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            mover.MoveTo(hit.point, stopDistance);
        }

    }
}
