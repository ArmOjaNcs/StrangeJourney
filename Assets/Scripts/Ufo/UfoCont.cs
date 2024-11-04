using UIElements;
using UnityEngine;


namespace UfoComponents
{

    [RequireComponent(typeof(CharacterController))]
    public class UfoCont : MonoBehaviour
    {
        public bool IsForcing;
        public bool IsAddGravity;

        public float MoveDir { get; private set; }
        public float FlySpeed { get; private set; }

        private float _gravity = -9.81f;
        private CharacterController _ufo;

        private void Awake()
        {
            _ufo = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            UfoStats.StatsChangedEvent += OnStatsChanged;
            LeftSideSensor.OnColliderOtherLeftSideEvent += OnColliderOtherLeft;
            RightSideSensor.OnColliderOtherRightSideEvent += OnColliderOtherRight;
            UfoHPandEnergy.OnSubZeroHPEvent += Die;
        }

        private void OnColliderOtherRight()
        {
            MoveLeft();
        }

        private void OnColliderOtherLeft()
        {
            MoveRight();
        }

        private void Start()
        {
            FlySpeed = UfoStats.Instance.FlySpeed;
        }

        private void OnStatsChanged()
        {
            FlySpeed = UfoStats.Instance.FlySpeed;
        }

        private void FixedUpdate()
        {
            MoveFD();

            if(IsForcing && !IsAddGravity)
                AddForce();
            
            if( IsAddGravity && !IsForcing)
                AddGravity();

            DoGravity();

        }

       
        public void Move(Vector2 direction)
        {
            MoveDir = direction.x;
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
            _ufo.Move(Vector3.up * FlySpeed * 1.5f * Time.fixedDeltaTime);
        }

        private void AddGravity()
        {
            _ufo.Move(Vector3.up * (-FlySpeed/2 + _gravity) * Time.fixedDeltaTime);
        }

        private void MoveLeft()
        {
            _ufo.Move(-Vector3.right * 100 * Time.deltaTime);
        }

        private void MoveRight()
        {
            _ufo.Move(Vector3.right * 100 * Time.deltaTime);
        }

        private void OnDisable()
        {
            UfoStats.StatsChangedEvent -= OnStatsChanged;
            LeftSideSensor.OnColliderOtherLeftSideEvent -= OnColliderOtherLeft;
            RightSideSensor.OnColliderOtherRightSideEvent -= OnColliderOtherRight;
            UfoHPandEnergy.OnSubZeroHPEvent -= Die;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }

}

