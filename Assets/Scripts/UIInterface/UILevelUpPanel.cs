using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class UILevelUpPanel : MonoBehaviour
    {
        [SerializeField] private Button _speedUp;
        [SerializeField] private Button _fireRateUp;
        [SerializeField] private Button _damageUp;
        [SerializeField] private Button _hpUp;
        [SerializeField] private Button _energyUp;

        public static event Action OnSpeedUpEvent;
        public static event Action OnDamageUpEvent;
        public static event Action OnFireRateUpEvent;
        public static event Action OnHpUpEvent;
        public static event Action OnEnergyUpEvent;

        private void Start()
        {
            
        }
    }
}