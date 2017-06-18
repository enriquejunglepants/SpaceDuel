using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Weapon {

    public Portal otherPortal;

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.position = otherPortal.transform.position;
        PlayerShip otherShip = other.gameObject.GetComponent<PlayerShip>();
        if (otherShip)
        {
            //otherShip.Warp();
        }
    }
}
