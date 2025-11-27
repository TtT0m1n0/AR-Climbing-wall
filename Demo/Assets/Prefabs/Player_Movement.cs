using UnityEngine;
using UnityEngine.InputSystem;   
public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;      
    public float rotationSpeed = 90f;  
    public float lookSpeed = 60f;      
    public Transform cam;              

    private float pitch = 0f;          

    void Start()
    {
       
        if (cam == null && Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return; 

        
        float moveInput = 0f;
        if (kb.wKey.isPressed) moveInput += 1f;
        if (kb.sKey.isPressed) moveInput -= 1f;

        float yawInput = 0f;
        if (kb.dKey.isPressed) yawInput += 1f;
        if (kb.aKey.isPressed) yawInput -= 1f;

        float pitchInput = 0f;
        if (kb.qKey.isPressed) pitchInput += 1f;
        if (kb.eKey.isPressed) pitchInput -= 1f;

        
        Vector3 move = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        transform.position += move;

        
        transform.Rotate(0f, yawInput * rotationSpeed * Time.deltaTime, 0f);

        
        if (cam != null)
        {
            pitch += pitchInput * lookSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -80f, 80f);      
            cam.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }
    }
}
