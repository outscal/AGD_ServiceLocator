using System.Collections.Generic;
using ServiceLocator.Events;
using ServiceLocator.Player;
using ServiceLocator.Wave;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ReSharper disable All

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Gameplay Panel")] [SerializeField]
        private GameObject gameplayPanel;

        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI waveProgressText;
        [SerializeField] private TextMeshProUGUI currentMapText;
        [SerializeField] private Button nextWaveButton;

        [Header("Level Selection Panel")] [SerializeField]
        private GameObject levelSelectionPanel;

        [SerializeField] private List<MapButton> mapButtons;

        [FormerlySerializedAs("MonkeySelectionPanel")] [SerializeField]
        private GameObject monkeySelectionPanel;

        [SerializeField] private Transform cellContainer;
        [SerializeField] private MonkeyCellView monkeyCellPrefab;
        [SerializeField] private LockedMonkeyCellView lockedMonkeyCellPrefab;
        [SerializeField] private List<MonkeyCellScriptableObject> monkeyCellScriptableObjects;

        [Header("Game End Panel")] [SerializeField]
        private GameObject gameEndPanel;

        [SerializeField] private GameObject levelUnlockText;
        [SerializeField] private TextMeshProUGUI gameEndText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button quitButton;

        private EventService eventService;

        // Dependencies:
        private int mapBtnCount = -1;

        [Header("Monkey Selection UI")] private MonkeySelectionUIController monkeySelectionController;
        private PlayerService playerService;

        private WaveService waveService;

        private void Start()
        {
            gameplayPanel.SetActive(false);
            gameEndPanel.SetActive(false);

            nextWaveButton.onClick.AddListener(OnNextWaveButton);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        }

        public void Init(WaveService waveService, PlayerService playerService, EventService eventService)
        {
            this.waveService = waveService;
            this.eventService = eventService;
            this.playerService = playerService;

            InitializeMapSelectionUI(eventService);
            InitializeMonkeySelectionUI(playerService);
            SubscribeToEvents();
        }

        private void InitializeMapSelectionUI(EventService eventService)
        {
            levelSelectionPanel.SetActive(true);
            foreach (MapButton mapButton in mapButtons)
            {
                mapButton.Init(eventService);
            }
            EnableNewMap();
        }

        private void EnableNewMap()
        {
            mapBtnCount++;
            mapButtons[mapBtnCount].ToggleMapButton(true);
        }

        private void InitializeMonkeySelectionUI(PlayerService playerService)
        {
            monkeySelectionController = new MonkeySelectionUIController(playerService, cellContainer, monkeyCellPrefab,
                lockedMonkeyCellPrefab, monkeyCellScriptableObjects);
            monkeySelectionPanel.SetActive(false);
            monkeySelectionController.SetActive(false);
        }

        private void SubscribeToEvents() => eventService.OnMapSelected.AddListener(OnMapSelected);

        public void OnMapSelected(int mapID)
        {
            levelSelectionPanel.SetActive(false);
            gameplayPanel.SetActive(true);
            monkeySelectionPanel.SetActive(true);
            monkeySelectionController.SetActive(true);
            currentMapText.SetText("Map: " + mapID);
        }

        private void OnNextWaveButton()
        {
            waveService.StartNextWave();
            SetNextWaveButton(false);
        }

        private void OnQuitButtonClicked() => Application.Quit();

        private void OnPlayAgainButtonClicked()
        {
            gameEndPanel.SetActive(false);
            monkeySelectionPanel.SetActive(false);
            levelSelectionPanel.SetActive(true);
            monkeySelectionController.ResetMonkeyCellViews();
            playerService.ClearActiveMonkeys();
        }

        public void SetNextWaveButton(bool setInteractable) => nextWaveButton.interactable = setInteractable;

        public void UpdateHealthUI(int healthToDisplay) => healthText.SetText(healthToDisplay.ToString());

        public void UpdateMoneyUI(int moneyToDisplay) => moneyText.SetText(moneyToDisplay.ToString());

        public void UpdateWaveProgressUI(int waveCompleted, int totalWaves) =>
            waveProgressText.SetText(waveCompleted.ToString() + "/" + totalWaves.ToString());

        public void UpdateGameEndUI(bool hasWon)
        {
            gameplayPanel.SetActive(false);
            levelSelectionPanel.SetActive(false);
            gameEndPanel.SetActive(true);

            if (hasWon)
            {
                EnableNewMap();
                gameEndText.SetText("You Won");
                levelUnlockText.SetActive(true);
            }
            else
            {
                gameEndText.SetText("Game Over");
                levelUnlockText.SetActive(false);
            }
        }
    }
}