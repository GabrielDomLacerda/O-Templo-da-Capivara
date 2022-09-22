using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Framing")]
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Transform followTransform = null;
    [SerializeField] private Vector2 framing = new Vector2(0, 0);

    [Header("Distance")]
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField][Range(-90, 90)] private float minDistance = 0f;
    [SerializeField][Range(-90, 90)] private float maxDistance = 10f;
    [SerializeField][Range(-90, 90)] private float defaultDistance = 5f;

    [Header("Rotation")]
    [SerializeField] private bool invertX = false;
    [SerializeField] private bool invertY = false;
    [SerializeField] private float defaultVerticalAngle = 20f;
    [SerializeField] private float rotationSharpness = 25f;
    [SerializeField][Range(-90, 90)] private float minYAngle = -90f;
    [SerializeField][Range(-90, 90)] private float maxYAngle = 90f;

    [Header("Obstacles")]
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask obstructionLayers = -1;
    private List<Collider> ignoreColliders = new List<Collider>();

    public Vector3 CameraPlanarDirection { get => planarDirection; }

    private float targetDistance;
    private Vector3 planarDirection;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float targetVerticalAngle;

    private Vector3 newPosition;
    private Quaternion newRotation;

    private void OnValidate()
    {
        defaultDistance = Mathf.Clamp(defaultDistance, minDistance, maxDistance);
        defaultVerticalAngle = Mathf.Clamp(defaultVerticalAngle, minYAngle, maxYAngle);
    }

    void Start()
    {
        ignoreColliders.AddRange(GetComponentsInChildren<Collider>());

        planarDirection = followTransform.forward;

        //calcular alvo
        targetDistance = defaultDistance;
        targetVerticalAngle = defaultVerticalAngle;

        targetRotation = Quaternion.LookRotation(planarDirection) * Quaternion.Euler(targetVerticalAngle, 0, 0);
        targetPosition = followTransform.position - (targetRotation * Vector3.forward) * targetDistance;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }
        float mouseX = PlayerInputs.MouseXInput;
        float mouseY = PlayerInputs.MouseYInput;
        float mouseScroll = -PlayerInputs.MouseScrollInput * zoomSpeed;

        if (invertX) { mouseX *= -1; }
        if (invertY) { mouseY *= -1; }

        Vector3 focusPosition = followTransform.position + new Vector3(framing.x, framing.y, 0);

        targetDistance = Mathf.Clamp(targetDistance + mouseScroll, minDistance, maxDistance);
        planarDirection = Quaternion.Euler(0, mouseX, 0) * planarDirection;
        targetVerticalAngle = Mathf.Clamp(targetVerticalAngle + mouseY, minYAngle, maxYAngle);

        Debug.DrawLine(_camera.transform.position, _camera.transform.position + planarDirection, Color.red);

        //tratando obstaculos
        float smallestDistance = targetDistance;
        RaycastHit[] hits = Physics.SphereCastAll(
            focusPosition, checkRadius, targetRotation * -Vector3.forward, targetDistance, obstructionLayers
        );

        if (hits.Length != 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (!ignoreColliders.Contains(hit.collider))
                {
                    if (hit.distance < smallestDistance) { smallestDistance = hit.distance; }
                }
            }
        }

        //alvo final
        targetRotation = Quaternion.LookRotation(planarDirection) * Quaternion.Euler(targetVerticalAngle, 0, 0);
        targetPosition = focusPosition - (targetRotation * Vector3.forward) * smallestDistance;

        //tratando suavização no zoom
        newRotation = Quaternion.Slerp(_camera.transform.rotation, targetRotation, rotationSharpness * Time.deltaTime);
        newPosition = Vector3.Lerp(_camera.transform.position, targetPosition, rotationSharpness * Time.deltaTime);

        //aplicando na camera
        _camera.transform.rotation = newRotation;
        _camera.transform.position = newPosition;
    }
}
