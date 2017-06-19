using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponControl : EventTrigger {

    public PlayerShip player;
    [SerializeField] public Image weapon_icon;
    [SerializeField] public Text weapon_name;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("drag " + eventData.position.y/Screen.height);
        player.ChangeWeapon(Mathf.RoundToInt((eventData.position.y / Screen.height) * (player.weapons.Count - 1)));
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("pointer down");
        player.firing = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("pointer up");
        player.firing = false;
    }

}
