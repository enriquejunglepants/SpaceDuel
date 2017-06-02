using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{

    public GameObject planetPrefab;

    public ParticleSystem spacedust;

    public Button fireButton;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Animation warp_animation;
    public List<LineRenderer> lines = new List<LineRenderer>();
    public float velocity;

    [SerializeField] private float acceleration;
    [SerializeField] private float friction;

    [SerializeField] private Slider thrust;

    private float max_speed = 0;
    private Camera m_Camera;

    private Rigidbody m_Rigidbody;
    private Health health;
    public Slider healthbar;

    void Start()
    {
        Input.gyro.enabled = true;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Camera = Camera.main;
        spacedust = GetComponentInChildren<ParticleSystem>();
        health = GetComponentInChildren<Health>();

        healthbar = GameObject.FindGameObjectWithTag("hud").GetComponentInChildren<Slider>();
        healthbar.maxValue = health.max_health;
        healthbar.minValue = 0;

        if (!bulletSpawn)
        {
            bulletSpawn = Instantiate(new GameObject("bulletSpawn"), transform).transform;

            bulletSpawn.position = transform.position + transform.forward * 10;
        }

        


        //fireButton.onClick.AddListener(Fire);
    }

    void Update()
    {
        //turn camera based on motion of phone
        //m_Camera.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        transform.Rotate(-Input.GetAxis("Mouse Y")*2, Input.GetAxis("Mouse X")*2, 0);

        if (healthbar)
        {
            healthbar.value = health.current_health;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }

        //thrust.value = Input.GetAxis("Vertical");

        velocity = m_Rigidbody.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        m_Rigidbody.velocity *= (1 - friction);
        m_Rigidbody.angularVelocity *= (1 - friction);

        //THRUSTERS
        m_Rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * acceleration, ForceMode.Impulse);

    }

    void Fire()
    {
        // create a projectile
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            transform.rotation);

        //bullet.transform.parent = bulletSpawn.transform.parent;
        //bullet.GetComponent<Rigidbody>().velocity = m_Rigidbody.velocity;
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