using UnityEngine;

namespace UfoComponents
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothing = 1f;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offset, Time.fixedDeltaTime * _smoothing);

            transform.position = nextPosition;
        }

        public void SetOffset(Vector3 offset)
        {
            _offset = offset;
        }

        public Vector3 GetOffset()
        {
            return _offset;
        }
    }
}