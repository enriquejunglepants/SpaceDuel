using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AI_Ship : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    [SerializeField] private float acceleration,thrust, friction;
    [SerializeField] private Vector3 turn,distance_between;

    public float turn_speed, thrust_thresh,fire_thresh,thrust_dist;
    private Transform ship_transform;
    public GameObject target;
    private Rigidbody m_Rigidbody;
    private Health health;

    private float last_shot,fire_rate=3;

    public Color color = Color.green;

    private Vector3 v3FrontTopLeft;
    private Vector3 v3FrontTopRight;
    private Vector3 v3FrontBottomLeft;
    private Vector3 v3FrontBottomRight;
    private Vector3 v3BackTopLeft;
    private Vector3 v3BackTopRight;
    private Vector3 v3BackBottomLeft;
    private Vector3 v3BackBottomRight;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        health = GetComponent<Health>();

        ship_transform = transform;
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (!bulletSpawn)
        {
            bulletSpawn = Instantiate(new GameObject("bulletSpawn"),transform).transform;

            bulletSpawn.position = transform.position + transform.forward * 10;
        }
    }


    void Update()
    {
        //CalcPositons();
        //DrawBox();

        distance_between = ship_transform.InverseTransformPoint(target.transform.position);
        turn = distance_between;
        thrust = 0;

        if (turn.z <= 0)
        {
            turn = new Vector3(0, turn_speed, 0);
        }
        else
        {
            //turn.z = 0;
            turn.Normalize();
            turn.z = 0;

            if (turn.magnitude < thrust_thresh && distance_between.magnitude >= thrust_dist)
            {
                thrust = 1;
            }

            if (turn.magnitude < fire_thresh)
            {
                Fire();
            }

            turn = new Vector3(-turn.y, turn.x, 0) * turn_speed;
        }

    }

    private void FixedUpdate()
    {
        m_Rigidbody.velocity *= (1 - friction);
        m_Rigidbody.angularVelocity *= (1 - friction);

        m_Rigidbody.AddForce(ship_transform.forward * acceleration * thrust, ForceMode.Impulse);
        ship_transform.Rotate(turn);

        //Fire();
    }

    void CalcPositons()
    {
        //Bounds bounds = GetComponent<MeshFilter>().mesh.bounds;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach(MeshFilter mf in GetComponentsInChildren<MeshFilter>())
        {
            Bounds part_bounds = new Bounds(mf.transform.localPosition, new Vector3(mf.mesh.bounds.size.x * mf.transform.localScale.x,
                                                                                    mf.mesh.bounds.size.y * mf.transform.localScale.y,
                                                                                    mf.mesh.bounds.size.z * mf.transform.localScale.z));
            bounds.Encapsulate(part_bounds);
        }
        Debug.Log("FINAL BOUNDS: " + bounds);

        //Bounds bounds;
        //BoxCollider bc = GetComponent<BoxCollider>();
        //if (bc != null)
        //    bounds = bc.bounds;
        //else
        //return;

        Vector3 v3Center = bounds.center;
        Vector3 v3Extents = bounds.extents;

        v3FrontTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
        v3FrontTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
        v3FrontBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
        v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
        v3BackTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
        v3BackTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
        v3BackBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
        v3BackBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner

        v3FrontTopLeft = transform.TransformPoint(v3FrontTopLeft);
        v3FrontTopRight = transform.TransformPoint(v3FrontTopRight);
        v3FrontBottomLeft = transform.TransformPoint(v3FrontBottomLeft);
        v3FrontBottomRight = transform.TransformPoint(v3FrontBottomRight);
        v3BackTopLeft = transform.TransformPoint(v3BackTopLeft);
        v3BackTopRight = transform.TransformPoint(v3BackTopRight);
        v3BackBottomLeft = transform.TransformPoint(v3BackBottomLeft);
        v3BackBottomRight = transform.TransformPoint(v3BackBottomRight);
    }

    void DrawBox()
    {
        //if (Input.GetKey (KeyCode.S)) {
        Debug.DrawLine(v3FrontTopLeft, v3FrontTopRight, color);
        Debug.DrawLine(v3FrontTopRight, v3FrontBottomRight, color);
        Debug.DrawLine(v3FrontBottomRight, v3FrontBottomLeft, color);
        Debug.DrawLine(v3FrontBottomLeft, v3FrontTopLeft, color);

        Debug.DrawLine(v3BackTopLeft, v3BackTopRight, color);
        Debug.DrawLine(v3BackTopRight, v3BackBottomRight, color);
        Debug.DrawLine(v3BackBottomRight, v3BackBottomLeft, color);
        Debug.DrawLine(v3BackBottomLeft, v3BackTopLeft, color);

        Debug.DrawLine(v3FrontTopLeft, v3BackTopLeft, color);
        Debug.DrawLine(v3FrontTopRight, v3BackTopRight, color);
        Debug.DrawLine(v3FrontBottomRight, v3BackBottomRight, color);
        Debug.DrawLine(v3FrontBottomLeft, v3BackBottomLeft, color);
        //}
    }


    void Fire()
    {
        if ((Time.time - last_shot) * fire_rate >= 1)
        {
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
            last_shot = Time.time;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //health.Hit(10);
    }
}