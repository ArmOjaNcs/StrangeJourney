using UnityEngine;

public class DetectableObject : MonoBehaviour, IDetectable
{
    public GameObject GameObj => this.gameObject;

    public event ObjectDetectedHandler OnGameObjectDetectEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public void Detected(GameObject detectionSource)
    {
        Debug.Log($"GameObject ({name}) was detected by {detectionSource.name}");
        OnGameObjectDetectEvent?.Invoke(detectionSource, gameObject);
    }

    public void DetectionReleased(GameObject detectionSource)
    {
        Debug.Log($"GameObject ({name}) detection released by {detectionSource.name}");
        OnGameObjectDetectionReleasedEvent?.Invoke(detectionSource, gameObject);
    }

}
