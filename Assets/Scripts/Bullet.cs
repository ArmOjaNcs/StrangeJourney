using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _vector;
    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    private void OnDisable()
    {
        StopCoroutine(LifeTime());
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_vector * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("wall"))
            Destroy(gameObject);
    }
}
