using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class HoldSelectable : MonoBehaviour
{
    public int holdId;
    public Color normalColor = Color.gray;
    public Color selectedColor = Color.yellow;
    public bool isSelected = false;

    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();

        if (normalColor == Color.gray)
            normalColor = rend.material.color;

        ApplyVisual();
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

    void ApplyVisual()
    {
        var mat = rend.material;

        if (isSelected)
        {
            mat.color = selectedColor;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EMISSIONColor", selectedColor);
        }
        else
        {
            mat.color = normalColor;
            mat.DisableKeyword("_EMISSION");
            mat.SetColor("_EMISSIONColor", Color.black);
        }
    }
}
