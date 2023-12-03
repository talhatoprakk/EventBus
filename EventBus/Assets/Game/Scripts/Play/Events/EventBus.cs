using System.Collections.Generic;

namespace Game.Scripts.Play.Events
{
    public static class EventBus<T> where T: IEvent
    {
         static readonly HashSet<IEventBinding<T>> _bindings = new ();


         public static void Register(EventBinding<T> action) => _bindings.Add(action);
         public static void DeRegister(EventBinding<T> action) => _bindings.Remove(action);

         public static void Raise(T @event)
         {
             foreach (var eventBinding in _bindings)
             {
                 eventBinding.OnEvent.Invoke(@event);
                 eventBinding.OnEventNoArguments.Invoke();
             }
         }
    }
}