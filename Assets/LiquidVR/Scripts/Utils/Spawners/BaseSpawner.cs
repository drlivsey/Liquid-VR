using System;
using UnityEngine;

namespace Liquid.Utils.Spawners
{
    public abstract class BaseSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnOptions m_spawnOptions = new SpawnOptions();
        
        public Transform SpawnPoint
        {
            get => m_spawnOptions.SpawnPoint;
            set => m_spawnOptions.SpawnPoint = value;
        }

        public SpawnOptions Options
        {
            get => m_spawnOptions;
            set => m_spawnOptions = value;
        }

        public bool SetSpawnPointAsParent
        {
            get => m_spawnOptions.SetSpawnPointAsParent;
            set => m_spawnOptions.SetSpawnPointAsParent = value;
        }

        public bool ResetPosition
        {
            get => m_spawnOptions.ResetPosition;
            set => m_spawnOptions.ResetPosition = value;
        }

        public bool ResetRotation
        {
            get => m_spawnOptions.ResetRotation;
            set => m_spawnOptions.ResetRotation = value;
        }

        public abstract void SpawnObject();
        
        [Serializable]
        public struct SpawnOptions
        {
            public Transform SpawnPoint;
            public bool ResetPosition;
            public bool ResetRotation;
            public bool SetSpawnPointAsParent;
        }
    }
}