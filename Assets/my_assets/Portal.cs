using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Portal otherPortal;
	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.position = otherPortal.transform.position;
        ShipController otherShip = other.gameObject.GetComponent<ShipController>();
        if (otherShip)
        {
            otherShip.Warp();
        }
    }
}
