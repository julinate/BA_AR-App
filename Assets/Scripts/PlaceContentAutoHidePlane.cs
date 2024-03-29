﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.Events; //neu


public class PlaceContentAutoHidePlane : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;
    public ARPlaneManager planeManager;
    public ARPointCloudManager pointCloudManager;

    public UnityEvent ActivateObjectOnPlacement; //neu
    //public UnityEvent HideGuide; //neu

    //[SerializeField]
    //GameObject UXPlaceGuide;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsClickOverUI())
        {
       
            List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.mousePosition, hitPoints, TrackableType.Planes);

            if (hitPoints.Count > 0)
            {
                ActivateObjectOnPlacement.Invoke(); //neu
                //HideGuide.Invoke(); //neu
                //UXPlaceGuide.SetActive(false);

                Pose pose = hitPoints[0].pose;
                transform.rotation = pose.rotation;
                transform.position = pose.position;
            }

            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            planeManager.enabled = false;

            foreach (var point in pointCloudManager.trackables)
            {
                point.gameObject.SetActive(false);
            }

            //pointCloudManager.enabled = false;
        }
    }

    bool IsClickOverUI()
    {
        PointerEventData data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
    };
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results.Count > 0;

    }
}
