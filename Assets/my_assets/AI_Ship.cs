using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AI_Ship : Ship
{

    [SerializeField] private Vector3 turn,distance_between;

    public float turn_speed, thrust_thresh,fire_thresh,thrust_dist;

    //private Transform ship_transform;

    public GameObject target;

    protected override void Start()
    {
        base.Start();

        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }


    protected override void Update()
    {
        base.Update();

        distance_between = transform.InverseTransformPoint(target.transform.position);
        turn = distance_between;

        thrust = 0;

        if (turn.z <= 0)
        {
            turn = new Vector3(0, turn_speed, 0);
        }
        else
        {
            //turn.z = 0;
            turn.Normalize();
            turn.z = 0;

            if (turn.magnitude < thrust_thresh && distance_between.magnitude >= thrust_dist)
            {
                thrust = 1;
                thrusting = true;
            }

            if (turn.magnitude < fire_thresh)
            {
                firing = true;
            }

            turn = new Vector3(-turn.y, turn.x, 0) * turn_speed;
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.Rotate(turn);
    }
    
}