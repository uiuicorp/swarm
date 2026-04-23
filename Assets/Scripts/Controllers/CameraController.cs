using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Alvo")]
    [SerializeField] private Transform player;

    [Header("Movimento")]
    [SerializeField] private float smoothSpeed = 2.5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    [Header("Zoom (Scroll)")]
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float minZoomSize = 3f;  // Zoom In (mais perto)
    [SerializeField] private float maxZoomSize = 5f;  // Zoom Out (mais longe)

    // Referência à câmera
    private Camera cam;

    // Valor lido do scroll
    private float scrollInput;
    private float currentZoomSize;

    private void Awake()
    {
        // Cache da câmera
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Camera Follow: Player não atribuído!");
            return;
        }

        // Inicializa com o Size atual da câmera
        currentZoomSize = cam.orthographicSize;
    }

    public void OnZoomPerformed(InputAction.CallbackContext context)
    {
        scrollInput = context.ReadValue<Vector2>().y;
    }

    private void Update()
    {
        if (scrollInput != 0f)
        {
            // Aplica o zoom no Size da câmera
            currentZoomSize -= scrollInput * zoomSpeed * Time.deltaTime;
            currentZoomSize = Mathf.Clamp(currentZoomSize, minZoomSize, maxZoomSize);
            cam.orthographicSize = currentZoomSize;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Posição desejada (X e Y do player + offset, Z fixo)
        Vector3 desiredPosition = player.position + offset;

        // Suavização
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Aplicação
        transform.position = smoothedPosition;
    }
}