using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
public class UfoCont : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _forcePower;
    private CharacterController _ufo;
    private float _gravity = -9.81f;
    private float _velocity;
    private float _rotateDir;
    private bool _isForcing;
    

    private void Awake()
    {
        _ufo = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Rotate();

        if (_isForcing)
        {
            
            _velocity = -2;
        }
        
        
        DoGravity();
        
    }

    private void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.Space))
        {
            _isForcing = true;
            AddForce();
        }
    }

    private void Move()
    {
        _rotateDir = Input.GetAxis("Horizontal");
    }

    private void DoGravity()
    {
        _velocity += _gravity * Time.fixedDeltaTime;
        Vector3 localRotAngle = transform.localRotation.eulerAngles;
        _ufo.Move(localRotAngle * _velocity * Time.fixedDeltaTime);
    } 

    private void Rotate()
    {
        transform.Rotate(Vector3.right * _rotateDir * Time.fixedDeltaTime * _rotateSpeed);
    }

    private void AddForce()
    {
        _velocity = Mathf.Sqrt(_forcePower * -2 * _gravity);   
    }
}
