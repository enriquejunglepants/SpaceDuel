using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AI_Ship : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    [SerializeField] private float acceleration,thrust, friction;

    [SerializeField] private Vector3 turn,distance_between;

    public float turn_speed, thrust_thresh,fire_thresh,thrust_dist;
    private Transform ship_transform;
    public GameObject target;
    private Rigidbody m_Rigidbody;
    private Health health;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        health = GetComponentInChildren<Health>();

        ship_transform = GetComponentInChildren<Collider>().transform;
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (!bulletSpawn)
        {
            bulletSpawn = Instantiate(new GameObject("bulletSpawn"),transform).transform;

            bulletSpawn.position = transform.position + transform.forward * 10;
        }
    }

    void Update()
    {
        distance_between = ship_transform.InverseTransformPoint(target.transform.position);
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

            if (turn.magnitude < thrust_thresh && distance_between.magnitude>=thrust_dist)
            {
                thrust = 1;
            }

            if (turn.magnitude < fire_thresh)
            {
                Fire();
            }

            turn = new Vector3(-turn.y, turn.x, 0) * turn_speed;
        }
        
    }

    private void FixedUpdate()
    {
        m_Rigidbody.velocity *= (1 - friction);
        m_Rigidbody.angularVelocity *= (1 - friction);

        m_Rigidbody.AddForce(ship_transform.forward * acceleration*thrust, ForceMode.Impulse);
        ship_transform.Rotate(turn);

        //Fire();
    }

    void Fire()
    {
        // create a projectile
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        //bullet.transform.parent = bulletSpawn.transform.parent;
        //bullet.GetComponent<Rigidbody>().velocity = m_Rigidbody.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //health.Hit(10);
    }
}