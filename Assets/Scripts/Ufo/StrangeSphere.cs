using Events;
using SerializationSystem;
using System;
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
        private readonly float _minRateSpeed = 0.1f;
        public float FireRate { get; private set; }
        public event Action<object, float> RateTimeChangedEvent;
        public event Action<object, float> DamageChangedEvent;

        private void OnEnable()
        {
            UfoEventsBus.RateSpeedChangedEvent += OnRateSpeedChanged;
            UfoEventsBus.DamageChangedEvent += OnDamageChanged;
        }

        private void Start()
        {
            _bullet.SetDamage(UfoStatsHandler.Instance.GameData.Damage);
            FireRate = UfoStatsHandler.Instance.GameData.FireRate;
        }

        private void OnDamageChanged(object sender, float damage)
        {
            _bullet.AddDamage(damage);
            if(_bullet.IsAddDamge)
                DamageChangedEvent?.Invoke(sender, _bullet.Damage);
        }

        private void OnRateSpeedChanged(object sender, float rateSpeed)
        {
            if(FireRate > _minRateSpeed)
            {
                FireRate -= rateSpeed;
                FireRate = Mathf.Max(FireRate, _minRateSpeed);
                RateTimeChangedEvent?.Invoke(sender, FireRate);
            }
            else
            {
                Debug.Log($"Ufo has minRateTime {FireRate}");
            }
        }

        private void Update()
        {
            
            _yPosition = Input.GetAxis("Vertical");
            _angle = transform.rotation.eulerAngles.z;

            SetOffset(_angle);

            _timeToShoot += Time.deltaTime;

            FocusSwitcher();

            if (Input.GetMouseButton(0) && _timeToShoot > FireRate)
            {
                Fire();
                _timeToShoot = 0;
            }

        }

        private void FocusSwitcher()
        {
            if (Input.GetMouseButton(0))
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
            UfoEventsBus.RateSpeedChangedEvent -= OnRateSpeedChanged;
            UfoEventsBus.DamageChangedEvent -= OnDamageChanged;
        }

    }
}