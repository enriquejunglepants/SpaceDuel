using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    private Canvas hud;
    private GameObject target;
    public Text name_text,distance_text;
    public RectTransform square;
    public Bounds target_bounds;

    public float default_x = 20, default_y = 20;

    private Camera main_camera;


    void Start () {
        GetComponentInChildren<Healthbar>().health = target.GetComponent<Health>();
        main_camera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }
	
	void Update () {
        if (!target)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Navigate();
        }
    }

    public void SetTarget(GameObject new_target)
    {
        target = new_target;
        //target_bounds = target.GetComponent<Collider>().bounds;
        name_text.text = target.name;
    }

    public void RecalculateBounds()
    {
        target_bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (MeshFilter mf in target.GetComponentsInChildren<MeshFilter>())
        {
            Bounds part_bounds = new Bounds(mf.transform.localPosition, new Vector3(mf.mesh.bounds.size.x * mf.transform.localScale.x,
                                                                                    mf.mesh.bounds.size.y * mf.transform.localScale.y,
                                                                                    mf.mesh.bounds.size.z * mf.transform.localScale.z));
            target_bounds.Encapsulate(part_bounds);
        }
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
                RecalculateBounds();

                Vector2[] verts = { main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.max.x, target_bounds.max.y, target_bounds.max.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.max.x, target_bounds.max.y, target_bounds.min.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.max.x, target_bounds.min.y, target_bounds.max.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.max.x, target_bounds.min.y, target_bounds.min.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.min.x, target_bounds.max.y, target_bounds.max.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.min.x, target_bounds.max.y, target_bounds.min.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.min.x, target_bounds.min.y, target_bounds.max.z))),
                                    main_camera.WorldToScreenPoint(target.transform.TransformPoint(new Vector3(target_bounds.min.x, target_bounds.min.y, target_bounds.min.z)))};

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

                square.sizeDelta = new Vector2(Mathf.Max(max_x - min_x,default_x), Mathf.Max(max_y - min_y, default_y));
                
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
                square.sizeDelta = new Vector2(default_x, default_y);
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
