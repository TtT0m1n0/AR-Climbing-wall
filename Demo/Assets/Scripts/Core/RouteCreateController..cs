using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class RouteCreateController : MonoBehaviour
{
    public static RouteCreateController Instance;

    [Header("Panels")]
    public GameObject createRoutePanel;
    public GameObject selectHoldsPanel;

    [Header("Input Fields")]
    public TMP_InputField inputName;
    public TMP_Dropdown dropdownDifficulty;
    public TMP_Dropdown dropdownColor;

    [Header("Buttons")]
    public Button btnSave;
    public Button btnCancel;

    [Header("Selection")]
    public bool isSelecting = false;
    public List<HoldSelectable> selectedHolds = new List<HoldSelectable>();

    void Awake()
    {
        Instance = this;
    }

    public void StartSelecting()
    {
        isSelecting = true;
        selectedHolds.Clear();

        createRoutePanel.SetActive(false);
        selectHoldsPanel.SetActive(true);

        Debug.Log("Selection mode ON");
    }

    public void StopSelecting()
    {
        isSelecting = false;

        foreach (var h in selectedHolds)
            h.SetSelected(false);

        selectedHolds.Clear();

        selectHoldsPanel.SetActive(false);
        createRoutePanel.SetActive(true);

        Debug.Log("Selection mode OFF");
    }

    public void ToggleHoldSelection(HoldSelectable hold)
    {
        if (selectedHolds.Contains(hold))
        {
            selectedHolds.Remove(hold);
            hold.SetSelected(false);
        }
        else
        {
            selectedHolds.Add(hold);
            hold.SetSelected(true);
        }

        Debug.Log("Vybrané chyty: " + selectedHolds.Count);
    }

    public List<int> GetSelectedHoldIds()
    {
        List<int> ids = new List<int>();
        foreach (var h in selectedHolds)
            ids.Add(h.holdId);

        return ids;
    }

    public void SaveRoute()
    {
         Debug.Log("SaveRoute START");

        Debug.Log("inputName = " + (inputName ? "OK" : "NULL"));
        Debug.Log("dropdownDifficulty = " + (dropdownDifficulty ? "OK" : "NULL"));
        Debug.Log("dropdownColor = " + (dropdownColor ? "OK" : "NULL"));
        Debug.Log("GameManager.Instance = " + (GameManager.Instance ? "OK" : "NULL"));

        string routeName = inputName.text;
        string difficulty = dropdownDifficulty.options[dropdownDifficulty.value].text;
        string color = dropdownColor.options[dropdownColor.value].text;

        var route = new RouteData
        {
            routeId = System.Guid.NewGuid().ToString(),
            wallId = GameManager.Instance.selectedWallId,
            routeName = routeName,
            difficulty = difficulty,
            color = color,
            holds = GetSelectedHoldIds()
        };

        RouteSaver.SaveRoute(route);

        Debug.Log("Saved route: " + route.routeName);

        StopSelecting();
    }

}
