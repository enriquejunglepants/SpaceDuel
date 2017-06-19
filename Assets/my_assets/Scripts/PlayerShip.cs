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
    public WeaponSelector selector;
    public float weapon_scroll = 0;

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

        float scroll_change = Input.GetAxis("Mouse ScrollWheel");

        if (scroll_change != 0)
        {
            weapon_scroll += scroll_change;
            if (weapon_scroll < 0)
            {
                weapon_scroll = 0;
            }
            else if (weapon_scroll > weapons.Count - 1)
            {
                weapon_scroll = weapons.Count - 1;
            }
            ChangeWeapon(Mathf.RoundToInt(scroll_change));
        }
        //if(scroll!=0) Debug.Log(scroll);
                
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

    public void ChangeWeapon(int w)
    {
        if (selected_weapon != w)
        {
            selected_weapon = w;
            selector.SetIcon(weapons[selected_weapon].icon);
            selector.SetName(weapons[selected_weapon].weapon_name);
        }
    }
}