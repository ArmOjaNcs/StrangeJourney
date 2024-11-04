using UnityEngine;

namespace UfoComponents
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothing = 1f;
        [SerializeField] private bool _isCamera = true;

        private void OnEnable()
        {
            UfoStats.StatsChangedEvent += OnStatsChanged;
        }

        private void Start()
        {
            if (_isCamera)
                _smoothing = UfoStats.Instance.CameraSmoothing;
            else
                _smoothing = UfoStats.Instance.SphereSmoothing;
        }

        private void OnStatsChanged()
        {
            if (_isCamera)
                _smoothing = UfoStats.Instance.CameraSmoothing;
            else
                _smoothing = UfoStats.Instance.SphereSmoothing;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + 
                _offset, Time.fixedDeltaTime * _smoothing);

            transform.position = nextPosition;
        }

        public void SetOffset(Vector3 offset)
        {
            _offset = offset;
        }

        private void OnDisable()
        {
            UfoStats.StatsChangedEvent -= OnStatsChanged;
        }

    }
}