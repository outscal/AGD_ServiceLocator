using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ServiceLocator.UI
{
    public class MonkeyCellView : MonoBehaviour
    {
        private MonkeyCellController controller;
        private MonkeyCellState cellState;

        [SerializeField] private MonkeyImageHandler monkeyImageHandler;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI unlockCostText;

        public void SetController(MonkeyCellController controllerToSet) => controller = controllerToSet;

        public void ConfigureCellUI(Sprite spriteToSet, string nameToSet, int costToSet,MonkeyCellState cellState,int unlockCost)
        {
            monkeyImageHandler.ConfigureImageHandler(spriteToSet, controller);
            nameText.SetText(nameToSet);
            costText.SetText(costToSet.ToString());
            unlockCostText.SetText(unlockCost.ToString());
            this.cellState = cellState;
        }

    }
}