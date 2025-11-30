using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject selectRoutePanel;
    public GameObject createRoutePanel;

    [Header("Route List Setup")]
    public Transform routesContent;
    public GameObject routeButtonTemplate;

    void Start()
    {
        selectRoutePanel.SetActive(false);
    }

    public void OnSelectRouteClicked()
    {
        Debug.Log("► Klik na Vybrať cestu!");
        selectRoutePanel.SetActive(true);

        foreach (Transform child in routesContent)
            Destroy(child.gameObject);

        List<string> wallFiles = WallLoader.GetAllWallFiles();

        foreach (string wallPath in wallFiles)
        {
            WallData wall = WallLoader.LoadWall(wallPath);
            RouteCollection routes = RouteLoader.LoadRoutes(wall.wallId);
            if (routes == null) continue;

            foreach (RouteData r in routes.routes)
            {
                GameObject btn = Instantiate(routeButtonTemplate, routesContent);
                btn.SetActive(true);

                TMP_Text t = btn.GetComponentInChildren<TMP_Text>();
                t.text = $"{wall.wallId} – {r.routeId}";

                string selectedWallPath = wallPath;
                string selectedWallId = wall.wallId;
                string selectedRoute = r.routeId;

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.Instance.selectedWallPath = selectedWallPath;
                    GameManager.Instance.selectedRouteId = selectedRoute;
                    GameManager.Instance.selectedWallId = selectedWallId;

                    SceneManager.LoadScene("Viewer");
                });
            }
        }
    }   // ← TÁTO CHÝBALA !!!

    public void CloseSelectPanel()
    {
        selectRoutePanel.SetActive(false);
    }

    public void OnCreateRouteClicked()
    {
        createRoutePanel.SetActive(true);
    }

    public void CloseCreatePanel()
    {
        createRoutePanel.SetActive(false);
    }
}
