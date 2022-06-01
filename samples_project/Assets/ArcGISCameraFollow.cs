using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

public class ArcGISCameraFollow : MonoBehaviour
{
    private HPTransform cameraTransform;
    public HPTransform target;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public float smoothing =3f;

    // Start is called before the first frame update
    private void Start()
    {
        cameraTransform = GetComponent<HPTransform>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //cameraTransform.LocalPosition = new double3(Vector3.Lerp(cameraTransform.LocalPosition.ToVector3(), target.LocalPosition.ToVector3() + offset, smoothing * Time.deltaTime));
        transform.LookAt(target.transform.position);
    }
}