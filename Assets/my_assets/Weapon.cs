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
        // shoot the projectile
        GetComponent<Rigidbody>().velocity += transform.forward * speed;
        Destroy(this.gameObject, lifespan);
        //m_Rigidbody = GetComponent<Rigidbody>();
        //line = GetComponent<LineRenderer>();
        //line.useWorldSpace = false;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        //transform.localPosition = new Vector3(0,0,100);
        //m_Rigidbody.AddForce(transform.forward, ForceMode.Impulse);
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.BroadcastMessage("Hit", damage);
        //Destroy(this.gameObject);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Debug.logger.Log("hit " + collision.gameObject.name);
        Health hurtable = collision.gameObject.GetComponent<Health>();
        if (hurtable)
        {
            hurtable.Hit(damage);
        }
        else
        {
            hurtable = collision.gameObject.GetComponentInChildren<Health>();
            if (hurtable)
            {
                hurtable.Hit(damage);
            }
        }

        Destroy(this.gameObject);
    }
}
