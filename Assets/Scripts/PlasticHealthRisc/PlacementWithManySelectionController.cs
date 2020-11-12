using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;


[RequireComponent(typeof(ARRaycastManager))]
public class PlacementWithManySelectionController : MonoBehaviour
{

    public UnityEvent TabOnObject; //neu

    [SerializeField]
    private PlacementObject[] placedObjects;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private bool displayOverlay = false;

    void Awake() 
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
  
    void Start()
    {
        ChangeSelectedObject(placedObjects[0]);
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                    if(placementObject != null)
                    {
                        ChangeSelectedObject(placementObject);
                    }
                }
            }
        }
    }

    void ChangeSelectedObject(PlacementObject selected)
    {
        foreach (PlacementObject current in placedObjects)
        {   
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if(selected != current) 
            {
                current.Selected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else 
            {
                current.Selected = true;
                meshRenderer.material.color = activeColor;
                TabOnObject.Invoke(); //neu
            }

            if (displayOverlay)
                current.ToggleOverlay();
        }
    }
}
