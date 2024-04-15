using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplescript : MonoBehaviour
{
    private Vector3 point;
    private Vector3 Diff;
    private float maxdist;
    private float dist;
    private GameObject target;
    private Rigidbody targetrigid;
    private bool held = false;
    public Camera playerCamera;//boo
    private fpscontroller2 parentbody;
    private Rigidbody parentrigid;
    private bool hitbool;
    private LineRenderer line;
    private Vector3 velChng;
    public GameObject Barrel;
    // Start is called before the first frame update
    void Start()
    {
        parentbody = GetComponent<fpscontroller2>();
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
                targetrigid = target.GetComponent<Rigidbody>();
                point = target.transform.InverseTransformPoint(point);
                hitbool = true;
                line.positionCount = 2;
                line.SetPosition(0, target.transform.TransformPoint(point));
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
                Diff = (target.transform.TransformPoint(point) - transform.position);
                dist = Diff.magnitude;
                line.SetPosition(1, Barrel.transform.position + Barrel.transform.forward * -0.45f + Barrel.transform.right * 0.05f);
                line.SetPosition(0, target.transform.TransformPoint(point));
                if (Diff.magnitude > maxdist)
                {
                    velChng.Set(0,0,0);
                    velChng -= Vector3.Dot(parentrigid.velocity, Diff.normalized) * Diff.normalized;
                    velChng +=  0.05f * (dist * Diff.normalized);
                    parentrigid.velocity += velChng;
                    if (targetrigid != null)
                    {
                        targetrigid.AddForceAtPosition(-velChng / Time.deltaTime, target.transform.TransformPoint(point));
                    }
                }
            }
        }
    }
}
