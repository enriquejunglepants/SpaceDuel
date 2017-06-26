using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    public Health health,shield_health;
    public RectTransform bar,shield;
	void Start () {
        //shield_health = health.gameObject.GetComponent<Shield>().GetComponent<Health>();
        //health = transform.root.GetComponentInChildren<Health>();
    }
	
	void Update () {
        bar.anchorMax = new Vector2(health.current_health/health.max_health,bar.anchorMax.y);
        if (shield && shield_health)
        {
            shield.anchorMax = new Vector2(shield_health.current_health / shield_health.max_health, shield.anchorMax.y);
        }
    }
}
