using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Transform holdedObject;
    private PlaceableObject placeableObject;

    //For Accessing Holded Object Rigidbody
    private Rigidbody rb;

    //Max Box Height While Holding
    private float maxHoldedHeight = 3f;

    private void Start()
    {
        //Subscribing Event
        GameOverManager.IsGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        //Unsubscribing Event
        GameOverManager.IsGameOver -= HandleGameOver;
    }

    private void Update()
    {
        TapDragToSort();
    }

    //Main Mechanic
    private void TapDragToSort()
    {
        RaycastHoldObject();

        PickAndRelease(GetMousePosition());
    }

    //Getting Current Mouse Position
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.y);
        Vector3 cP = GetComponent<Camera>().ScreenToWorldPoint(mousePos);
        return cP;
    }

    //Holding The Object We Hitted By Using Raycast
    private void RaycastHoldObject()
    {
        Vector2 mousePos = Input.mousePosition;
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, 1000, layerMask) && holdedObject == null)
        {
            placeableObject = hit.collider.gameObject.GetComponent<PlaceableObject>();

            if (Input.GetMouseButtonDown(0))
            {
                holdedObject = hit.collider.transform;

                rb = hit.collider.GetComponent<Rigidbody>();
                rb.isKinematic = false;

                holdedObject.parent.parent.GetComponent<ReplacementController>().RemoveItem(holdedObject.gameObject);
                holdedObject.GetComponent<PlaceableObject>().isFilled = false;
            }
        }
    }

    //Picking Up And Release The Object We Hold
    private void PickAndRelease(Vector3 mousePos)
    {
        if(holdedObject == null) { return; }

        //Raise Box To Maximum Height
        if (holdedObject.transform.position.y < maxHoldedHeight)
        {
            rb.useGravity = false;
            holdedObject.position += new Vector3(0f, 0.5f, 0f); //Increase Box Height By 0.5f Every Frame
            placeableObject.isHold = true; //Object Grabbed
        }
        //If Box Reaches The Top Point Then Drag
        else
        {
            //Use Drag
            MouseDrag(mousePos);
        }

        Release();
    }

    //Drag Mechanic
    private void MouseDrag(Vector3 mousePos)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        holdedObject.position = Vector3.Lerp(holdedObject.position, new Vector3(mousePos.x, holdedObject.position.y, mousePos.z), Time.deltaTime * 7.5f);
    }

    //Dropping The Object When We Release Click
    private void Release()
    {
        if (holdedObject == null) { return; }

        if (Input.GetMouseButtonUp(0))
        {
            rb.useGravity = true;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

            placeableObject.isHold = false;
            holdedObject = null;
        }
    }

    //When Game Ends Disable This Script
    private void HandleGameOver()
    {
        enabled = false;
    }
}
