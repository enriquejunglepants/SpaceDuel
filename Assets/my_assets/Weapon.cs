using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public float damage;
    public float lifespan;
    public float speed;

    public Rigidbody m_Rigidbody;
    public LineRenderer line;

    void Start()
    {
        Destroy(this.gameObject, lifespan);
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward * speed;
        //line = GetComponent<LineRenderer>();
        //line.useWorldSpace = false;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.BroadcastMessage("Hit", damage);
        //Destroy(this.gameObject);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Debug.logger.Log("hit " + collision.collider.name);
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
