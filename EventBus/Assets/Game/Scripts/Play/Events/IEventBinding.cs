using System;

namespace Game.Scripts.Play.Events
{
    internal interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArguments { get; set; }
    }
}