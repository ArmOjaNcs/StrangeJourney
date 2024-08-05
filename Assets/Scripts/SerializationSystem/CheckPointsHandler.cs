using UfoComponents;
using UnityEngine;

namespace SerializationSystem
{
    public class CheckPointsHandler : MonoBehaviour
    {
        [SerializeField] private UfoCont _ufo;
        [SerializeField] private CameraFollower _sphere;
        [SerializeField] private CameraFollower _camera;

        private Storage _storage;

        public GameData GameData;

        public static CheckPointsHandler Instance;

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

        private void Start()
        {
            _storage = new Storage();
            Load();
        }

        private void Update()
        {
            
        }

        private void Save()
        {
            GameData.SetPosition(_ufo.transform.position);
            GameData.SetRotation(_ufo.transform.rotation);
           
            _storage.Save(GameData);
        }

        private void Load()
        {
            GameData = (GameData)_storage.Load(new GameData());

            _ufo.transform.position = GameData.Position;
            _ufo.transform.rotation = GameData.Rotation;
            _camera.transform.position = GameData.Position + _camera.GetOffset();
            _camera.transform.rotation = GameData.Rotation;
            _sphere.transform.position = GameData.Position + _sphere.GetOffset();
            _sphere.transform.rotation = GameData.Rotation;

        }

        private void DeleteSavedFiles()
        {
            GameData.DeleteCheckPointsFiles();
        }
    }
}
