using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.Utilities;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("Scriptable Objects")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    [SerializeField] private SoundScriptableObject soundScriptableObject;

    [Header("Sound Service")]
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private AudioSource backgroundMusic;

    public PlayerService playerService { get; private set; }
    public SoundService soundService { get; private set; }

    void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
        soundService = new SoundService(soundScriptableObject, audioEffect, backgroundMusic);
    }

    void Update()
    {
        playerService.PlayerServiceUpdate();
    }
}
