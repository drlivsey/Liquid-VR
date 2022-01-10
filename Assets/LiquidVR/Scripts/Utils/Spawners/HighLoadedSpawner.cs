using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;
using Liquid.Subsystems.Updating;

namespace Liquid.Utils.Spawners
{
    public class HighLoadedSpawner : SimpleSpawner
    {
        [SerializeField, Min(0)] private int m_maxObjectsPerFrame = 5;

        public int MaxObjectsPerFrame
        {
            get => m_maxObjectsPerFrame;
            set => m_maxObjectsPerFrame = value;
        }

        private int _currentSpawnRequestSize = 0;

        public void Update()
        {
            if (_currentSpawnRequestSize == 0) return;

            for (var i = 0; i < m_maxObjectsPerFrame; i++)
            {
                if (_currentSpawnRequestSize == 0) return;
                
                SpawnObject();
                _currentSpawnRequestSize--;
            }
        }

        public void SpawnObjects(int count)
        {
            _currentSpawnRequestSize += count;
        }
    }
}