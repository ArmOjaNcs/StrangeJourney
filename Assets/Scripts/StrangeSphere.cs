using UnityEngine;

public class StrangeSphere : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _rateTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _focusSpeed;
    [SerializeField] private CameraFollower _sphere;
    private float _timeToShoot;
    private float _yPosition;
    private float _angle;
    private bool _isFocusing;

    private void Update()
    {
        _yPosition = Input.GetAxis("Vertical");
        _angle = transform.rotation.eulerAngles.z;

        SetOffset(_angle);

        _timeToShoot += Time.deltaTime;

        FocusSwitcher();

        if (Input.GetMouseButton(0) && _timeToShoot > _rateTime)
        {
            Fire();
            _timeToShoot = 0;
        }

    }

    private void FocusSwitcher()
    {
        if (Input.GetMouseButton(0))
            _isFocusing = true;
        else
            _isFocusing = false;
    }

    private void FixedUpdate()
    {
        
        Rotate();
        
        if (_isFocusing)
        {
            GetFocus();
        }
    }


    private void Fire()
    {
        var bullet = Instantiate(_bullet, transform.position, transform.rotation);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, _yPosition * _rotationSpeed * Time.fixedDeltaTime));
    }

    private void GetFocus()
    {
        transform.Translate(Vector3.right * _focusSpeed * Time.fixedDeltaTime);
    }

    private void SetOffset(float angle)
    {
        
        if( (angle < 45 && angle>0) || (angle < 360 && angle > 315))
        {
            _sphere.SetOffset(new Vector3(5, 0, 0));
            return;
        }
        if(angle < 315 && angle > 225)
        {
            _sphere.SetOffset(new Vector3(0, -5, 0));
            return;
        }
        if(angle < 225 && angle > 135)
        {
            _sphere.SetOffset(new Vector3(-5, 0, 0));
            return;
        }
        if(angle < 135 && angle > 45)
        {
            _sphere.SetOffset(new Vector3(0, 5, 0));
            return;
        }
        
    }
}
