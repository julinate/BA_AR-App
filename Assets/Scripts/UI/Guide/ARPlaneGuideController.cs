using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneGuideController : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }

    [SerializeField]
    GameObject UXPlaneGuide;
    
    [SerializeField]
    GameObject UXPlaceGuide;



    void Start()
    {
        if (!PlanesFound) 
        { 
            UXPlaneGuide.SetActive(true);
            UXPlaceGuide.SetActive(false);
        }
    }

    public void Update()
    {
        if (PlanesFound)
        {
            UXPlaneGuide.SetActive(false);
            UXPlaceGuide.SetActive(true);
            //Destroy(UXGuide);
        }
        if (!planeManager.enabled)
        {
            UXPlaceGuide.SetActive(false);
        }
    }

    bool PlanesFound => m_PlaneManager?.trackables.count > 0;
}
