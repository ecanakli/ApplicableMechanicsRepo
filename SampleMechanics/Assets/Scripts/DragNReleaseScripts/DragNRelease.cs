using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNRelease : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField] private float force = 375f;
    [SerializeField] private Vector3 minForce;
    [SerializeField] private Vector3 maxForce;

    //----------Vector Positions------------
    private Vector3 startPoint;
    private Vector3 endPoint;

    private Rigidbody rb;
    private LineRenderer lr;

    //-------To Limit Start And End Position
    private Vector3 clampedStartPos, clampedEndPos;

    private Camera mainCamera;

    //Cam Z Position Is In Minus Direction, To Reset This, We Use rZValue
    private float rZValue;

    //To Avoid Repeating Shot
    private bool isThrow;

    private void Start()
    {
        //Getting Needed References
        GetReferences();

        //Set Opposite Z Value (If Cam.position.z = -10f Set It To = 10f)
        rZValue = -(mainCamera.transform.position.z);
    }

    private void GetReferences()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        ThrowBall();

        Destroy();
    }

    //Throwing Ball With Using Mouse Position
    private void ThrowBall()
    {
        //Set Start Point Which Is Object Position
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = transform.position;
        }

        //Draw Line Renderer Between Object Position And MousePosition
        if (Input.GetMouseButton(0))
        {
            //Setting End Point Of The Mouse
            endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, rZValue));

            //Get Limited Forces To Drawing Lines And To Shot
            GetLimitedForces(out clampedStartPos, out clampedEndPos);

            //Drawing
            DrawLine(clampedStartPos, clampedEndPos);
        }

        //Throwing Ball
        if (Input.GetMouseButtonUp(0))
        {
            Throw(clampedEndPos - clampedStartPos);

            //Reset Line After Shot
            ResetLine();
        }
    }

    //Limiting StartPosition And EndPosition Forces
    private void GetLimitedForces(out Vector3 clampedStartPos, out Vector3 clampedEndPos)
    {
        clampedStartPos = new Vector3(Mathf.Clamp(startPoint.x, minForce.x, maxForce.x), Mathf.Clamp(startPoint.y, minForce.y, maxForce.y), Mathf.Clamp(startPoint.z, minForce.z, maxForce.z));
        clampedEndPos = new Vector3(Mathf.Clamp(endPoint.x, minForce.x, maxForce.x), Mathf.Clamp(endPoint.y, minForce.y, maxForce.y), Mathf.Clamp(endPoint.z, minForce.z, maxForce.z));
    }
    private void Throw(Vector3 Force)
    {
        //To Avoid Repeating Shot
        if (isThrow)
            return;

        //Applying Force
        rb.AddForce(new Vector3(Force.x, 0f, Force.z) * force);
        isThrow = true;
    }
    //Drawing Line Points
    private void DrawLine(Vector3 startPoint, Vector3 endPoint)
    {
        if (isThrow)
            return;

        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        startPoint = transform.position;
        points[0] = startPoint;
        points[1] = endPoint;
        lr.SetPositions(points);
    }

    //To Reset Line After Shot
    private void ResetLine()
    {
        lr.positionCount = 0;
    }

    //After Reaching Certain Point Destroy Object
    private void Destroy()
    {
        if (transform.position.y < -3f)
        {
            Destroy(gameObject);
        }
    }
}