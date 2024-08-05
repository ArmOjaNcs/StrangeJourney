using SerializationSystem;
using System.Collections;
using UnityEngine;

namespace UfoComponents
{

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _vector;
        private readonly float _maxDamage = 50;
        public float Damage { get; private set; }
        public bool IsAddDamge { get; private set; }
        private IEnumerator LifeTime()
        {
            Damage = UfoStatsHandler.Instance.GameData.Damage;
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
            if (other.CompareTag("wall") || other.CompareTag("ground"))
                Destroy(gameObject);
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        public void AddDamage(float damage)
        {
            if (Damage < _maxDamage)
            {
                float oldDamageValue = Damage;
                Damage += damage;
                Damage = Mathf.Min(Damage, _maxDamage);
                if (oldDamageValue < Damage)
                    IsAddDamge = true;
                else 
                    IsAddDamge = false;
                    
            }
            else
            {
                Debug.Log($"Bullet has a max Damage {Damage}");
            }
        }
    }
}