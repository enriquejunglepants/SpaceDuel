using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{

    private Canvas hud;

    //public Bounds target_bounds;

    public GameObject cross_prefab;
    private Crosshair cross;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("hud").GetComponent<Canvas>();
        if (!gameObject.tag.Equals("Player"))
        {
            cross = Instantiate(cross_prefab, hud.transform).GetComponent<Crosshair>();
            cross.SetTarget(gameObject);
        }
    }
}
