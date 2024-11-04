using UIElements;
using UnityEngine;

namespace UfoComponents
{
    public class StrangeSphere : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _focusSpeed;
        [SerializeField] private CameraFollower _sphere;
        private float _timeToShoot;
        private float _yPosition;
        private float _angle;
        private bool _isFocusing;
        
        public bool IsShooting;

        public float FireRate { get; private set; }

        private void OnEnable()
        {
            UfoStats.StatsChangedEvent += OnStatsChanged;
            UfoHPandEnergy.OnSubZeroHPEvent += Die;
        }

        private void Start()
        {
            _bullet.SetDamage(UfoStats.Instance.Damage);
            _bullet.SetSpeed(UfoStats.Instance.BulletSpeed);
            FireRate = UfoStats.Instance.FireRate;
        }

        private void OnStatsChanged()
        {
            _bullet.SetDamage(UfoStats.Instance.Damage);
            _bullet.SetSpeed(UfoStats.Instance.BulletSpeed);
            FireRate = UfoStats.Instance.FireRate;
            Debug.Log($"Ufo FireRate is {FireRate}");
            Debug.Log($"Ufo Damage is {_bullet.Damage}");
        }

        private void Update()
        {
            _angle = transform.rotation.eulerAngles.z;
            _timeToShoot += Time.deltaTime;
            
            SetOffset(_angle);

            FocusSwitcher();

            if (IsShooting && _timeToShoot > FireRate)
            {
                Fire();
                _timeToShoot = 0;
            }
        }

        private void FocusSwitcher()
        {
            if (IsShooting)
                _isFocusing = true;
            else
                _isFocusing = false;
        }

        private void FixedUpdate()
        {

            Rotate();

            if (_isFocusing)
                GetFocus();
            
        }


        private void Fire()
        {
            var bullet = Instantiate(_bullet, transform.position, transform.rotation);
        }

        private void Rotate()
        {
            transform.Rotate(new Vector3(0, 0, _yPosition * _rotationSpeed * Time.fixedDeltaTime));
        }

        private void GetFocus()
        {
            transform.Translate(Vector3.right * _focusSpeed * Time.fixedDeltaTime);
        }

        private void SetOffset(float angle)
        {

            if ((angle < 45 && angle > 0) || (angle < 360 && angle > 315))
            {
                _sphere.SetOffset(new Vector3(5, 0, 0));
                return;
            }
            if (angle < 315 && angle > 225)
            {
                _sphere.SetOffset(new Vector3(0, -5, 0));
                return;
            }
            if (angle < 225 && angle > 135)
            {
                _sphere.SetOffset(new Vector3(-5, 0, 0));
                return;
            }
            if (angle < 135 && angle > 45)
            {
                _sphere.SetOffset(new Vector3(0, 5, 0));
                return;
            }

        }

        private void SetOppositeOffset()
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wall") || other.CompareTag("ground"))
                SetOppositeOffset();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("wall") || other.CompareTag("ground"))
                SetOppositeOffset();
        }

        private void OnDisable()
        {
            UfoStats.StatsChangedEvent -= OnStatsChanged;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }

        public void SetYPos(float yPos)
        {
            _yPosition = yPos;
        }
    }
}