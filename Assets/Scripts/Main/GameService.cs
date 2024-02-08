using UnityEngine;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Player;
using ServiceLocator.UI;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoBehaviourSingleton<GameService>
    {
        // Services:
        private EventService eventService;
       
        private WaveService waveService;
      
       


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene References:
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgMusicSource;

        private void Start()
        {
            InitializeServices();
            InjectDependencies();
        }

        private void InitializeServices()
        {
            eventService = new EventService();
            
            SoundService.Instance.Init(soundScriptableObject, sfxSource, bgMusicSource);
            
            MapService.Instance.InitFromGameSerive(mapScriptableObject);
              
            PlayerService.Instance.InitGameService(playerScriptableObject);    
        
           
            WaveService.Instance.InitWaveFromGameService(waveScriptableObject);
        }

        private void InjectDependencies()
        {
            MapService.Instance.Init(eventService);
            UIService.Instance.Init(eventService);
            PlayerService.Instance.Init();
            WaveService.Instance.Init(eventService);
            //UIService.Instance.Init(  soundService, eventService);
        }

        private void Update()
        {
            PlayerService.Instance.Update();
        }
    }
}