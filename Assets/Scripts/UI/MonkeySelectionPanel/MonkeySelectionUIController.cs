using ServiceLocator.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Events;

namespace ServiceLocator.UI
{
    public class MonkeySelectionUIController
    {
        private Transform cellContainer;
        private List<MonkeyCellController> monkeyCellControllers;
        private EventService eventService;

        private void SubscribeToEvents() => eventService.OnPlayAgainEvent.AddListener(ResetMonkeyCellViews);

        public MonkeySelectionUIController(PlayerService playerService, Transform cellContainer, MonkeyCellView monkeyCellPrefab,LockedMonkeyCellView lockedMonkeyCellPrefab, List<MonkeyCellScriptableObject> monkeyCellScriptableObjects)
        {
            this.cellContainer = cellContainer;
            monkeyCellControllers = new List<MonkeyCellController>();

            foreach (MonkeyCellScriptableObject monkeySO in monkeyCellScriptableObjects)
            {
                MonkeyCellController monkeyCell = new MonkeyCellController(playerService, cellContainer, monkeyCellPrefab,lockedMonkeyCellPrefab, monkeySO);
                monkeyCellControllers.Add(monkeyCell);
            }
        }

        private void ResetMonkeyCellViews()
        {
            foreach (MonkeyCellController monkeyCellController in monkeyCellControllers)
            {
                monkeyCellController.ResetLockedCellView();
            }
        }

        public void SetActive(bool setActive) => cellContainer.gameObject.SetActive(setActive);
    }
}