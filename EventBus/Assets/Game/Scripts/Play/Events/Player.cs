using UnityEngine;

namespace Game.Scripts.Play.Events
{
    public class Player : MonoBehaviour
    {
         private EventBinding<PlayerEvent> _playerEvent;
        private void OnEnable()
        {
            _playerEvent = new EventBinding<PlayerEvent>(HandlePlayerEvent);
            EventBus<PlayerEvent>.Register(_playerEvent);
        }

        private void OnDisable()
        {
            EventBus<PlayerEvent>.DeRegister(_playerEvent);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                EventBus<PlayerEvent>.Raise(new PlayerEvent()
                {
                    health =  5
                });
            }
        }

        private void HandlePlayerEvent(PlayerEvent events)
        {
            Debug.LogError("Player Health : " + events.health);
        }
    }
}