using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraOverride;
    private Transform cameraTransform;

    void Start()
    {
        if (cameraOverride != null)
        {
            cameraTransform = cameraOverride;
        }
        else
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        transform.LookAt(cameraTransform.position);
        transform.Rotate(Vector3.up, 180f);
    }
}
