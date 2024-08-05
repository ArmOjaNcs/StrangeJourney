using Events;
using SerializationSystem;
using System;
using UnityEngine;

namespace UfoComponents
{

    public class UfoHPandEnergy : MonoBehaviour
    {
        private int _maxHp;
        private int _maxEnergy;

        private readonly int _maxHpTotal = 100; 
        private readonly int _maxEnergyTotal = 100; 
        public int HitPoints { get; private set; }
        public int Energy { get; private set; }

        public event Action<object, int> EnergyChangedEvent;
        public event Action<object, int> HPChangedEvent;
        private void OnEnable()
        {
            UfoEventsBus.EnergyChangedEvent += OnEnergyChanged;
            UfoEventsBus.HPChangedEvent += OnHPChanged;
            
        }

        private void Start()
        {
            HitPoints = UfoStatsHandler.Instance.GameData.Hitpoints;
            Energy = UfoStatsHandler.Instance.GameData.Energy;

            _maxHp = UfoStatsHandler.Instance.GameData.Hitpoints;
            _maxEnergy = UfoStatsHandler.Instance.GameData.Energy;
        }

        private void OnHPChanged(object sender, int Hp)
        {
            if(HitPoints < _maxHpTotal)
            {
                HitPoints += Hp;
                HitPoints = Mathf.Min(HitPoints, _maxHpTotal);
                _maxHp = HitPoints;
                HPChangedEvent?.Invoke(sender, HitPoints);
            }
            else
            {
                Debug.Log($"Ufo has max HP {HitPoints}");
            }
        }

        private void OnEnergyChanged(object sender, int energy)
        {
            if (Energy < _maxEnergyTotal)
            {
                Energy += energy;
                Energy = Mathf.Min(Energy, _maxEnergyTotal);
                _maxHp = HitPoints;
                EnergyChangedEvent?.Invoke(sender, Energy);
            }
            else
            {
                Debug.Log($"Ufo has max Energy {HitPoints}");
            }
        }

        private void OnDisable()
        {
            UfoEventsBus.EnergyChangedEvent -= OnEnergyChanged;
            UfoEventsBus.HPChangedEvent -= OnHPChanged;
        }
    }
}
