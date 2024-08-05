using UnityEngine;

namespace UfoComponents
{

    public class RotateItSelf : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private Vector3 _vector;
        private void FixedUpdate()
        {
            transform.Rotate(_vector * _rotateSpeed * Time.fixedDeltaTime);
        }
    }
}