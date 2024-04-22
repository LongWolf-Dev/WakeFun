using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;

namespace VictorDev.MarkerUtils.MiniMap
{
    /// <summary>
    /// 小地圖組件，Prefab放在場景上即可
    /// <para>+ 自動抓取玩家和Main Camera</para>
    /// <para>+ 手動設置小地圖的Border、Mask、PlayerMark的圖片</para>
    /// </summary>
    public class MiniMap : MonoBehaviour
    {
        [Range(2, 15)]
        [Header(">>> MiniMap的OrthographicSize距離")]
        [SerializeField] private float orthographicSize = 8f;

        [Header(">>> 是否在地圖上顯示玩家Marker")]
        [SerializeField] private bool isPlayerMarkerVisible = true;

        [Header(">>> 要排除Character的Layer")]
        [SerializeField] private LayerMask characterLayer;

        [Space(10)]
        [Header("[1] >>> 針對單獨物件設置Marker")]
        [SerializeField] private List<MarkerForGameObject> markerForGameObject;
        [Header("[2] >>> 針對目標容器底下所有GameObject設置Marker")]
        [SerializeField] private List<MarkerForGameObject> markerForGameObjects;
        [Header("[3] >>> 針對Tag名稱的物件設置Marker")]
        [SerializeField] private List<MarkerForTag> markerForTag;


        [Space(20)]
        [SerializeField] private Camera miniMapCamera;
        [SerializeField] private GameObject playerMarker;
        [SerializeField] private float miniMapCamera_OffsetY = 10f;

        private Camera mainCamera;
        private Transform followPlayer;
        private Transform markerContainer;

        private List<GameObjectMarkerInfo> markerList = new List<GameObjectMarkerInfo>();

        private void Start()
        {
            mainCamera = Camera.main;
            followPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            markerContainer = transform.Find("MarkerContainer").transform;

            SetPlayerMarkerVisible();
            CreateMarkers();
        }

        /// <summary>
        /// 設定Camera顯示/隱藏Player物件與其Marker
        /// </summary>
        private void SetPlayerMarkerVisible()
        {
            if (playerMarker != null) playerMarker.SetActive(isPlayerMarkerVisible);
            if (miniMapCamera != null)
            {
                if (isPlayerMarkerVisible)
                    // 從目標攝影機的CullingMask裡移除指定的Layer
                    LayerMaskHandler.RemoveLayerMaskFromCamera(miniMapCamera, characterLayer);
                else
                    // 新增指定的Layer到目標攝影機的CullingMask
                    LayerMaskHandler.AddLayerMaskToCamera(miniMapCamera, characterLayer);
            }
        }

        /// <summary>
        /// 建立目標物件的Marker
        /// </summary>
        private void CreateMarkers()
        {
            // 新增Marker物件
            Action<Transform, MarkerData> createHandler = (target, markData) =>
            {
                Image marker = new GameObject($"Marker - {target.name}").AddComponent<Image>();
                marker.transform.parent = markerContainer;
                marker.transform.localScale = Vector3.one;
                marker.sprite = markData.markerSprite;
                marker.rectTransform.sizeDelta = markData.size;
                marker.transform.position = new Vector3(target.position.x, miniMapCamera_OffsetY - 1, target.position.z);
                markerList.Add(new GameObjectMarkerInfo(target.gameObject, marker));
            };

            //依照指定目標物件建立Marker
            foreach (MarkerForGameObject item in markerForGameObject)
            {
                createHandler(item.target.transform, item);
            }

            //依照指定目標容器底下所有GameObject建立Marker
            foreach (MarkerForGameObject item in markerForGameObjects)
            {
                foreach (Transform child in item.target.transform)
                {
                    createHandler(child, item);
                }
            }

            //依照指定LayerMask建立Marker
            foreach (MarkerForTag item in markerForTag)
            {
                try
                {
                    List<GameObject> tagObjectList = GameObject.FindGameObjectsWithTag(item.tagName).ToList();
                    tagObjectList.ForEach(obj =>
                    {
                        createHandler(obj.transform, item);
                    });
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[CreateMarkers] >>> Exception: {ex}");
                }
            }
        }


        void Update()
        {
            MIniMapCameraHandler();
            UpdateMarkerRotationY();
        }

        /// <summary>
        /// 處理攝機移動與旋轉
        /// <para>+ 小地圖UI組件不移動、不旋轉</para> 
        /// </summary>
        private void MIniMapCameraHandler()
        {
            //MiniMapCamera 跟隨玩家的移動
            Vector3 playerPos = followPlayer.position;
            playerPos.y += miniMapCamera_OffsetY;

            //MiniMapCamera 跟隨主攝影機的旋轉
            miniMapCamera.transform.SetPositionAndRotation(playerPos, Quaternion.Euler(90f, mainCamera.transform.eulerAngles.y, 0));
        }

        /// <summary>
        /// 當攝景機旋轉時，Marker要跟著反方向旋轉，以保持角度正確
        /// </summary>
        private void UpdateMarkerRotationY()
        {
            foreach (GameObjectMarkerInfo item in markerList)
            {
                item.marker.transform.rotation = Quaternion.Euler(90f, 0, -mainCamera.transform.eulerAngles.y);
                item.OnUpdateActive();
            }
        }

        private void OnValidate()
        {
            if (miniMapCamera != null) miniMapCamera.orthographicSize = orthographicSize;
            SetPlayerMarkerVisible();
        }

        [Serializable]
        private class MarkerForGameObject : MarkerData
        {
            public Transform target;
        }

        [Serializable]
        private class MarkerForTag : MarkerData
        {
            public string tagName;
        }

        private class MarkerData
        {
            public Sprite markerSprite;
            public Vector2 size;
        }

        private class GameObjectMarkerInfo
        {
            public GameObject sourceTarget;
            public Image marker;

            public GameObjectMarkerInfo(GameObject sourceTarget, Image marker)
            {
                this.sourceTarget = sourceTarget;
                this.marker = marker;
            }

            public void OnUpdateActive() => marker.gameObject.SetActive(sourceTarget.activeSelf);
        }
    }
}
