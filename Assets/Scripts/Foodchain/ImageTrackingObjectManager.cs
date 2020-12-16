using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingObjectManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image manager on the AR Session Origin")]
    ARTrackedImageManager m_ImageManager;

    /// <summary>
    /// Get the <c>ARTrackedImageManager</c>
    /// </summary>
    public ARTrackedImageManager ImageManager
    {
        get => m_ImageManager;
        set => m_ImageManager = value;
    }

    [SerializeField]
    [Tooltip("Reference Image Library")]
    XRReferenceImageLibrary m_ImageLibrary;

    /// <summary>
    /// Get the <c>XRReferenceImageLibrary</c>
    /// </summary>
    public XRReferenceImageLibrary ImageLibrary
    {
        get => m_ImageLibrary;
        set => m_ImageLibrary = value;
    }

    [SerializeField]
    [Tooltip("Prefab for Net image")]
    GameObject m_NetPrefab;

    /// <summary>
    /// Get the one prefab
    /// </summary>
    public GameObject netPrefab
    {
        get => m_NetPrefab;
        set => m_NetPrefab = value;
    }

    GameObject m_SpawnedNetPrefab;
    
    /// <summary>
    /// get the spawned one prefab
    /// </summary>
    public GameObject spawnedNetPrefab
    {
        get => m_SpawnedNetPrefab;
        set => m_SpawnedNetPrefab = value;
    }

    [SerializeField]
    [Tooltip("Prefab for tracked Plankton One image")]
    GameObject m_PlanktonOne;

    /// <summary>
    /// get the two prefab
    /// </summary>
    public GameObject planktonOne
    {
        get => m_PlanktonOne;
        set => m_PlanktonOne = value;
    }

    GameObject m_SpawnedPlanktonOnePrefab;
    
    /// <summary>
    /// get the spawned two prefab
    /// </summary>
    public GameObject spawnedPlanktonOnePrefab
    {
        get => m_SpawnedPlanktonOnePrefab;
        set => m_SpawnedPlanktonOnePrefab = value;
    }

    int m_NumberOfTrackedImages;
    
    NumberManager m_OneNumberManager;
    NumberManager m_TwoNumberManager;

    static Guid s_NetImageGUID;
    static Guid s_PlanktonOneImageGUID;

    void OnEnable()
    {
        s_NetImageGUID = m_ImageLibrary[2].guid;
        s_PlanktonOneImageGUID = m_ImageLibrary[0].guid;
        
        m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // added, spawn prefab
        foreach(ARTrackedImage image in obj.added)
        {
            if (image.referenceImage.guid == s_NetImageGUID)
            {
                m_SpawnedNetPrefab = Instantiate(m_NetPrefab, image.transform.position, image.transform.rotation);
                m_OneNumberManager = m_SpawnedNetPrefab.GetComponent<NumberManager>();
            }
            else if (image.referenceImage.guid == s_PlanktonOneImageGUID)
            {
                m_SpawnedPlanktonOnePrefab = Instantiate(m_PlanktonOne, image.transform.position, image.transform.rotation);
                m_TwoNumberManager = m_SpawnedPlanktonOnePrefab.GetComponent<NumberManager>();
            }
        }
        
        // updated, set prefab position and rotation
        foreach(ARTrackedImage image in obj.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (image.trackingState == TrackingState.Tracking)
            {
                if (image.referenceImage.guid == s_NetImageGUID)
                {
                    m_OneNumberManager.Enable3DNumber(true);
                    m_SpawnedNetPrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                }
                else if (image.referenceImage.guid == s_PlanktonOneImageGUID)
                {
                    m_TwoNumberManager.Enable3DNumber(true);
                    m_SpawnedPlanktonOnePrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                }
            }
            // image is no longer tracking, disable visuals TrackingState.Limited TrackingState.None
            else
            {
                if (image.referenceImage.guid == s_NetImageGUID)
                {
                    m_OneNumberManager.Enable3DNumber(false);
                }
                else if (image.referenceImage.guid == s_PlanktonOneImageGUID)
                {
                    m_TwoNumberManager.Enable3DNumber(false);
                }
            }
        }
        
        // removed, destroy spawned instance
        foreach(ARTrackedImage image in obj.removed)
        {
            if (image.referenceImage.guid == s_NetImageGUID)
            {
                Destroy(m_SpawnedNetPrefab);
            }
            else if (image.referenceImage.guid == s_NetImageGUID)
            {
                Destroy(m_SpawnedPlanktonOnePrefab);
            }
        }
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_NumberOfTrackedImages++;
            }
        }
        return m_NumberOfTrackedImages;
    }
}
