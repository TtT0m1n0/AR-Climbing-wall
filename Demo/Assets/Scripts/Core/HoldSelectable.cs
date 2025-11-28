using UnityEngine;

public class HoldSelectable : MonoBehaviour
{
    public int holdId;
    public bool isSelected;

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseDown()
    {

        // Ak nie sme v selection mode → ignoruj klik
        if (!RouteCreateController.Instance.isSelecting)
            return;

        Debug.Log("Klik na chyt!");

        // Skús pridať / odobrať chyt
        RouteCreateController.Instance.ToggleHoldSelection(this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if (selected)
            rend.material.color = Color.white;   // vybraný chyt
        else
            rend.material.color = originalColor; // pôvodná farba
    }
}
