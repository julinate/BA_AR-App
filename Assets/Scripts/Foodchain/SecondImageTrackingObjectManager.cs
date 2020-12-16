using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SecondImageTrackingObjectManager : MonoBehaviour
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
    [Tooltip("Prefab for tracked Net image")]
    GameObject m_SecondNetPrefab;

    /// <summary>
    /// Get the one prefab
    /// </summary>
    public GameObject secondNetPrefab
    {
        get => m_SecondNetPrefab;
        set => m_SecondNetPrefab = value;
    }

    GameObject m_SpawnedSecondNetPrefab;
    
    /// <summary>
    /// get the spawned one prefab
    /// </summary>
    public GameObject spawnedSecondNetPrefab
    {
        get => m_SpawnedSecondNetPrefab;
        set => m_SpawnedSecondNetPrefab = value;
    }

    [SerializeField]
    [Tooltip("Prefab for tracked PlanktonTwo image")]
    GameObject m_PlanktonTwo;

    /// <summary>
    /// get the two prefab
    /// </summary>
    public GameObject planktonTwo
    {
        get => m_PlanktonTwo;
        set => m_PlanktonTwo = value;
    }

    GameObject m_SpawnedPlanktonTwo;
    
    /// <summary>
    /// get the spawned two prefab
    /// </summary>
    public GameObject spawnedPlanktonTwoPrefab
    {
        get => m_SpawnedPlanktonTwo;
        set => m_SpawnedPlanktonTwo = value;
    }

    int m_SecondNumberOfTrackedImages;
    
    NumberManager m_ThreeNumberManager;
    NumberManager m_FourNumberManager;

    static Guid s_PlanktonTwoImageGUID;
    static Guid s_SecondNetImageGUID;

    void OnEnable()
    {
        s_SecondNetImageGUID = m_ImageLibrary[3].guid;
        s_PlanktonTwoImageGUID = m_ImageLibrary[1].guid;
        
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
            if (image.referenceImage.guid == s_SecondNetImageGUID)
            {
                m_SpawnedSecondNetPrefab = Instantiate(m_SecondNetPrefab, image.transform.position, image.transform.rotation);
                m_ThreeNumberManager = m_SpawnedSecondNetPrefab.GetComponent<NumberManager>();
            }
            else if (image.referenceImage.guid == s_PlanktonTwoImageGUID)
            {
                m_SpawnedPlanktonTwo = Instantiate(m_PlanktonTwo, image.transform.position, image.transform.rotation);
                m_FourNumberManager = m_SpawnedPlanktonTwo.GetComponent<NumberManager>();
            }
        }
        
        // updated, set prefab position and rotation
        foreach(ARTrackedImage image in obj.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (image.trackingState == TrackingState.Tracking)
            {
                if (image.referenceImage.guid == s_SecondNetImageGUID)
                {
                    m_ThreeNumberManager.Enable3DNumber(true);
                    m_SpawnedSecondNetPrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                }
                else if (image.referenceImage.guid == s_PlanktonTwoImageGUID)
                {
                    m_FourNumberManager.Enable3DNumber(true);
                    m_SpawnedPlanktonTwo.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                }
            }
            // image is no longer tracking, disable visuals TrackingState.Limited TrackingState.None
            else
            {
                if (image.referenceImage.guid == s_SecondNetImageGUID)
                {
                    m_ThreeNumberManager.Enable3DNumber(false);
                }
                else if (image.referenceImage.guid == s_PlanktonTwoImageGUID)
                {
                    m_FourNumberManager.Enable3DNumber(false);
                }
            }
        }
        
        // removed, destroy spawned instance
        foreach(ARTrackedImage image in obj.removed)
        {
            if (image.referenceImage.guid == s_SecondNetImageGUID)
            {
                Destroy(m_SpawnedSecondNetPrefab);
            }
            else if (image.referenceImage.guid == s_SecondNetImageGUID)
            {
                Destroy(m_SpawnedPlanktonTwo);
            }
        }
    }

    public int SecondNumberOfTrackedImages()
    {
        m_SecondNumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_SecondNumberOfTrackedImages++;
            }
        }
        return m_SecondNumberOfTrackedImages;
    }
}
