
using UnityEngine;
using UnityEngine.InputSystem;
public class PLAYER : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 0.1f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        // Lukitse hiiri pelin keskelle
        Cursor.lockState = CursorLockMode.Locked;
    }

    // N‰m‰ metodit kutsutaan automaattisesti, jos PlayerInput-komponentin 
    // Behavior on asetettu tilaan "Send Messages"
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Pystysuuntainen k‰‰ntyminen (kamera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Vaakasuuntainen k‰‰ntyminen (koko hahmo)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        // Liikesuunta suhteessa hahmon rintamasuuntaan
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}