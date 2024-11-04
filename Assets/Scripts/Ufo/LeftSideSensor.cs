using System;
using UnityEngine;

namespace UfoComponents
{

    public class LeftSideSensor : MonoBehaviour
    {
        
        public static event Action OnColliderOtherLeftSideEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wall"))
                OnColliderOtherLeftSideEvent?.Invoke();     
        }

    }
}