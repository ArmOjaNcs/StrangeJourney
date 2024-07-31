using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotateAxis;
    [SerializeField] private float _degrees;
    [SerializeField] private Vector3 _vector;

    private void Update()
    {
        transform.RotateAround(transform.position, _vector * _rotateAxis * Time.fixedDeltaTime, _degrees);
    }

}
