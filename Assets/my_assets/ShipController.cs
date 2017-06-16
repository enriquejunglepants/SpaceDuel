using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{
    
    public ParticleSystem spacedust;

    public Button fireButton;

    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public Animation warp_animation;

    
    public float velocity;

    [SerializeField] private float acceleration;
    [SerializeField] private float friction;

    public float thrust;

    //private float max_speed = 0;
    private Camera m_Camera;

    private Rigidbody m_Rigidbody;
    private Health health;
    
    private float last_shot, fire_rate = 3;

    public Boolean firing,thrusting;

    void Start()
    {
        Input.gyro.enabled = true;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Camera = Camera.main;
        //spacedust = GetComponentInChildren<ParticleSystem>();
        health = GetComponentInChildren<Health>();

        if (!bulletSpawn)
        {
            bulletSpawn = Instantiate(new GameObject("bulletSpawn"), transform).transform;

            bulletSpawn.position = transform.position + transform.forward * 10;
        }

        

        #if UNITY_ANDROID
            //fireButton.onClick.AddListener(Fire);
        #endif
    }

    void Update()
    {
        if (velocity < .0001)
        {
            //prevent spacedust death until moving
            spacedust.Pause();
        }
        else
        {
            spacedust.Play();
        }
    
#if UNITY_ANDROID
        //turn camera based on motion of phone
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        if (firing)
        {
            Fire();
        }
#endif
#if UNITY_EDITOR
        transform.Rotate(-Input.GetAxis("Mouse Y")*2, Input.GetAxis("Mouse X")*2, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
#endif

        velocity = m_Rigidbody.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        m_Rigidbody.velocity *= (1 - friction);
        m_Rigidbody.angularVelocity *= (1 - friction);

        //THRUSTERS

#if UNITY_ANDROID
        if (thrusting)
        {
            m_Rigidbody.AddForce(transform.forward * acceleration * thrust, ForceMode.Impulse);
        }
#endif
#if UNITY_EDITOR
        //m_Rigidbody.AddForce(transform.forward * acceleration * thrust, ForceMode.Impulse);
        m_Rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * acceleration, ForceMode.Impulse);
#endif
    }

    void Fire()
    {
        if ((Time.time - last_shot) * fire_rate >= 1)
        {
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
            last_shot = Time.time;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        //health.Hit(10);
    }

    public void Warp()
    {
        //warp_animation.Play();
    }
}