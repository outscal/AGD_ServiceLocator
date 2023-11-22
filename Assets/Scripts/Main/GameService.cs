using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Wave;
using ServiceLocator.Events;
using ServiceLocator.Map;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; } 
    public WaveService waveService { get; private set; }
    public EventService eventService { get; private set; }

    [Header("Player Attributes")]
    [SerializeField] public PlayerScriptableObject playerScriptableObject;

    [Header("Sound Attributes")]
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    [Header("UI Attributes")]
    [SerializeField] private UIService uIService;
    [SerializeField] private MapService mapService;

    [Header("Wave Attributes")]
    [SerializeField] private WaveScriptableObject waveScriptableObject;

    public UIService UIService => uIService;
    public MapService MapService => mapService;

    private void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
        soundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
        waveService = new WaveService(waveScriptableObject);

    }

    public void Update()
    {
        playerService.Update();
    }

}
