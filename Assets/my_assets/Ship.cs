using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Target))]
public class Ship : MonoBehaviour
{
    
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    private int selected_weapon = 0;

    public Transform bulletSpawn;

    public float velocity,acceleration;

    public float thrust;

    private Rigidbody m_Rigidbody;
    
    private float last_shot, fire_rate = 3;

    public Boolean firing, thrusting;

    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        if (!bulletSpawn)
        {
            bulletSpawn = Instantiate(new GameObject("bulletSpawn"), transform).transform;
            bulletSpawn.position = transform.position + transform.forward * 10;
        }
    }

    protected virtual void Update()
    {
        if (firing)
        {
            Fire();
        }

        velocity = m_Rigidbody.velocity.magnitude;
    }

    protected virtual void FixedUpdate()
    {
        //THRUSTERS
        if (thrusting)
        {
            m_Rigidbody.AddForce(transform.forward * acceleration * thrust, ForceMode.Impulse);
        }
    }

    protected void Fire()
    {
        if ((Time.time - last_shot) * fire_rate >= 1)
        {
            Instantiate(weapons[selected_weapon].gameObject,
                        bulletSpawn.position,
                        bulletSpawn.rotation);

            last_shot = Time.time;
        }
    }
}