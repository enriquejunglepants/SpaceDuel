using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon {
    public GameObject m_explosion;
    protected override void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity =Vector3.zero;

        m_collider = GetComponent<Collider>();

        if (shooter)
        {
            foreach (Collider c in shooter.GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(m_collider, c);
            }
        }
    }

    protected override void Update()
    {

    }

    protected override void FixedUpdate()
    {
    }
    protected override void OnCollisionEnter(Collision collision)
    {

       
        if (m_explosion)
        {
            Instantiate(m_explosion,
                        transform.position,
                        transform.rotation);
            //Debug.logger.Log("hit " + collision.collider.name);
            Health hurtable = collision.transform.root.GetComponent<Health>();
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
            m_explosion = null;
        }

        Destroy(this.gameObject);
    }
}