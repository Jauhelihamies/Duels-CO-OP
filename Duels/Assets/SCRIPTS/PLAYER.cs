using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 0.1f;
    public float gravity = -9.81f; // Painovoiman voimakkuus
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity; // Tänne tallennetaan putoamisnopeus
    private float xRotation = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnLook(InputValue value) => lookInput = value.Get<Vector2>();

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        // 1. Maanpinnan tarkistus (nollataan putoamisnopeus kun ollaan maassa)
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pieni negatiivinen arvo pitää pelaajan tiukasti maassa
        }

        // 2. Horisontaalinen liike (X ja Z)
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 3. Vertikaalinen liike (Painovoima Y)
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            Debug.Log("Osuma pelaajaan!");

            Destroy(other.gameObject); // Tuhoa ammus osumasta
        }
    }

}
