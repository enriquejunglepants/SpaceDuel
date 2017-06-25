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
    
    [SerializeField] public List<Weapon> weapons = new List<Weapon>();

    public int selected_weapon = 0;

    public Transform bulletSpawn;

    public float velocity,acceleration,max_speed;

    public float thrust;

    private Rigidbody m_Rigidbody;
    
    private float last_shot, fire_rate = 3;

    public Boolean firing, thrusting;

    public Portal portalA,portalB;

    protected Health m_health;

    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_health = GetComponent<Health>();
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
        if (thrusting && velocity<max_speed)
        {
            m_Rigidbody.AddForce(transform.forward * acceleration * thrust, ForceMode.Impulse);
        }
    }

    protected void Fire()
    {
        if ((Time.time - last_shot) * fire_rate >= 1)
        {
            Weapon bullet = Instantiate(weapons[selected_weapon].gameObject,
                        bulletSpawn.position,
                        bulletSpawn.rotation).GetComponent<Weapon>();
            bullet.shooter = this;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward*velocity;
            last_shot = Time.time;
        }
        firing = false;
    }

    public Bounds GetBounds()
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (MeshFilter mf in gameObject.GetComponentsInChildren<MeshFilter>())
        {
            Bounds part_bounds = new Bounds(mf.transform.localPosition, new Vector3(mf.mesh.bounds.size.x * mf.transform.localScale.x,
                                                                                    mf.mesh.bounds.size.y * mf.transform.localScale.y,
                                                                                    mf.mesh.bounds.size.z * mf.transform.localScale.z));
            bounds.Encapsulate(part_bounds);
        }
        return bounds;
    }

    public void Die()
    {
        Debug.Log("Ship down");
        float radius = 5000;
        m_health.current_health = m_health.max_health;
        transform.position = new Vector3(UnityEngine.Random.Range(-radius, radius),
                                    UnityEngine.Random.Range(-radius, radius),
                                    UnityEngine.Random.Range(-radius, radius));
    }
}