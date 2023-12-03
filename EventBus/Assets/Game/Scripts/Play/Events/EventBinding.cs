using System;
using UnityEngine;

namespace Game.Scripts.Play.Events
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        Action<T> onEvent = _ => { };
        Action onEventNoArguments = () => { };


        public Action<T> OnEvent
        {
            get => onEvent;
            set => onEvent = value;
        }

        public Action OnEventNoArguments
        {
            get => onEventNoArguments;
            set => onEventNoArguments = value;
        }

        public EventBinding(Action<T> action) => onEvent = action;
        public EventBinding(Action action) => onEventNoArguments = action;

        public void Add(Action<T> action) => onEvent += action;
        public void Add(Action action) => onEventNoArguments += action;
        
        public void Remove(Action<T> action) => onEvent -= action;
        public void Remove(Action action) => onEventNoArguments -= action;

    }
}


