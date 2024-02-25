using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ServiceLocator.UI
{
    public class LockedMonkeyCellView : MonoBehaviour
    {
        private MonkeyCellController controller;

        [SerializeField] private LockedCellHandler lockedCellHandler;
        [SerializeField] private TextMeshProUGUI unlockCostText;

        public void SetController(MonkeyCellController controllerToSet) => controller = controllerToSet;

        public void ConfigureCellUI(int unlockCost)
        {
            lockedCellHandler.ConfigureImageHandler(controller);
            unlockCostText.SetText(unlockCost.ToString());
        }
    }
}