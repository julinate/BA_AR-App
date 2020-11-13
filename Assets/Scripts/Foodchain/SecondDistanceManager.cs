using UnityEngine;

public class SecondDistanceManager : MonoBehaviour
{
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

    void Start()
    {
        m_SpawnedSecondSumPrefab = Instantiate(m_SecondSumPrefab, Vector3.zero, Quaternion.identity);
        m_SpawnedSecondSumPrefab.SetActive(false);
    }

    void Update()
    {
        m_SecondNetObject = m_SecondImageTrackingObjectManager.spawnedSecondNetPrefab;
        m_PlanktonTwoObject = m_SecondImageTrackingObjectManager.spawnedPlanktonTwoPrefab;

        if (m_SecondImageTrackingObjectManager.SecondNumberOfTrackedImages() > 1)
        {
            m_SecondDistance = Vector3.Distance(m_SecondNetObject.transform.position, m_PlanktonTwoObject.transform.position);

            if (m_SecondDistance <= k_SecondSumDistance)
            {
                if (!m_SecondSumActive)
                {
                    m_SpawnedSecondSumPrefab.SetActive(true);
                    m_SecondSumActive = true;
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
        }
    }
}
