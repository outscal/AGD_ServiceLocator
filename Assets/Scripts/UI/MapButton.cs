using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Events;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int MapId;
        private EventService eventService;

        public void Init(EventService eventService)
        {
            Debug.Log("new map");
            this.eventService = eventService;
            GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);
            GetComponent<Button>().interactable = true;
        }
        
        public void DisableMapButton()
        {
            GetComponent<Button>().interactable = false;
        }

        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked() =>  eventService.OnMapSelected.InvokeEvent(MapId);
    }
}