using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

public class ArcGISCameraFollow : MonoBehaviour
{
    private HPTransform cameraTransform;
    public HPTransform target;
    public double3 offset;
    public bool followEnabled = true;
    public bool lookEnabled = true;
    public float cameraSpeed = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        cameraTransform = GetComponent<HPTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lookEnabled)
        {
            // Look at player character.
            Quaternion _lookRotation = Quaternion.LookRotation((target.LocalPosition - cameraTransform.LocalPosition).ToVector3().normalized);
            cameraTransform.UniverseRotation = _lookRotation;
        }

        if (followEnabled)
        {
            // Move camera behind player character.
            var targetCameraPoint = target.UniversePosition + offset;
            var difference = targetCameraPoint - cameraTransform.UniversePosition;
            cameraTransform.UniversePosition += difference * cameraSpeed * Time.deltaTime;
        }
    }
}