using System;
using System.Collections;
using System.Collections.Generic;
using Liquid.Core;
using UnityEngine;

namespace Liquid.Subsystems.Updating
{
    public class UpdatingSubsystem : BaseSubsystem
    {
        private List<BaseUpdatableComponent> _updateList = null;
        private List<BaseUpdatableComponent> _fixedUpdateList = null;
        private List<BaseUpdatableComponent> _lateUpdateList = null;
        private Dictionary<BaseUpdatableComponent, Coroutine> _updateByTimeList = null;

        public bool IsPerformed
        {
            get; private set;
        }

        public bool IsInitialized
        {
            get; private set;
        }
        
        private static UpdatingSubsystem _instance = null;

        private void Reset()
        {
            SetupInstance();
        }

        private void OnEnable()
        {
            StartExecution();
        }

        private void OnDisable()
        {
            StopExecution();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Update()
        {
            UpdateComponents(_updateList);
        }

        private void FixedUpdate()
        {
            UpdateComponents(_fixedUpdateList);
        }

        private void LateUpdate()
        {
            UpdateComponents(_lateUpdateList);
        }

        public override void Initialize()
        {
            if (IsInitialized) return;
            
            _updateList = new List<BaseUpdatableComponent>();
            _fixedUpdateList = new List<BaseUpdatableComponent>();
            _lateUpdateList = new List<BaseUpdatableComponent>();
            _updateByTimeList = new Dictionary<BaseUpdatableComponent, Coroutine>();
            IsInitialized = true;
        }

        public override void StartExecution()
        {
            IsPerformed = true;
        }

        public override void StopExecution()
        {
            
            IsPerformed = false;
        }

        public override void Dispose()
        {
            StopExecution();
            
            _updateList.Clear();
            _updateList = null;
            
            _fixedUpdateList.Clear();
            _fixedUpdateList = null;
            
            _lateUpdateList.Clear();
            _lateUpdateList = null;
            
            _updateByTimeList.Clear();
            _updateByTimeList = null;

            IsInitialized = false;
            IsPerformed = false;
        }

        public void RegisterComponent(BaseUpdatableComponent component)
        {
            if (IsInitialized == false) return;
            switch (component.UpdatingState)
            {
                case UpdatingState.Update:
                    RegisterInCollection(_updateList, component);
                    break;
                case UpdatingState.FixedUpdate:
                    RegisterInCollection(_fixedUpdateList, component);
                    break;
                case UpdatingState.LateUpdate:
                    RegisterInCollection(_lateUpdateList, component);
                    break;
                case UpdatingState.UpdateByTime:
                    RegisterInDictionary(_updateByTimeList, component);
                    break;
            }
        }

        public void UnregisterComponent(BaseUpdatableComponent component)
        {
            if (IsInitialized == false) return;
            switch (component.UpdatingState)
            {
                case UpdatingState.Update:
                    UnregisterInCollection(_updateList, component);
                    break;
                case UpdatingState.FixedUpdate:
                    UnregisterInCollection(_fixedUpdateList, component);
                    break;
                case UpdatingState.LateUpdate:
                    UnregisterInCollection(_lateUpdateList, component);
                    break;
                case UpdatingState.UpdateByTime:
                    UnregisterInDictionary(_updateByTimeList, component);
                    break;
            }
        }

        public static UpdatingSubsystem SubsystemInstance()
        {
            if (_instance != null) return _instance;
            
            var subsystemGameObject = new GameObject("[UpdatingSubsystem]");
            _instance = subsystemGameObject.AddComponent<UpdatingSubsystem>();
            _instance.Initialize();
            return _instance;
        }

        private void SetupInstance()
        {
            if (_instance == this) return;
            if (_instance == null)
            {
                _instance = this;
                _instance.Initialize();
                this.gameObject.name = "[UpdatingSubsystem]";
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void UpdateComponents(ICollection<BaseUpdatableComponent> components)
        {
            if (IsInitialized == false) return;
            if (IsPerformed == false) return;
            foreach (var component in components)
            {
                component.UpdateComponent();
            }
        }

        private void RegisterInCollection(ICollection<BaseUpdatableComponent> collection,
            BaseUpdatableComponent component)
        {
            if (collection.Contains(component)) return;
            collection.Add(component);
        }
        
        private void UnregisterInCollection(ICollection<BaseUpdatableComponent> collection,
            BaseUpdatableComponent component)
        {
            if (collection.Contains(component) == false) return;
            collection.Remove(component);
        }

        private void RegisterInDictionary(IDictionary<BaseUpdatableComponent, Coroutine> dictionary,
            BaseUpdatableComponent component)
        {
            if (dictionary.ContainsKey(component)) return;
            dictionary.Add(component, StartCoroutine(UpdateByTime(component, component.UpdateFrequency, component.UpdateInRealtime)));
        }
        
        private void UnregisterInDictionary(IDictionary<BaseUpdatableComponent, Coroutine> dictionary,
            BaseUpdatableComponent component)
        {
            if (dictionary.ContainsKey(component) == false) return;
            StopCoroutine(dictionary[component]);
            dictionary.Remove(component);
        }

        private IEnumerator UpdateByTime(BaseUpdatableComponent component, float frequency, bool isRealtime)
        {
            while (_instance.gameObject.activeInHierarchy)
            {
                if (isRealtime) yield return new WaitForSecondsRealtime(frequency);
                else yield return new WaitForSeconds(frequency);
                
                component.UpdateComponent();
            }
        }
    }
}