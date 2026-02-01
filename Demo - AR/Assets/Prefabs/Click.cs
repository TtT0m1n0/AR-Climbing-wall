using UnityEngine;
using UnityEngine.InputSystem;   

public class HoldClickController : MonoBehaviour
{
    public Camera cam;         
    public float maxDistance = 100f;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || cam == null)
            return;

       
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                var hold = hit.collider.GetComponent<ClimbingHold>();
                if (hold != null)
                {
                    hold.Toggle();
                }
            }
        }
    }
}
