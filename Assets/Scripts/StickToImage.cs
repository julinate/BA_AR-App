//Script to place an object at the center of a tracked image. Once an image is tracked, we wait a bit before adding a Reference Point
//so that it is more likely to be placed accurately.
//Instructions:
//
//1. Add this script to a 'registration' prefab that is used in the ARTrackedImageManager from the ARFoundation samples
//2. The ARTrackedImageManager is usually on the AR Session Origin gameobject in the scene.
//3. Add the ARReferencePointManager script to the AR Session Origina gameobject as well.
//4. Put the prefab that you want to stick to the image on the ARReferencePointManager Reference Point Prefab
//
//After an image is found, ARTrackedImageManager creates the 'registration' prefab - I used a cube that is the same size as the image.
//The 'registration' prefab has this script and starts to count each time the image tracking is updated. Once delayCount is reached,
//a new ReferencePoint is added.  The ARReferencePointManager takes care of instantiating the object. The ARTrackedImageManager is
//then disabled, which also deletes the 'registration' prefab (I believe).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StickToImage : MonoBehaviour
{
    public int delayCount = 200;

    ARTrackedImageManager trackedImageManager;
    ARAnchorManager referencePointManager;

    int count = 0;

    void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        referencePointManager = FindObjectOfType<ARAnchorManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        count++;

        if (count >= delayCount)
        {
            //Make sure your desired prefab is the m_ReferencePointPrefab on the ARReferencePointManager in the Editor
            referencePointManager.AddAnchor(new Pose(trackedImage.transform.position, trackedImage.transform.rotation));
            Debug.Log("Placed Object at " + Time.time);
            trackedImageManager.enabled = false;
            Destroy(this);
        }
    }

    void RemoveTrackedImage(ARTrackedImage trackedImage)
    {
        Destroy(this);
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
            UpdateTrackedImage(trackedImage);

        foreach (var trackedImage in eventArgs.updated)
            UpdateTrackedImage(trackedImage);

        foreach (var trackedImage in eventArgs.removed)
            RemoveTrackedImage(trackedImage);
    }
}