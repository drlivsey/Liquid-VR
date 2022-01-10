using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Utils.Spawners
{
    public class PeriodicalSpawner : SimpleSpawner
    {
        [SerializeField, Min(0f)] private float m_period = 1f;
        [SerializeField] private bool m_isRealtime = false;
        [SerializeField] private bool m_startOnEnable = false;

        public float Period
        {
            get => m_period;
            set => m_period = value;
        }

        public bool IsRealtime
        {
            get => m_isRealtime;
            set => m_isRealtime = value;
        }

        public bool StartOnEnable
        {
            get => m_startOnEnable;
            set => m_startOnEnable = value;
        }

        private Coroutine _spawnCoroutine = null;

        private void OnEnable()
        {
            if (m_startOnEnable) StartSpawn();
        }

        private void OnDisable()
        {
            StopSpawn();
        }

        public void StartSpawn()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        public void StopSpawn()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            while (this.gameObject.activeInHierarchy)
            {
                SpawnObject();
                if (m_isRealtime) yield return new WaitForSecondsRealtime(m_period);
                else yield return new WaitForSeconds(m_period);
            }
        }
    }
}