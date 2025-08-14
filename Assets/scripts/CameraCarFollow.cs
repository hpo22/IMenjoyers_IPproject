using UnityEngine;

public class CameraCarFollow : MonoBehaviour
{
   public Transform target; // assign player car 
    public Vector3 offset = new Vector3(0, 5, -8); // camera angle behind car
    public float mouseSensitivity = 3f;
    public float smoothSpeed = 10f;

    private float yaw = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Rotate camera with mouse
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;

        Quaternion rotation = Quaternion.Euler(0, yaw, 0);

        // Camera position
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at the target
        transform.LookAt(target.position + Vector3.up * 2f);
    }
}
