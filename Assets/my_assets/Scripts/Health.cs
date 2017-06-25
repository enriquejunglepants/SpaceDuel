using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float max_health;
    public float current_health;


	void Start () {
        current_health = max_health;
	}
	
	
	void Update () {
        if (current_health <= 0)
        {
            //gameObject.BroadcastMessage("Die");
            gameObject.SendMessage("Die");
            //Die();
            
        }
	}

    public void Hit(float damage)
    {
        current_health -= damage;
        gameObject.SendMessage("Hit_Animate");
    }
}
