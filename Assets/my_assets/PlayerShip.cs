using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : Ship
{
    public ParticleSystem spacedust;

    //public Button fireButton;

    private Camera m_Camera;
    
    protected override void Start()
    {
        base.Start();
        Input.gyro.enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        #if UNITY_ANDROID
                //turn camera based on motion of phone
                transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        #endif

        #if UNITY_EDITOR
                //turn camera based on mouse
                transform.Rotate(-Input.GetAxis("Mouse Y")*2, Input.GetAxis("Mouse X")*2, 0);
                if (Input.GetKey(KeyCode.Space))
                {
                    firing = true;
                }
        #endif
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        #if UNITY_EDITOR
                thrust = Input.GetAxis("Vertical");
                thrusting = true;
        #endif
    }
}