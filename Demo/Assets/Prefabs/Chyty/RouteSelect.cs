using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class RouteSelect : MonoBehaviour
{
    public int holdId;

    public Color normalColor = Color.gray;      
    public Color selectedColor = Color.white;   
    public bool useEmission = true;             

    public bool isSelected = false;

    private Renderer rend;
    private Color originalColor;                

    void Awake()
    {
        rend = GetComponent<Renderer>();

        originalColor = rend.material.color;
        if (normalColor == Color.gray) 
            normalColor = originalColor;

        ApplyVisual();
    }

    void OnMouseDown()
    {
        if (RouteCreateController.Instance == null ||
            !RouteCreateController.Instance.isSelecting)
            return;

        RouteCreateController.Instance.ToggleRouteSelect(this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        ApplyVisual();
    }

    public void SetBaseColor(Color c)
    {
        normalColor = c;
        if (!isSelected)
            ApplyVisual();
    }

    private void ApplyVisual()
    {
        if (rend == null) rend = GetComponent<Renderer>();
        var mat = rend.material;

        Color c = isSelected ? selectedColor : normalColor;
        mat.color = c;

        if (useEmission)
        {
            if (isSelected)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", c);
            }
            else
            {
                mat.SetColor("_EmissionColor", Color.black);
                mat.DisableKeyword("_EMISSION");
            }
        }
    }
}
