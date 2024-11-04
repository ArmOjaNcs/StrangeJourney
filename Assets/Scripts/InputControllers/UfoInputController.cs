using UnityEngine;

namespace UfoComponents
{
    public class UfoInputController : MonoBehaviour
    {
        private UfoCont _ufo;
        private InputSystem _inputSystem;
        
        
        private void Awake()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Enable();
            _ufo = GetComponent<UfoCont>();
        }

       

        private void Update()
        {
            ReadDirections();
            _ufo.IsForcing = Forcing();
            _ufo.IsAddGravity = AddGravity();
        }

        private void ReadDirections()
        {
            var moveDir = _inputSystem.Gameplay.UfoMovement.ReadValue<Vector2>();

            _ufo.Move(moveDir);
        }

        private bool Forcing()
        {
            return _inputSystem.Gameplay.UfoAddForce.ReadValue<float>() > 0.1f;
        }

        private bool AddGravity()
        {
            return _inputSystem.Gameplay.UfoAddGravity.ReadValue<float>() > 0.1f;
        }
    }
}