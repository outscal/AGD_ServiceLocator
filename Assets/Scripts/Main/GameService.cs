using ServiceLocator.Player;
using ServiceLocator.Utilities;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] public PlayerScriptableObject playerScriptableObject;

    public PlayerService playerService { get; private set; }

    void Start()
    {
        playerService = new PlayerService(playerScriptableObject);
    }

    void Update()
    {
        playerService.PlayerServiceUpdate();
    }
}
