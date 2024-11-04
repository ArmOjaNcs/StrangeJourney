using UnityEngine;

public interface IDetectable 
{
    event ObjectDetectedHandler OnGameObjectDetectEvent;
    event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    GameObject GameObj { get; }

    void Detected(GameObject detectionSource);
    void DetectionReleased(GameObject detectionSource);
}
