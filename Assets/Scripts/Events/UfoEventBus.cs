using System;
using UnityEngine;

namespace Events
{
    public class UfoEventsBus : MonoBehaviour
    {
        public static event Action<object, float> FlySpeedChangedEvent;
        public static event Action<object, float> RateSpeedChangedEvent;
        public static event Action<object, float> DamageChangedEvent;
        public static event Action<object, int> HPChangedEvent;
        public static event Action<object, int> EnergyChangedEvent;
        public static UfoEventsBus Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
                FlySpeedChangedEvent?.Invoke(this, 0.5f);
            if (Input.GetKey(KeyCode.X))
                RateSpeedChangedEvent?.Invoke(this, 0.05f); 
            if (Input.GetKey(KeyCode.C))
                DamageChangedEvent?.Invoke(this, 2);
            if (Input.GetKey(KeyCode.V))
                HPChangedEvent?.Invoke(this, 5);
            if (Input.GetKey(KeyCode.B))
                EnergyChangedEvent?.Invoke(this, 5);
        }
    }

}
