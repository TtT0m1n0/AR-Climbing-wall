using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class ClimbingHold : MonoBehaviour
{
    [Header("Colors")]
    public Color baseColor = Color.orange;      
    public Color litColor = Color.red;     

    bool isLit = false;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        ApplyState(false);
    }

    public void Toggle()
    {
        isLit = !isLit;
        ApplyState(isLit);
    }

    void ApplyState(bool lit)
    {
        if (rend == null) return;

        var mat = rend.material;

        
        mat.SetColor("_Color", baseColor);

        if (lit)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", litColor);
        }
        else
        {
            mat.SetColor("_EmissionColor", Color.black);
            mat.DisableKeyword("_EMISSION");
        }
    }
}
