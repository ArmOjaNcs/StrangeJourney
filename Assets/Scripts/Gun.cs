using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _rateTime;
    [SerializeField] private float _rotationSpeed;
    private float _timeToShoot;
    private float _yPosition;
    private float _angle;

    private void Update()
    {
        _yPosition = Input.GetAxis("Vertical");
        _angle = transform.rotation.eulerAngles.z;

        _timeToShoot += Time.deltaTime;

        if (Input.GetMouseButton(0) && _timeToShoot > _rateTime)
        {
            CreateBullet();
            _timeToShoot = 0;
        }
            
    }

    private void FixedUpdate()
    {
        if (_yPosition < 0 && _angle > 10)
            Rotate();
        if (_yPosition > 0 && _angle < 170)
            Rotate();
    }


    private void CreateBullet()
    {
        var bullet = Instantiate(_bullet, transform.position, transform.rotation);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, _yPosition * _rotationSpeed * Time.fixedDeltaTime));
    }

}
