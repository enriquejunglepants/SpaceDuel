using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    private Canvas hud;
    private GameObject target;
    public Health health;

    public Text name_text,distance_text;
    public RectTransform healthbar,square;
    public Bounds target_bounds;

    private Camera main_camera;


    void Start () {
        main_camera = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
    }
	
	void Update () {
        if (!target)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (health)
            {
                healthbar.sizeDelta = new Vector2(100 * health.current_health / health.max_health, 10);
                //healthbar.value = health.current_health;
            }

            Navigate();
        }
    }

    public void SetTarget(GameObject new_target)
    {
        target = new_target;
        health = target.GetComponent<Health>();
        target_bounds = target.GetComponent<Collider>().bounds;
        //healthbar.minValue = 0;
        //healthbar.maxValue = health.max_health;
        name_text.text = target.name;
    }

    void Navigate()
    {
        Vector3 arrow = main_camera.WorldToScreenPoint(target.transform.position);
        

        if (arrow.z > 0)
        {
            //target is not behind you

            if (arrow.x > 0 && arrow.x < Screen.width && arrow.y > 0 && arrow.y < Screen.height)
            {
                //target is visible on screen
                //Bounds target_bounds = target.GetComponent<Collider>().bounds;

                Vector2[] verts = { main_camera.WorldToScreenPoint(new Vector3(target_bounds.max.x, target_bounds.max.y, target_bounds.max.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.max.x, target_bounds.max.y, target_bounds.min.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.max.x, target_bounds.min.y, target_bounds.max.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.max.x, target_bounds.min.y, target_bounds.min.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.min.x, target_bounds.max.y, target_bounds.max.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.min.x, target_bounds.max.y, target_bounds.min.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.min.x, target_bounds.min.y, target_bounds.max.z)),
                                    main_camera.WorldToScreenPoint(new Vector3(target_bounds.min.x, target_bounds.min.y, target_bounds.min.z))};

                float min_x = verts[0].x,
                        max_x = verts[0].x,
                        min_y = verts[0].y,
                        max_y = verts[0].y;

                for (int i = 1; i < verts.Length; i++)
                {
                    if (verts[i].x < min_x)
                    {
                        min_x = verts[i].x;
                    }
                    if (verts[i].x > max_x)
                    {
                        max_x = verts[i].x;
                    }
                    if (verts[i].y < min_y)
                    {
                        min_y = verts[i].y;
                    }
                    if (verts[i].y > max_y)
                    {
                        max_y = verts[i].y;
                    }
                }

                square.sizeDelta = new Vector2(max_x - min_x, max_y - min_y);
                
                //name_text.transform.position = new Vector3(arrow.x,max_y,0);
                //name_text.transform.position = new Vector3(arrow.x, max_y, 0);
            }
            else
            {
                if (arrow.x > Screen.width)
                {
                    arrow.x = Screen.width;
                }
                if (arrow.x < 0)
                {
                    arrow.x = 0;
                }
                if (arrow.y > Screen.height)
                {
                    arrow.y = Screen.height;
                }
                if (arrow.y < 0)
                {
                    arrow.y = 0;
                }
                square.sizeDelta = new Vector2(100, 100);
            }
        }
        else
        {
            if (transform.position.x > Screen.width / 2)
            {
                arrow.x = Screen.width;
            }
            else
            {
                arrow.x = 0;
            }
            arrow.y = Screen.height / 2;
        }

        arrow.z = 0;

        transform.position = arrow;
    }
}
