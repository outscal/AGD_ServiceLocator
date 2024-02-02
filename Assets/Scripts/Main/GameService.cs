using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using ServiceLocator.Wave;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("References")]
    [SerializeField] private UIService uIService;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private MapScriptableObject mapScriptableObject;
    [SerializeField] private WaveScriptableObject waveScriptableObject;

    [Header("Sound Service")]
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private AudioSource backgroundMusic;

    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; }
    public UIService UIService => uIService;
    public EventService eventService { get; private set; }
    public MapService mapService { get; private set; }
    public WaveService waveService { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        eventService = new EventService();
    }

    void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
        soundService = new SoundService(soundScriptableObject, audioEffect, backgroundMusic);
        mapService = new MapService(mapScriptableObject);
        waveService = new WaveService(waveScriptableObject);
    }

    void Update()
    {
        playerService.PlayerServiceUpdate();
    }
}
