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
        PlayerShip otherShip = other.gameObject.GetComponent<PlayerShip>();
        if (otherShip)
        {
            //otherShip.Warp();
        }
    }
}
