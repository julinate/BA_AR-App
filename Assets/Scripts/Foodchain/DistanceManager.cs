using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image Tracking manager that detects tracked images")]
    ImageTrackingObjectManager m_ImageTrackingObjectManager;
    
    /// <summary>
    /// Get the <c>ImageTrackingObjectManger</c>
    /// </summary>
    public ImageTrackingObjectManager imageTrackingObjectManager
    {
        get => m_ImageTrackingObjectManager;
        set => m_ImageTrackingObjectManager = value;
    }

    [SerializeField]
    [Tooltip("Prefab to be spawned and showed between numbers based on distance")]
    GameObject m_SumPrefab;

    /// <summary>
    /// Get the sum prefab
    /// </summary>
    public GameObject sumPrefab
    {
        get => m_SumPrefab;
        set => m_SumPrefab = value;
    }

    GameObject m_SpawnedSumPrefab;
    GameObject m_NetObject;
    GameObject m_PlanktonOneObject;
    float m_Distance;
    bool m_SumActive;

    const float k_SumDistance = 0.3f;

    //aus SecondDistanceManager -Start-

    [SerializeField]
    [Tooltip("Image Tracking manager that detects tracked images")]
    SecondImageTrackingObjectManager m_SecondImageTrackingObjectManager;

    /// <summary>
    /// Get the <c>ImageTrackingObjectManger</c>
    /// </summary>
    public SecondImageTrackingObjectManager secondImageTrackingObjectManager
    {
        get => m_SecondImageTrackingObjectManager;
        set => m_SecondImageTrackingObjectManager = value;
    }

    [SerializeField]
    [Tooltip("Prefab to be spawned and showed between numbers based on distance")]
    GameObject m_SecondSumPrefab;

    /// <summary>
    /// Get the sum prefab
    /// </summary>
    public GameObject secondSumPrefab
    {
        get => m_SecondSumPrefab;
        set => m_SecondSumPrefab = value;
    }

    GameObject m_SpawnedSecondSumPrefab;
    GameObject m_SecondNetObject;
    GameObject m_PlanktonTwoObject;
    float m_SecondDistance;
    bool m_SecondSumActive;

    const float k_SecondSumDistance = 0.3f;

    //aus SecondDistanceManager -Ende-

    void Start()
    {
        m_SpawnedSumPrefab = Instantiate(m_SumPrefab, Vector3.zero, Quaternion.identity);
        m_SpawnedSumPrefab.SetActive(false);
        m_SpawnedSecondSumPrefab = Instantiate(m_SecondSumPrefab, Vector3.zero, Quaternion.identity); // aus SecondDistanceManager
        m_SpawnedSecondSumPrefab.SetActive(false); // aus SecondDistanceManager
    }

    void Update()
    {
        m_NetObject = m_ImageTrackingObjectManager.spawnedNetPrefab;
        m_PlanktonOneObject = m_ImageTrackingObjectManager.spawnedPlanktonOnePrefab;
        m_SecondNetObject = m_SecondImageTrackingObjectManager.spawnedSecondNetPrefab; // aus SecondDistanceManager
        m_PlanktonTwoObject = m_SecondImageTrackingObjectManager.spawnedPlanktonTwoPrefab; // aus SecondDistanceManager

        if (m_ImageTrackingObjectManager.NumberOfTrackedImages() > 1)
        {
            m_Distance = Vector3.Distance(m_NetObject.transform.position, m_PlanktonOneObject.transform.position);

            if (m_Distance <= k_SumDistance)
            {
                if (!m_SumActive)
                {
                    m_SpawnedSumPrefab.SetActive(true);
                    m_SumActive = true;
                    //m_SpawnedSecondSumPrefab.SetActive(false);
                    //m_SecondSumActive = false;
                }
                
                m_SpawnedSumPrefab.transform.position = (m_NetObject.transform.position + m_PlanktonOneObject.transform.position) / 2;
            }
            else
            {
                m_SpawnedSumPrefab.SetActive(false);
                m_SumActive = false;
            }
        }
        else
        {
            m_SpawnedSumPrefab.SetActive(false);
            m_SumActive = false;
            //m_SpawnedSecondSumPrefab.SetActive(true);
            //m_SecondSumActive = true;
        }

        //aus SecondDistanceManager -Start-

        if (m_SecondImageTrackingObjectManager.SecondNumberOfTrackedImages() > 1)
        {
            m_SecondDistance = Vector3.Distance(m_SecondNetObject.transform.position, m_PlanktonTwoObject.transform.position);

            if (m_SecondDistance <= k_SecondSumDistance)
            {
                if (!m_SecondSumActive)
                {
                    m_SpawnedSecondSumPrefab.SetActive(true);
                    m_SecondSumActive = true;
                    //m_SpawnedSumPrefab.SetActive(false);
                    //m_SumActive = false;
                }

                m_SpawnedSecondSumPrefab.transform.position = (m_SecondNetObject.transform.position + m_PlanktonTwoObject.transform.position) / 2;
            }
            else
            {
                m_SpawnedSecondSumPrefab.SetActive(false);
                m_SecondSumActive = false;
            }
        }
        else
        {
            m_SpawnedSecondSumPrefab.SetActive(false);
            m_SecondSumActive = false;
            //m_SpawnedSumPrefab.SetActive(true);
            //m_SumActive = true;
        }

        //aus SecondDistanceManager -Ende-
    }
}
