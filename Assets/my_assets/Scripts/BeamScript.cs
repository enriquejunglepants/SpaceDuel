using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamScript : MonoBehaviour {

    public float damage;

    void Start () {
        // shoot the projectile
        //bullet.GetComponent<Rigidbody>().velocity = m_Rigidbody.velocity + bullet.transform.forward * 100;
        Destroy(this.gameObject, .5f);
    }

	void Update () {
	}

    //THIS IS AWFUL AND YOU SHOULD FEEL BAD ABOUT IT
    void OnTriggerEnter(Collider other)
    {
        //DO YOU FEEL BAD?
        other.gameObject.BroadcastMessage("Hit", damage);
        //YOU FUCKING SHOULD
    }
}
