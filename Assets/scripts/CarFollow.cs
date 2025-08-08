using UnityEngine;

public class Camera : MonoBehaviour
{
     [SerializeField]
    private Transform Target; // The target to follow (usually the car)
    [SerializeField]
    private Transform CameraTransform; // The camera transform
    [SerializeField]
    private Vector3 offset; // Offset from the target
    [SerializeField]
    private float smoothTime;
    private Vector3 velocity = Vector3.zero; // Used for smooth damping
    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + offset;
        CameraTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(Target); // Make the camera look at the target
    }
}
