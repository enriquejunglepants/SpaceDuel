using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour {

    public RawImage weapon_icon;
    public Text weapon_name;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        weapon_icon.color = new Color(weapon_icon.color.r, weapon_icon.color.g, weapon_icon.color.b, weapon_icon.color.a * .9f);
        weapon_name.color = new Color(weapon_name.color.r, weapon_name.color.g, weapon_name.color.b, weapon_name.color.a * .9f);
    }

    public void SetIcon(Texture icon)
    {
        weapon_icon.texture = icon;
        weapon_icon.color = new Color(weapon_icon.color.r, weapon_icon.color.g, weapon_icon.color.b, 255);
    }

    public void SetName(string new_name)
    {
        weapon_name.text = new_name;
        weapon_name.color = new Color(weapon_name.color.r, weapon_name.color.g, weapon_name.color.b, 255);
    }
}
