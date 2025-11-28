using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject selectRoutePanel;

    [Header("Route List Setup")]
    public Transform routesContent;
    public GameObject routeButtonTemplate;

    void Start()
    {
        // panel je defaultne skryty
        selectRoutePanel.SetActive(false);
    }

    // zavola sa po kliknuti na "Vybrať cestu"
    public void OnSelectRouteClicked()
    {
        Debug.Log("► Klik na Vybrať cestu!");

        selectRoutePanel.SetActive(true);

        // vymazat stare tlacidla
        foreach (Transform child in routesContent)
        {
            Destroy(child.gameObject);
        }

        // nacitat vsetky steny
        List<string> wallFiles = WallLoader.GetAllWallFiles();

        foreach (string wallPath in wallFiles)
        {
            WallData wall = WallLoader.LoadWall(wallPath);

            // pokus nacitat cesty pre tuto stenu
            RouteCollection routes = RouteLoader.LoadRoutes(wall.wallId);
            if (routes == null) continue;

            foreach (RouteData r in routes.routes)
            {
                // vytvor kopiu templatu
                GameObject btn = Instantiate(routeButtonTemplate, routesContent);
                btn.SetActive(true);

                // nastav text
                TMP_Text t = btn.GetComponentInChildren<TMP_Text>();
                t.text = $"{wall.wallId} – {r.routeId}";

                // pridaj akciu pri kliknuti
                string selectedWall = wallPath;
                string selectedRoute = r.routeId;

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.Instance.selectedWallPath = selectedWall;
                    GameManager.Instance.selectedRouteId = selectedRoute;

                    SceneManager.LoadScene("Viewer");
                });
            }
        }
    }

    // zavrie select panel
    public void CloseSelectPanel()
    {
        selectRoutePanel.SetActive(false);
    }
}
