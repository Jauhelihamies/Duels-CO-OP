using UnityEngine;
using UnityEngine.InputSystem; // MUISTA TÄMÄ

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // Lukitsee hiiren ja piilottaa sen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // UUSI TAPA: Luetaan hiiren liike suoraan
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // Huom: Mouse.current.delta palauttaa arvot pikseleinä, 
        // joten voit joutua säätämään herkkyyttä (sensitivity) pienemmäksi
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}