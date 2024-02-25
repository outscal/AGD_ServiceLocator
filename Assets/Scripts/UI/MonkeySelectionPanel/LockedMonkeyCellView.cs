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

        [SerializeField] private MonkeyImageHandler monkeyImageHandler;
        [SerializeField] private TextMeshProUGUI unlockCostText;

        public void SetController(MonkeyCellController controllerToSet) => controller = controllerToSet;

        public void ConfigureCellUI(Sprite spriteToSet, string nameToSet, int costToSet)
        {
            monkeyImageHandler.ConfigureImageHandler(spriteToSet, controller);
        }
    }
}