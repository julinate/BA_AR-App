using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events; //neu


[RequireComponent(typeof(ARTrackedImageManager))]
public class ARTrackedImageManagerUXController : MonoBehaviour
{

    public UnityEvent HideGuide; //neu

    //[SerializeField]
    //GameObject UXGuide;

    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventsArgs)
    {
        foreach (ARTrackedImage trackedImage in eventsArgs.added)
        {
            HideGuide.Invoke(); //neu
            //Destroy(UXGuide);
        }
    }
}
