using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Ship shooter;
    public float damage;
    public float lifespan;
    public float speed;
    public Texture icon;
    public string weapon_name;
    protected Collider m_collider;

    public Rigidbody m_Rigidbody;
    
    protected virtual void Start()
    {
        Destroy(this.gameObject, lifespan);
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity += transform.forward * speed;

        m_collider = GetComponent<Collider>();

        foreach (Collider c in shooter.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(m_collider, c);
        }
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //Debug.logger.Log("hit " + collision.collider.name);
        Health hurtable;
        if (collision.collider.name.Equals("Shield"))
        {
            hurtable = collision.collider.GetComponent<Health>();
            if (hurtable)
            {
                hurtable.Hit(damage);
            }
        }
        else
        {
            hurtable = collision.transform.root.GetComponent<Health>();
            if (hurtable)
            {
                hurtable.Hit(damage);
            }
            /*
            hurtable = collision.collider.GetComponent<Health>();
            if (hurtable)
            {
                hurtable.Hit(damage);
            }*/
        }

        Destroy(m_collider);
    }
}
