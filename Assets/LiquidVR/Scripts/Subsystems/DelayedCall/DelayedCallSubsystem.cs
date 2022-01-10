using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Subsystems.DelayedCall
{
    public class DelayedCallSubsystem : BaseSubsystem
    {
        private List<Coroutine> _delayedCalls = null;
        private static DelayedCallSubsystem _instance = null;
        
        public bool IsPerformed
        {
            get; private set;
        }

        public bool IsInitialized
        {
            get; private set;
        }

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

        public override void Initialize()
        {
            if (IsInitialized) return;
            
            _delayedCalls = new List<Coroutine>();
            IsInitialized = true;
        }

        public override void StartExecution()
        {
            IsPerformed = true;
        }

        public override void StopExecution()
        {
            foreach (var call in _delayedCalls)
            {
                StopCoroutine(call);
            }
            IsPerformed = false;
        }

        public override void Dispose()
        {
            StopExecution();
            
            _delayedCalls.Clear();
            _delayedCalls = null;

            IsInitialized = false;
            IsPerformed = false;
        }

        public void RegisterAction(Action action, float delay, bool isRealtime = false)
        {
            if (IsInitialized == false) return;
            
            _delayedCalls.Add(StartCoroutine(CallDelayed(action, delay, isRealtime)));
        }

        public static DelayedCallSubsystem SubsystemInstance()
        {
            if (_instance != null) return _instance;
            
            var subsystemGameObject = new GameObject("[DelayedCallSubsystem]");
            _instance = subsystemGameObject.AddComponent<DelayedCallSubsystem>();
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
                this.gameObject.name = "[DelayedCallSubsystem]";
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private IEnumerator CallDelayed(Action action, float delay, bool isRealtime)
        {
            if (isRealtime) yield return new WaitForSecondsRealtime(delay);
            else yield return new WaitForSeconds(delay);
            
            action?.Invoke();
        }
    }
}