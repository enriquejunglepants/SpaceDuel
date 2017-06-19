using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Ship shooter;
    public float damage;
    public float lifespan;
    public float speed;
    public Texture icon;
    public string weapon_name;

    public Rigidbody m_Rigidbody;
    
    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
    }
}
