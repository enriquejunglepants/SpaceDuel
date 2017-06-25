using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Weapon {

    public Portal otherPortal;

    protected override void Start()
    {
        m_collider = GetComponent<Collider>();
        if(shooter)
        {
            transform.Translate(transform.forward * 50);
            if (shooter.portalA)
            {
                if (shooter.portalB)
                {
                    shooter.portalA = shooter.portalB;
                }
                shooter.portalB = this;
                otherPortal = shooter.portalA;
                otherPortal.otherPortal = this;
            }
            else
            {
                shooter.portalA = this;
            }
        }
    }

    protected override void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (otherPortal)
        {
            foreach (Collider c in other.transform.root.GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(m_collider, c);
                Physics.IgnoreCollision(otherPortal.m_collider, c);
            }
            other.transform.root.position = otherPortal.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(name+" - "+other.name);
    }
}
