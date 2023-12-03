using System;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Play.Events
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }


#if UNITY_EDITOR

        public static PlayModeStateChange playModeState { get; set; }

        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            playModeState = state;
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                ClearAllBuses();
            }
        }
       
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventTypes = InitializeAllBuses();
        }

        private static IReadOnlyList<Type> InitializeAllBuses()
        {
            var eventBusTypes = new List<Type>();

            var typeDef = typeof(EventBus<>);
            foreach (var eventType in EventTypes)
            {
                var busType = typeDef.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
                Debug.LogError($"Initialized Event Bus + EventBus<{eventType.Name}> ");
            }

            return eventBusTypes;
        }

        public static void ClearAllBuses()
        {
            Debug.LogError("Clear All Buses ");
            for (var i = 0; i < EventBusTypes.Count; i++)
            {
                var busType = EventBusTypes[i];
                var clearMethod = busType.GetMethod("Clear", BindingFlags.NonPublic | BindingFlags.Static);
                clearMethod.Invoke(null, null);

            }
        }
    }
}