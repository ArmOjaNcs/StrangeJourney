using UnityEngine;

public delegate void ObjectDetectedHandler(GameObject source, GameObject detectedObject);
public interface IDetector 
{
    event ObjectDetectedHandler OnGameObjectDetectedEvent;
    event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    void Detect(IDetectable detectableObj);
    void Detect(GameObject detectedObj);

    void ReleaseDetection(IDetectable detectabledObj);
    void ReleaseDetection(GameObject detectedObj);
}
