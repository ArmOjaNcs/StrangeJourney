using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
public class UfoCont : MonoBehaviour
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _forcePower;
    private CharacterController _ufo;
    private float _gravity = -5f;
    public float MoveDir { get; private set; }
    private bool _isForcing;
    

    private void Awake()
    {
        _ufo = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        MoveFD();

        if (_isForcing)
        {
            AddForce();
        }
        else
        {
            DoGravity();
        }
        
        
    }

    private void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.Space))
        {
            _isForcing = true;
            
        }
        else
        {
            _isForcing = false;
        }
    }

    private void Move()
    {
        MoveDir = Input.GetAxis("Horizontal");
    }

    private void DoGravity()
    {
        _ufo.Move(Vector3.up * _gravity * Time.fixedDeltaTime);
    } 

    private void MoveFD()
    {
        _ufo.Move(Vector3.right * MoveDir * Time.fixedDeltaTime * _flySpeed);
    }

    private void AddForce()
    { 
        _ufo.Move(Vector3.up * _forcePower * Time.fixedDeltaTime);
    }

   
}
