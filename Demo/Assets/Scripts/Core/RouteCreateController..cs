using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class RouteCreateController : MonoBehaviour
{
    public static RouteCreateController Instance;

    public GameObject createRoutePanel;
    public GameObject selectHoldsPanel;

    public TMP_InputField inputName;
    public TMP_Dropdown dropdownDifficulty;
    public TMP_Dropdown dropdownColor;

    public Button btnSave;
    public Button btnCancel;

    public bool isSelecting = false;
    public List<HoldSelectable> selectedHolds = new List<HoldSelectable>();

    void Awake()
    {
        Instance = this;
    }

    public void StartSelecting()
    {
        Debug.Log("StartSelecting CALLED");

        isSelecting = true;
        selectedHolds.Clear();

        createRoutePanel.SetActive(false);
        selectHoldsPanel.SetActive(true);
    }

    public void StopSelecting()
    {
        Debug.Log("StopSelecting CALLED");

        isSelecting = false;

        foreach (var h in selectedHolds)
            h.SetSelected(false);

        selectedHolds.Clear();

        selectHoldsPanel.SetActive(false);
        createRoutePanel.SetActive(true);
    }

    public void ToggleHoldSelection(HoldSelectable hold)
    {
        bool nowSelected;

        if (selectedHolds.Contains(hold))
        {
            selectedHolds.Remove(hold);
            hold.SetSelected(false);
            nowSelected = false;
        }
        else
        {
            selectedHolds.Add(hold);
            hold.SetSelected(true);
            nowSelected = true;
        }

        int id = hold.holdId;
        Vector3 pos = hold.transform.position;

        string colorHex = "UNKNOWN";
        var rend = hold.GetComponent<Renderer>();
        if (rend != null)
            colorHex = ColorUtility.ToHtmlStringRGB(rend.material.color);

        Debug.Log(
            $"Hold {(nowSelected ? "SELECTED" : "UNSELECTED")} " +
            $"id={id}, pos=({pos.x:F2}, {pos.y:F2}, {pos.z:F2}), color=#{colorHex}"
        );

        Debug.Log("Aktuálne vybraných chytov: " + selectedHolds.Count);
    }
    public void ResetAllHolds()
    {
        isSelecting = false;
        selectedHolds.Clear();

        HoldSelectable[] allHolds = FindObjectsOfType<HoldSelectable>();
        foreach (var h in allHolds)
        {
            h.SetSelected(false);
        }

        Debug.Log("ResetAllHolds – všetky chyty odznačené");
    }

    public void SaveRoute()
    {
        if (selectedHolds.Count == 0)
        {
            Debug.LogWarning("Nie sú vybrané žiadne chyty, nie je čo uložiť.");
            return;
        }

        string routeName = string.IsNullOrEmpty(inputName.text)
            ? "Route_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
            : inputName.text;

        List<RouteHoldData> holdsToSave = new List<RouteHoldData>();

        foreach (var h in selectedHolds)
        {
            var rend = h.GetComponent<Renderer>();
            Color col = rend != null ? rend.material.color : Color.white;
            string hex = ColorUtility.ToHtmlStringRGB(col);

            holdsToSave.Add(new RouteHoldData
            {
                id = h.holdId,
                position = h.transform.position,
                color = hex
            });
        }

        RouteFileData fileData = new RouteFileData
        {
            routeName = routeName,
            holds = holdsToSave
        };

        string dir = Path.Combine(Application.dataPath, "walls");
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string path = Path.Combine(dir, routeName + ".json");

        string json = JsonUtility.ToJson(fileData, true);
        File.WriteAllText(path, json);

        Debug.Log("Cesta uložená do: " + path);

        StopSelecting();
    }
}

[System.Serializable]
public class RouteHoldData
{
    public int id;
    public Vector3 position;
    public string color;
}

[System.Serializable]
public class RouteFileData
{
    public string routeName;
    public List<RouteHoldData> holds;
}
