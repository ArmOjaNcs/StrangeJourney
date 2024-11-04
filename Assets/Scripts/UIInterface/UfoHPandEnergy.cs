using System;
using UfoComponents;
using UnityEngine;

namespace UIElements
{

    public class UfoHPandEnergy : MonoBehaviour
    {
        [SerializeField] private ProgressBar _hpBar;
        [SerializeField] private ProgressBar _energyBar;
        private float _maxHp;
        private float _maxEnergy;
        private float _energyUpdateTimer;
        private float _collisionDamage => _maxHp * 0.1f;

        public float HitPoints { get; private set; }
        public float Energy { get; private set; }

        public static event Action OnSubZeroHPEvent;

        private void OnEnable()
        {
            UfoStats.StatsChangedEvent += OnStatsChanged;
            LeftSideSensor.OnColliderOtherLeftSideEvent += OnColliderOther;
            RightSideSensor.OnColliderOtherRightSideEvent += OnColliderOther;
        }

        private void OnColliderOther()
        {
            if (Energy >= _collisionDamage * 2)
            {
                Energy -= _collisionDamage * 2;
                _energyBar.SetValue(CurrentBarPoint(Energy, _maxEnergy));
            }
            else
            {
                var reserveEnergy = Energy / 2;

                Energy = 0;
                _energyBar.SetValue(CurrentBarPoint(Energy, _maxEnergy));
                HitPoints -= (_collisionDamage - reserveEnergy);
                _hpBar.SetValue(CurrentBarPoint(HitPoints, _maxHp));
            }

            if(HitPoints<=0)
                OnSubZeroHPEvent?.Invoke();
        }

        private void Start()
        {
            _maxHp = UfoStats.Instance.Hp;
            _maxEnergy = UfoStats.Instance.Energy;

            HitPoints = _maxHp;
            Energy = _maxEnergy;

            _hpBar.SetValue(CurrentBarPoint(HitPoints, _maxHp));
            _energyBar.SetValue(CurrentBarPoint(Energy, _maxEnergy));
        }

        private void FixedUpdate()
        {
            if (Energy < _maxEnergy)
            {
                Energy += _maxEnergy * 0.001f;
                if (Energy > _maxEnergy)
                    Energy = _maxEnergy;
            }   
            _energyBar.SetValue(CurrentBarPoint(Energy, _maxEnergy));

        }

        private void OnStatsChanged()
        {
            _maxHp = UfoStats.Instance.Hp;
            _maxEnergy = UfoStats.Instance.Energy;
            HitPoints = _maxHp;
            Energy = _maxEnergy;
            _hpBar.SetValue(CurrentBarPoint(HitPoints, _maxHp));
            _energyBar.SetValue(CurrentBarPoint(Energy, _maxEnergy));
            Debug.Log($"Ufo has {_maxHp} HP");
            Debug.Log($"Ufo has {_maxEnergy} energy");
        }

        private void OnDisable()
        {
            UfoStats.StatsChangedEvent -= OnStatsChanged;
            LeftSideSensor.OnColliderOtherLeftSideEvent -= OnColliderOther;
            RightSideSensor.OnColliderOtherRightSideEvent -= OnColliderOther;
        }

        private float CurrentBarPoint(float value, float maxValue)
        {
            return value / maxValue;
        }

    }
}
