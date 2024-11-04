using UnityEngine;

namespace UfoComponents
{

    public class SphereInputController : MonoBehaviour
    {
        private StrangeSphere _sphere;
        private InputSystem _inputSystem;


        private void Awake()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Enable();
            _sphere = GetComponent<StrangeSphere>();
        }

        private void Update()
        {
            _sphere.IsShooting = Shooting();
            _sphere.SetYPos(RotationByClockWise() + RotationCounterClockWise());
        }

        private bool Shooting()
        {
            return _inputSystem.Gameplay.Fire.ReadValue<float>() > 0.1f;
        }

        private float RotationByClockWise()
        {
            return -_inputSystem.Gameplay.SphereRotateByClockWise.ReadValue<float>();
        }

        private float RotationCounterClockWise()
        {
            return _inputSystem.Gameplay.SphereRotateCounterClockWise.ReadValue<float>();
        }
    }
}