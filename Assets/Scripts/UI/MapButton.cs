using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Events;
using UnityEngine.Serialization;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int mapId;
        private EventService eventService;
        private Button mapButton;

        // ReSharper disable Unity.PerformanceAnalysis
        private void Awake()
        {
            mapButton = GetComponent<Button>();
        }

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            mapButton.onClick.AddListener(OnMapButtonClicked);
            ToggleMapButton(false);
        }

        public void ToggleMapButton(bool isActive) => mapButton.interactable = isActive;

        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked() =>  eventService.OnMapSelected.InvokeEvent(mapId);
    }
}