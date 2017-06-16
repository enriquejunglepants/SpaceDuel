using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    public Health health;
    public RectTransform bar;
	void Start () {
	}
	
	void Update () {
        bar.anchorMax = new Vector2(health.current_health/health.max_health,bar.anchorMax.y);
	}
}
