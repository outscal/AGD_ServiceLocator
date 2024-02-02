using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("References")]
    [SerializeField] private UIService uIService;

    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private MapScriptableObject mapScriptableObject;

    [Header("Sound Service")]
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private AudioSource backgroundMusic;

    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; }
    public UIService UIService => uIService;
    public EventService eventService { get; private set; }
    public MapService mapService { get; private set; }

    void Awake()
    {
        base.Awake();
        eventService = new EventService();
    }

    void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
        soundService = new SoundService(soundScriptableObject, audioEffect, backgroundMusic);
        mapService = new MapService(mapScriptableObject);
    }

    void Update()
    {
        playerService.PlayerServiceUpdate();
    }
}
