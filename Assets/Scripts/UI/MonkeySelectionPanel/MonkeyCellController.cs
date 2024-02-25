using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class MonkeyCellController
    {
        private PlayerService playerService;
        private MonkeyCellView monkeyCellView;
        private LockedMonkeyCellView lockedMonkeyCellView;
        private MonkeyCellScriptableObject monkeyCellSO;

        public MonkeyCellController(PlayerService playerService, Transform cellContainer, MonkeyCellView monkeyCellPrefab, LockedMonkeyCellView lockedMonkeyCellPrefab, MonkeyCellScriptableObject monkeyCellScriptableObject)
        {
            this.playerService = playerService;
            this.monkeyCellSO = monkeyCellScriptableObject;
            monkeyCellView = Object.Instantiate(monkeyCellPrefab, cellContainer);
            monkeyCellView.SetController(this);
            monkeyCellView.ConfigureCellUI(monkeyCellSO.Sprite, monkeyCellSO.Name, monkeyCellSO.Cost);
            if (monkeyCellSO.cellState == MonkeyCellState.LOCKED)
            {
                monkeyCellView.gameObject.SetActive(false);
                lockedMonkeyCellView = Object.Instantiate(lockedMonkeyCellPrefab, cellContainer);
                lockedMonkeyCellView.SetController(this);
                lockedMonkeyCellView.ConfigureCellUI(monkeyCellSO.UnlockCost);
            }
        }

        public void MonkeyDraggedAt(Vector3 dragPosition)
        {
            playerService.ValidateSpawnPosition(monkeyCellSO.Cost, dragPosition);
        }

        public void MonkeyDroppedAt(Vector3 dropPosition)
        {
            playerService.TrySpawningMonkey(monkeyCellSO.Type, monkeyCellSO.Cost, dropPosition);
        }

        public void UnlockMonkey()
        {
            if (playerService.TryUnlockMonkey(monkeyCellSO.UnlockCost))
            {
                monkeyCellView.gameObject.SetActive(true);
                lockedMonkeyCellView.gameObject.SetActive(false);
            }
        }
    }
}