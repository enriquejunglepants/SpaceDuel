using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrusterControl : EventTrigger
{

    public PlayerShip player;
    public RectTransform filler;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        filler = GetComponentsInChildren<RectTransform>()[1];
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("pointer down");
        player.thrust = 2 * (eventData.position.y / Screen.height) - 1;
        filler.anchorMax = new Vector2(filler.anchorMax.x, eventData.position.y / Screen.height);
        player.thrusting = true;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("pointer down");
        player.thrust = 2 * (eventData.position.y / Screen.height) - 1;
        filler.anchorMax = new Vector2(filler.anchorMax.x, eventData.position.y / Screen.height);
        player.thrusting = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("pointer up");
        filler.anchorMax = new Vector2(filler.anchorMax.x, .5f);
        player.thrusting = false;
    }

}
