using System;
using UnityEngine;

namespace UfoComponents
{
    public class RightSideSensor : MonoBehaviour
    {

        public static event Action OnColliderOtherRightSideEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wall"))
            {
                OnColliderOtherRightSideEvent?.Invoke();
                Debug.Log("It's Invoke from collider ");
            }
        }

    }
}