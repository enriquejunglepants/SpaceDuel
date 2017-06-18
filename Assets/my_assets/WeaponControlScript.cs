using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponControlScript : EventTrigger {

    public PlayerShip player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("drag " + eventData.position.y/Screen.height);
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
