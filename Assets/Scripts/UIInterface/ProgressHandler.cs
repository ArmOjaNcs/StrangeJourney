using EnemiesAndBosses;
using System;
using UfoComponents;
using UnityEngine;

namespace UIElements
{

    public class ProgressHandler : MonoBehaviour
    {
        [SerializeField] private ProgressBar _experienceBar;
        private float _experienceToNextLevel = 100;
        private float _currentExperience;

        public static event Action OnLevelUpEvent;


        private void Start()
        {
            _currentExperience = UfoStats.Instance.Experience;
        }

        private void OnEnable()
        {
            Enemy.OnEnemyDiedEvent += EnemyDied;
        }

        private void OnDisable() 
        {
            Enemy.OnEnemyDiedEvent -= EnemyDied;
        }
        private void EnemyDied(object enemy)
        {
            var Enemy = (Enemy)enemy;
            _currentExperience += Enemy.Experience;
            if(_currentExperience >= _experienceToNextLevel)
            {
                _currentExperience -= _experienceToNextLevel;
                _experienceToNextLevel *= 1.1f;
                OnLevelUpEvent?.Invoke();
            }
        }

        private void SetZeroExperience()
        {
            _currentExperience = 0;
        }
    }
}