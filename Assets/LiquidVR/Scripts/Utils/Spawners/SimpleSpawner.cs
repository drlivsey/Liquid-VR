using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils.Spawners
{
    public class SimpleSpawner : BaseSpawner
    {
        [SerializeField] private GameObject m_prefab = null;

        public GameObject Prefab
        {
            get => m_prefab;
            set => m_prefab = value;
        }

        public override void SpawnObject()
        {
            var instance = Instantiate(m_prefab);

            if (SetSpawnPointAsParent) instance.transform.SetParent(SpawnPoint, true);
            
            if (ResetPosition) instance.transform.position = SpawnPoint.position;
            
            if (ResetRotation) instance.transform.localRotation = Quaternion.identity;
        }
    }
}
