using UnityEngine;

namespace UfoComponents
{
    public class RightSideSensor : MonoBehaviour
    {
        [SerializeField] private UfoCont _ufo;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wall"))
                _ufo.MoveLeft();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("wall"))
                _ufo.MoveLeft();
        }
    }
}