using UnityEngine;

namespace UfoComponents
{

    public class LeftSideSensor : MonoBehaviour
    {
        [SerializeField] private UfoCont _ufo;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wall"))
                _ufo.MoveRight();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("wall"))
                _ufo.MoveRight();
        }
    }
}