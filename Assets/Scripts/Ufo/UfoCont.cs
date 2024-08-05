using Events;
using SerializationSystem;
using System;
using UnityEngine;


namespace UfoComponents
{

    [RequireComponent(typeof(CharacterController))]
    public class UfoCont : MonoBehaviour
    {
        private float _gravity = -9.81f;
        private bool _isForcing;
        private CharacterController _ufo;
        private readonly float _maxFlySpeed = 15;

        public event Action<object, float> FlySpeedChangedEvent;

        public float MoveDir { get; private set; }
        public float FlySpeed { get; private set; }


        private void Awake()
        {
            _ufo = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            UfoEventsBus.FlySpeedChangedEvent += OnFlySpeedChanged;
        }

        private void Start()
        {
            FlySpeed = UfoStatsHandler.Instance.GameData.FlySpeed;
        }

        private void OnFlySpeedChanged(object sender, float flySpeed)
        {
            if(FlySpeed < _maxFlySpeed) 
            {
                FlySpeed += flySpeed;
                FlySpeed = Mathf.Min(FlySpeed, _maxFlySpeed);
                FlySpeedChangedEvent?.Invoke(sender, FlySpeed); 
            }
            else
            {
                Debug.Log($"Ufo has max Fly Speed {FlySpeed}");
            }
        }

        private void FixedUpdate()
        {
            MoveFD();

            if (_isForcing)
                AddForce();
            else
                DoGravity();
           
        }

        private void Update()
        {
            Move();

            if (Input.GetKey(KeyCode.Space))
                _isForcing = true;
            else
                _isForcing = false;

        }

        private void Move()
        {
            MoveDir = Input.GetAxis("Horizontal");
        }

        private void DoGravity()
        {
            _ufo.Move(Vector3.up * _gravity * Time.fixedDeltaTime);
        }

        private void MoveFD()
        {
            _ufo.Move(Vector3.right * MoveDir * Time.fixedDeltaTime * FlySpeed);
        }

        private void AddForce()
        {
            _ufo.Move(Vector3.up * FlySpeed * Time.fixedDeltaTime);
        }

        public void MoveLeft()
        {
            _ufo.Move(-Vector3.right * 100 * Time.deltaTime);
        }

        public void MoveRight()
        {
            _ufo.Move(Vector3.right * 100 * Time.deltaTime);
        }

        private void OnDisable()
        {
            UfoEventsBus.FlySpeedChangedEvent -= OnFlySpeedChanged;
        }
    }

}

