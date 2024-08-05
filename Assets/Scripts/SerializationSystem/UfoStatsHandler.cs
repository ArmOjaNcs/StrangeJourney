using UfoComponents;
using UnityEngine;

namespace SerializationSystem
{
    public class UfoStatsHandler : MonoBehaviour
    {
        [SerializeField] private UfoCont _ufo; 
        [SerializeField] private StrangeSphere _sphere; 
        [SerializeField] private UfoHPandEnergy _hpAndEnergy; 
        
        private Storage _storage;

        public GameData GameData;
        public static UfoStatsHandler Instance { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            _storage = new Storage();
            Load();
            _ufo.FlySpeedChangedEvent += OnFlySpeedChanged;
            _sphere.RateTimeChangedEvent += OnRateTimeChanged;
            _sphere.DamageChangedEvent += OnDamageChanged;
            _hpAndEnergy.EnergyChangedEvent += OnEnergyChanged;
            _hpAndEnergy.HPChangedEvent += OnHPChanged;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.M))
                DeleteSavedFiles();
        }

        private void OnHPChanged(object sender, int hp)
        {
            GameData.SetHitpoints(hp);
            Save();
        }

        private void OnEnergyChanged(object sender, int energy)
        {
            GameData.SetEnergy(energy);
            Save();
        }

        private void OnDamageChanged(object sender, float damage)
        {
            GameData.SetDamage(damage);
            Save();
        }

        private void OnRateTimeChanged(object sender, float fireRate)
        {
            GameData.SetFireRate(fireRate);
            Save();
        }

        private void OnFlySpeedChanged(object sender, float flySpeed)
        {
            GameData.SetFlySpeed(flySpeed);
            Save();
        }


        private void Save()
        {
            _storage.Save(GameData); 
        }

        private void Load()
        {
            GameData = (GameData)_storage.Load(new GameData());  
        }

        private void DeleteSavedFiles()
        {
            GameData.DeleteStatsFiles();
            Save();
        }

        private void OnDisable()
        {
            _ufo.FlySpeedChangedEvent -= OnFlySpeedChanged;
            _sphere.RateTimeChangedEvent -= OnRateTimeChanged;
            _sphere.DamageChangedEvent -= OnDamageChanged;
            _hpAndEnergy.HPChangedEvent -= OnHPChanged;
            _hpAndEnergy.EnergyChangedEvent -= OnEnergyChanged;
        }
    }

}
