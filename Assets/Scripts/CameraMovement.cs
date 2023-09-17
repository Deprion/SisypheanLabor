using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float sens, rotation, mouseX;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90, 30);
    }

    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(rotation, 90, 0);
        target.Rotate(Vector3.up * mouseX);
    }
}
