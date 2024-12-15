using System;
using System.Collections.Generic;

namespace Core
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, List<Delegate>> EventDictionary = new();

        public static void Subscribe<T>(Action<T> listener)
        {
            var eventType = typeof(T);

            if (!EventDictionary.ContainsKey(eventType))
            {
                EventDictionary[eventType] = new List<Delegate>();
            }

            EventDictionary[eventType].Add(listener);
        }
        
        public static void Unsubscribe<T>(Action<T> listener)
        {
            var eventType = typeof(T);

            if (!EventDictionary.TryGetValue(eventType, out var value)) return;
            value.Remove(listener);
            
            if (EventDictionary[eventType].Count == 0)
            {
                EventDictionary.Remove(eventType);
            }
        }

        public static void TriggerEvent<T>(T eventData)
        {
            var eventType = typeof(T);

            if (!EventDictionary.TryGetValue(eventType, out var value)) return;
            
            foreach (var listener in value)
            {
                (listener as Action<T>)?.Invoke(eventData);
            }
        }
    }
}