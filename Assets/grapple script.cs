using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplescript : MonoBehaviour
{
    private Vector3 point;
    public Vector3 Diff;
    public float maxdist;
    public float dist;
    public Vector3 perpDir;
    public float perpVel;
    GameObject target;
    private bool held = false;
    public Camera playerCamera;
    private fpscontroller parentbody;
    private Rigidbody parentrigid;
    private bool hitbool;
    private LineRenderer line;
    public GameObject Barrel;
    // Start is called before the first frame update
    void Start()
    {
        parentbody = GetComponent<fpscontroller>();
        parentrigid = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !held)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, transform.rotation * playerCamera.transform.localRotation * Vector3.forward, out hit, Mathf.Infinity))
            {
                point = hit.point;
                maxdist = Vector3.Distance(point, transform.position);
                target = hit.collider.gameObject;
                hitbool = true;
                line.positionCount = 2;
                line.SetPosition(0, point);
                line.SetPosition(1, transform.position);
            }
            held = true;
        }
        if (held)
        {
            if (!Input.GetMouseButton(0))
            {
                maxdist = 0f;
                Diff.Set(0f, 0f, 0f);
                hitbool = false;
                held = false;
                line.positionCount = 0;
            }
            else if (hitbool)// && !parentbody.isgrounded)
            {
                Diff = (point - transform.position);
                dist = Diff.magnitude;
                line.SetPosition(1, Barrel.transform.position + Barrel.transform.forward * -0.45f + Barrel.transform.right * 0.05f);
                if (Diff.magnitude > maxdist)
                {
                    parentrigid.velocity -= Vector3.Dot(parentrigid.velocity, Diff.normalized) * Diff.normalized;
                    parentrigid.velocity +=  0.05f * (dist * Diff.normalized);
                }

            }
        }
    }

}
