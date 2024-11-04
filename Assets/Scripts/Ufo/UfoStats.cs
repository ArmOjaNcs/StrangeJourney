using System;
using UnityEngine;

namespace UfoComponents
{
    public class UfoStats : MonoBehaviour
    {
        public int Hp { get; private set; }
        public int Energy { get; private set; }
        public float FlySpeed { get; private set; }
        public float Damage { get; private set; }
        public float FireRate { get; private set; }
        public float CameraSmoothing { get; private set; }
        public float SphereSmoothing { get; private set; }
        public float BulletSpeed { get; private set; }
        public float Experience { get; private set; }
        public float TotalExp { get; private set; }

        public static UfoStats Instance { get; private set; }

        public static event Action StatsChangedEvent;

        private const float _maxDamage = 26;
        private const float _maxFlySpeed = 24;
        private const float _minFireRate = 0.075f;
        private const int _maxEnergy = 70;
        private const int _maxHP = 70;
        private const float _maxSphereSmoothing = 12;
        private const float _maxCameraSmoothing = 6;
        private const float _maxBulletSpeed = 56;


        private const string UfoHP = nameof(UfoHP);
        private const string UfoEnergy = nameof(UfoEnergy);
        private const string UfoFlySpeed = nameof(UfoFlySpeed);
        private const string UfoDamage = nameof(UfoDamage);
        private const string UfoFireRate = nameof(UfoFireRate);
        private const string UfoSphereSmoothing = nameof(UfoSphereSmoothing);
        private const string UfoCameraSmoothing = nameof(UfoCameraSmoothing);
        private const string UfoBulletSpeed = nameof(UfoBulletSpeed);
        private const string CurrentExperience = nameof(CurrentExperience);
        private const string TotalExperience = nameof(TotalExperience);

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                SetFirstEntryDefaultStats();
                GetStats();
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                AddAllStats();
            if (Input.GetKeyDown(KeyCode.X))
                SetDefaultSatats();
        }

        private void SetFirstEntryDefaultStats()
        {
            if (!PlayerPrefs.HasKey(CurrentExperience))
                PlayerPrefs.SetFloat(CurrentExperience, 0);
            if (!PlayerPrefs.HasKey(TotalExperience))
                PlayerPrefs.SetFloat(TotalExperience, 0);
            if (!PlayerPrefs.HasKey(UfoHP))
                PlayerPrefs.SetInt(UfoHP, 10);
            if (!PlayerPrefs.HasKey(UfoEnergy))
                PlayerPrefs.SetInt(UfoEnergy, 10);
            if (!PlayerPrefs.HasKey(UfoFlySpeed))
                PlayerPrefs.SetFloat(UfoFlySpeed, 12);
            if (!PlayerPrefs.HasKey(UfoDamage))
                PlayerPrefs.SetFloat(UfoDamage, 2);
            if (!PlayerPrefs.HasKey(UfoFireRate))
                PlayerPrefs.SetFloat(UfoFireRate, 1.2f);
            if (!PlayerPrefs.HasKey(UfoSphereSmoothing))
                PlayerPrefs.SetFloat(UfoSphereSmoothing, 5);
            if (!PlayerPrefs.HasKey(UfoCameraSmoothing))
                PlayerPrefs.SetFloat(UfoCameraSmoothing, 2);
            if (!PlayerPrefs.HasKey(UfoBulletSpeed))
                PlayerPrefs.SetFloat(UfoBulletSpeed, 20);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(UfoHP, Hp);
            PlayerPrefs.SetInt(UfoEnergy, Energy);
            PlayerPrefs.SetFloat(UfoDamage, Damage);
            PlayerPrefs.SetFloat(UfoFlySpeed, FlySpeed);
            PlayerPrefs.SetFloat(UfoFireRate, FireRate);
            PlayerPrefs.SetFloat(UfoCameraSmoothing, CameraSmoothing);
            PlayerPrefs.SetFloat(UfoSphereSmoothing, SphereSmoothing);
            PlayerPrefs.SetFloat(UfoBulletSpeed, BulletSpeed);
            PlayerPrefs.SetFloat(CurrentExperience, Experience);
            PlayerPrefs.SetFloat(TotalExperience, TotalExp);
        }

        private void GetStats()
        {
            Hp = PlayerPrefs.GetInt(UfoHP);
            Energy = PlayerPrefs.GetInt(UfoEnergy);
            Damage = PlayerPrefs.GetFloat(UfoDamage);
            FlySpeed = PlayerPrefs.GetFloat(UfoFlySpeed);
            FireRate = PlayerPrefs.GetFloat(UfoFireRate);
            CameraSmoothing = PlayerPrefs.GetFloat(UfoCameraSmoothing);
            SphereSmoothing = PlayerPrefs.GetFloat(UfoSphereSmoothing);
            BulletSpeed = PlayerPrefs.GetFloat(UfoBulletSpeed);
            Experience = PlayerPrefs.GetFloat(CurrentExperience);
            TotalExp = PlayerPrefs.GetFloat(TotalExperience);
        }

        private void SetDefaultSatats()
        {
            PlayerPrefs.SetInt(UfoHP, 10);
            PlayerPrefs.SetInt(UfoEnergy, 10);
            PlayerPrefs.SetFloat(UfoDamage, 2);
            PlayerPrefs.SetFloat(UfoFlySpeed, 12);
            PlayerPrefs.SetFloat(UfoFireRate, 1.2f);
            PlayerPrefs.SetFloat(UfoCameraSmoothing, 2);
            PlayerPrefs.SetFloat(UfoSphereSmoothing, 5);
            PlayerPrefs.SetFloat(UfoBulletSpeed, 20);
            PlayerPrefs.SetFloat(CurrentExperience, 0);
            PlayerPrefs.SetFloat(TotalExperience, 0);
            GetStats();
            StatsChangedEvent?.Invoke();
        }

        private void AddAllStats()
        {
            int tempHP = PlayerPrefs.GetInt(UfoHP);
            PlayerPrefs.SetInt(UfoHP, CheckMinInt(tempHP + 5, _maxHP));
            int tempEnergy = PlayerPrefs.GetInt(UfoEnergy);
            PlayerPrefs.SetInt(UfoEnergy, CheckMinInt(tempEnergy + 5, _maxEnergy));
            float tempDamage = PlayerPrefs.GetFloat(UfoDamage);
            PlayerPrefs.SetFloat(UfoDamage, CheckMinFloat(tempDamage + 2, _maxDamage));
            float tempFlySpeed = PlayerPrefs.GetFloat(UfoFlySpeed);
            PlayerPrefs.SetFloat(UfoFlySpeed, CheckMinFloat(tempFlySpeed + 0.5f, _maxFlySpeed));
            float tempFireRate = PlayerPrefs.GetFloat(UfoFireRate);
            PlayerPrefs.SetFloat(UfoFireRate, CheckMaxFloat(tempFireRate - 0.1f, _minFireRate));
            float tempSphereSmooth = PlayerPrefs.GetFloat(UfoSphereSmoothing);
            PlayerPrefs.SetFloat(UfoSphereSmoothing, CheckMinFloat(tempSphereSmooth + 0.6f, _maxSphereSmoothing));
            float tempCameraSmoothing = PlayerPrefs.GetFloat(UfoCameraSmoothing);
            PlayerPrefs.SetFloat(UfoCameraSmoothing, CheckMinFloat(tempCameraSmoothing + 0.25f, _maxCameraSmoothing));
            float tempBulletSpeed = PlayerPrefs.GetFloat(UfoBulletSpeed);
            PlayerPrefs.SetFloat(UfoBulletSpeed, CheckMinFloat(tempBulletSpeed + 3, _maxBulletSpeed));
            GetStats();
            StatsChangedEvent?.Invoke();
        }

        private float CheckMinFloat(float value, float maxValue)
        {
            return value = Mathf.Min(value, maxValue);
        }

        private int CheckMinInt(int value, int maxValue)
        {
            return value = Mathf.Min(value, maxValue);
        }

        private float CheckMaxFloat(float value, float minValue)
        {
            return value = Mathf.Max(value, minValue);
        }
    }
}