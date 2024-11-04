using System;
using UfoComponents;
using UnityEngine;

namespace EnemiesAndBosses
{


    public class Enemy : MonoBehaviour
    {
        public float Hitpoits { get; private set; }
        public float Experience { get; private set; }

        public static event Action<object> OnEnemyDiedEvent;

        private void Start()
        {
            Hitpoits = 5;
            Experience = 50;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                Hitpoits -= other.GetComponent<Bullet>().Damage;
                if (Hitpoits <= 0)
                {
                    OnEnemyDiedEvent?.Invoke(this);
                    gameObject.SetActive(false);
                }

            }
        }
    }
}