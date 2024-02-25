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

        [SerializeField] private MonkeyImageHandler monkeyImageHandler;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;

        public void SetController(MonkeyCellController controllerToSet) => controller = controllerToSet;

        public void ConfigureCellUI(Sprite spriteToSet, string nameToSet, int costToSet)
        {
            monkeyImageHandler.ConfigureImageHandler(spriteToSet, controller);
            nameText.SetText(nameToSet);
            costText.SetText(costToSet.ToString());
        }
    }
}