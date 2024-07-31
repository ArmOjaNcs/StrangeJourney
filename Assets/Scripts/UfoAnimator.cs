using UnityEngine;


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
            _animator.SetBool("TurnRight", true);
        else
            _animator.SetBool("TurnRight", false);

        if(_ufoCont.MoveDir < -0.2f)
            _animator.SetBool("TurnLeft", true);
        else
            _animator.SetBool("TurnLeft", false);
    }
}
