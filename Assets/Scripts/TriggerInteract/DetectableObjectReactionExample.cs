using UnityEngine;

[RequireComponent(typeof(DetectableObject))]
public class DetectableObjectReactionColor : MonoBehaviour
{
    private IDetectable _detectableObject;

    private void Awake()
    {
        _detectableObject = GetComponent<IDetectable>();
        transform.localScale *= 2;
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent += OnGameObjectDetectionReleased;
    }
    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent -= OnGameObjectDetectionReleased;
    }


    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        SetScale(2);
    }
    private void OnGameObjectDetectionReleased(GameObject source, GameObject detectedObject)
    {
        SetUnscale(2);
    }


    private void SetScale(int scale)
    {
        transform.localScale *= scale; 
    }

    private void SetUnscale(int unscale)
    {
        transform.localScale /= unscale;
    }
}
