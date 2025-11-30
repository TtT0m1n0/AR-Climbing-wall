using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject selectRoutePanel;
    public GameObject createRoutePanel;

    [Header("Route List Setup")]
    public Transform routesContent;
    public GameObject routeButtonTemplate;

    string wallsFolderPath;

    void Start()
    {
        wallsFolderPath = Path.Combine(Application.dataPath, "walls");

        if (selectRoutePanel != null)
            selectRoutePanel.SetActive(false);
        if (createRoutePanel != null)
            createRoutePanel.SetActive(false);
    }

    public void OnCreateRouteClicked()
    {
        if (createRoutePanel != null)
            createRoutePanel.SetActive(true);
    }

    public void CloseCreatePanel()
    {
        if (createRoutePanel != null)
            createRoutePanel.SetActive(false);
    }

    public void OnSelectRouteClicked()
    {
        if (selectRoutePanel != null)
            selectRoutePanel.SetActive(true);

        RefreshRoutesList();
    }

    public void CloseSelectPanel()
    {
        if (selectRoutePanel != null)
            selectRoutePanel.SetActive(false);
    }

    void RefreshRoutesList()
    {
        foreach (Transform child in routesContent)
            Destroy(child.gameObject);

        if (!Directory.Exists(wallsFolderPath))
        {
            Debug.LogWarning("Walls folder neexistuje: " + wallsFolderPath);
            return;
        }

        string[] files = Directory.GetFiles(wallsFolderPath, "*.json");
        Debug.Log("Nájdené route json súbory: " + files.Length);

        foreach (string file in files)
        {
            string json = File.ReadAllText(file);
            RouteFileData data = JsonUtility.FromJson<RouteFileData>(json);

            GameObject btn = Instantiate(routeButtonTemplate, routesContent);
            btn.SetActive(true);

            TMP_Text t = btn.GetComponentInChildren<TMP_Text>();
            if (t != null)
                t.text = data.routeName;

            string filePath = file;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadRoute(filePath);
            });
        }
    }

    void LoadRoute(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Route file neexistuje: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        RouteFileData data = JsonUtility.FromJson<RouteFileData>(json);

        HoldSelectable[] allHolds = FindObjectsOfType<HoldSelectable>();

        foreach (var h in allHolds)
            h.SetSelected(false);

        HashSet<int> selectedIds = new HashSet<int>();
        foreach (var rh in data.holds)
            selectedIds.Add(rh.id);

        foreach (var h in allHolds)
        {
            if (selectedIds.Contains(h.holdId))
            {
                h.SetSelected(true);
            }
        }

        Debug.Log("Načítaná cesta: " + data.routeName);

        if (selectRoutePanel != null)
            selectRoutePanel.SetActive(false);
    }
}
