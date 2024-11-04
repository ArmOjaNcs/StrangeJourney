using System.Collections;
using UnityEngine;

namespace UfoComponents
{

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _vector;
        public float Damage { get; private set; }

        private void Start()
        {
            StartCoroutine(LifeTime());
        }

        private void FixedUpdate()
        {
            Move();
        }

        private IEnumerator LifeTime()
        {
            var wait = new WaitForSeconds(_lifeTime);
            yield return wait;
            Destroy(gameObject);
        }

        private void Move()
        {
            transform.Translate(_vector * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("wall") || other.CompareTag("ground"))
                Destroy(gameObject);
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

    }
}