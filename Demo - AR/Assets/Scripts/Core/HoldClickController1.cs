using UnityEngine;
using UnityEngine.InputSystem;

public class HoldClickController1 : MonoBehaviour
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
        if (Mouse.current == null)
            return;

        if (RouteCreateController.Instance == null ||
            !RouteCreateController.Instance.isSelecting)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                var hold = hit.collider.GetComponent<HoldSelectable>();
                if (hold != null)
                {
                    RouteCreateController.Instance.ToggleHoldSelection(hold);
                }
            }
        }
    }
}
