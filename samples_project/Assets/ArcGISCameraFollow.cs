using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

public class ArcGISCameraFollow : MonoBehaviour
{
    private HPTransform cameraTransform;
    public HPTransform target;
    public double3 offset;
    private Vector3 velocity = Vector3.zero;
    public float speed = 0.05f;

    // Start is called before the first frame update
    private void Start()
    {
        cameraTransform = GetComponent<HPTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        var newPosition = Vector3.SmoothDamp(cameraTransform.LocalPosition.ToVector3(), target.LocalPosition.ToVector3() + offset.ToVector3(), ref velocity, speed);
        cameraTransform.LocalPosition = new double3(newPosition);
        transform.LookAt(target.transform.position);
    }
}