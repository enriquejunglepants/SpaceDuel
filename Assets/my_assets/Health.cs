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
            Die();
        }
	}

    public void Hit(float damage)
    {
        current_health -= damage;
    }

    void Die()
    {
        if (gameObject.GetComponent<Rigidbody>())
        {
            float radius = 5000;
            current_health = max_health;
            transform.position = new Vector3(Random.Range(-radius, radius),
                                        Random.Range(-radius, radius),
                                        Random.Range(-radius, radius));
        }
        else Destroy(this.gameObject);
    }
}
