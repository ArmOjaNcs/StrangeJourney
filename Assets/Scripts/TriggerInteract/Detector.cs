using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Detector : MonoBehaviour, IDetector
{
    public event ObjectDetectedHandler OnGameObjectDetectedEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public GameObject[] DetectedObjects => DetectedObjects.ToArray();

    private List<GameObject> _detectedObjects = new List<GameObject>();

    public void Detect(IDetectable detectableObj)
    {
        if (!_detectedObjects.Contains(detectableObj.GameObj))
        {
            detectableObj.Detected(gameObject);
            _detectedObjects.Add(detectableObj.GameObj);

            OnGameObjectDetectedEvent?.Invoke(gameObject, detectableObj.GameObj);
        }
    }

    public void Detect(GameObject detectedObj)
    {
        if (!_detectedObjects.Contains(detectedObj))
        {
            _detectedObjects.Add(detectedObj);

            OnGameObjectDetectedEvent?.Invoke(gameObject, detectedObj);
        }
    }

    public void ReleaseDetection(IDetectable detectableObj)
    {
        if (_detectedObjects.Contains(detectableObj.GameObj))
        {
            detectableObj.DetectionReleased(gameObject);
            _detectedObjects.Remove(detectableObj.GameObj);

            OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectableObj.GameObj);
        }
    }

    public void ReleaseDetection(GameObject detectedObj)
    {
        if (_detectedObjects.Contains(detectedObj))
        {
            _detectedObjects.Remove(detectedObj);

            OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectedObj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsColliderDetectedObject(other, out var detectedObject))
        {
            Detect(detectedObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsColliderDetectedObject(other, out var detectedObject))
        {
            ReleaseDetection(detectedObject);
        }
    }

    private bool IsColliderDetectedObject(Collider objectCollider, out IDetectable detectedObject)
    {
        detectedObject = objectCollider.GetComponentInParent<IDetectable>();

        return detectedObject != null;
    }
}
