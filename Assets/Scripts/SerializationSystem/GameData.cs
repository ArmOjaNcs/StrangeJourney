using System;
using UnityEngine;

namespace SerializationSystem
{
    [Serializable]
    public class GameData
    {
        public float FireRate { get; private set; }
        public float FlySpeed { get; private set; }
        public float Damage { get; private set; }
        public int Hitpoints { get; private set; }
        public int Energy { get; private set; }
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public GameData()
        {
            FireRate = 1.2f;
            FlySpeed = 7;
            Damage = 2;
            Hitpoints = 10;
            Energy = 10;
            Position = Vector3.zero;
            Rotation = Quaternion.identity;

        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public void SetFireRate(float fireRate)
        {
            FireRate = fireRate;
        }

        public void SetFlySpeed(float flySpeed)
        {
            FlySpeed = flySpeed;
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        public void SetHitpoints(int hitPoints)
        {
            Hitpoints = hitPoints;
        }

        public void SetEnergy(int energy)
        {
            Energy = energy;
        }

        public void DeleteStatsFiles()
        {
            FireRate = 1.2f;
            FlySpeed = 7;
            Damage = 2;
            Hitpoints = 10;
            Energy = 10;
           
        }

        public void DeleteCheckPointsFiles()
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }
    }
}
