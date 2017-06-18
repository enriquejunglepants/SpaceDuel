using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon {

    protected override void Start()
    {
        base.Start();
        Destroy(this.gameObject, lifespan);
        m_Rigidbody.velocity = transform.forward * speed;
    }

    protected override void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.logger.Log("hit " + collision.collider.name);
        Health hurtable = collision.gameObject.GetComponent<Health>();
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

        Destroy(this.gameObject);
    }
}
