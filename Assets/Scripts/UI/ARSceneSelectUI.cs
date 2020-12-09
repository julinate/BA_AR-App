using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class ARSceneSelectUI : MonoBehaviour
    {
       /*

        [SerializeField]
        Scrollbar m_HorizontalScrollBar;
        public Scrollbar horizontalScrollBar
        {
            get => m_HorizontalScrollBar;
            set => m_HorizontalScrollBar = value;
        }

        [SerializeField]
        Scrollbar m_VerticalScrollBar;
        public Scrollbar verticalScrollBar
        {
            get => m_VerticalScrollBar;
            set => m_VerticalScrollBar = value;
        }

        [SerializeField]
        GameObject m_AllMenu;
        public GameObject allMenu
        {
            get => m_AllMenu;
            set => m_AllMenu = value;
        }

        [SerializeField]
        GameObject m_PlasticToOceanMenu;
        public GameObject plasticToOceanMenu
        {
            get => m_PlasticToOceanMenu;
            set => m_PlasticToOceanMenu = value;
        }

        [SerializeField]
        GameObject m_PlasticVsFishMenu;
        public GameObject plasticVsFishMenu
        {
            get => m_PlasticVsFishMenu;
            set => m_PlasticVsFishMenu = value;
        }

        [SerializeField]
        GameObject m_PlasticToFoodChainMenu;
        public GameObject plasticToFoodChainMenu
        {
            get => m_PlasticToFoodChainMenu;
            set => m_PlasticToFoodChainMenu = value;
        }

        [SerializeField]
        GameObject m_PlasticHealthRiscMenu;
        public GameObject plasticHealthRiscMenu
        {
            get => m_PlasticHealthRiscMenu;
            set => m_PlasticHealthRiscMenu = value;
        }

        [SerializeField]
        GameObject m_ScanProductsMenu;
        public GameObject scanProductsMenu
        {
            get => m_ScanProductsMenu;
            set => m_ScanProductsMenu = value;
        }
       */


       /* 
        void Start()
        {
            if(ActiveMenu.currentMenu == MenuType.FaceTracking)
            {
                m_FaceTrackingMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            else if(ActiveMenu.currentMenu == MenuType.ImageTracking)
            {
                m_ImageTrackingMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            else if(ActiveMenu.currentMenu == MenuType.PlaneDetection)
            {
                m_PlaneDetectionMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            else if(ActiveMenu.currentMenu == MenuType.BodyTracking)
            {
                m_BodyTrackingMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            else if(ActiveMenu.currentMenu == MenuType.Meshing)
            {
                m_MeshingMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            else if(ActiveMenu.currentMenu == MenuType.Depth)
            {
                m_DepthMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            else if(ActiveMenu.currentMenu == MenuType.LightEstimation)
            {
                m_LightEstimationMenu.SetActive(true);
                m_AllMenu.SetActive(false);
            }
            ScrollToStartPosition();
        }

        */

        static void LoadScene(string sceneName)
        {
            LoaderUtility.Initialize();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void ChatButtonPressed()
        {
            LoadScene("Chat");
        }
        
        public void PlasticToOceanButtonPressed()
        {
            LoadScene("PlasticToOcean");
        }

        public void PlasticVsFishButtonPressed()
        {
            LoadScene("PlasticVsFish");
        }

        public void PlasticToFoodChainButtonPressed()
        {
            LoadScene("Foodchain");
        }

        public void PlasticHealthRiscButtonPressed()
        {
            LoadScene("PlasticHealthRisc");
        }

        public void ScanProductsButtonPressed()
        {
            LoadScene("Productscan");
        }

        /*
        void ScrollToStartPosition()
        {
            m_HorizontalScrollBar.value = 0;
            m_VerticalScrollBar.value = 1;
        }
        */
    }
}
        