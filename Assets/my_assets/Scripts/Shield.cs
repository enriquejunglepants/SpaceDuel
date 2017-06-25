using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    // Use this for initialization
    public float reload_time=3, remaining_time=0;
    private Collider m_collider;
    private Health m_health;
    public Animator m_animator;

	void Start () {
        m_collider = GetComponent<Collider>();
        m_health = GetComponent<Health>();
        m_animator = GetComponent<Animator>();
        m_animator.StopPlayback();
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_collider.enabled)
        {
            if (remaining_time > 0)
            {
                remaining_time -= Time.deltaTime;
            }
            else
            {
                m_collider.enabled = true;
                m_health.enabled = true;
                m_health.current_health = m_health.max_health;
                m_animator.SetTrigger("reload");
            }
        }
	}

    public void Die()
    {
        Debug.Log("Shield down");
        m_collider.enabled = false;
        m_health.enabled = false;
        remaining_time = reload_time;
    }

    public void Hit_Animate()
    {
        m_animator.SetTrigger("hit");
    }
}
