using UnityEngine;

namespace UfoComponents
{

    [RequireComponent(typeof(Animator))]
    public class UfoAnimator : MonoBehaviour
    {
        private UfoCont _ufoCont;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _ufoCont = GetComponent<UfoCont>();
        }

        private void Update()
        {
            if (_ufoCont.MoveDir > 0.2f)
                _animator.SetBool("FlyRight", true);
            else
                 _animator.SetBool("FlyRight", false);
      

            if (_ufoCont.MoveDir < -0.2f)
                _animator.SetBool("FlyLeft", true);
            else
                _animator.SetBool("FlyLeft", false);
           
        }

    }
}