using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player;
using ServiceLocator.Player.Projectile;
using ServiceLocator.Wave.Bloon;
using ServiceLocator.Sound;

public class MonkeyController_UnitTest
{
    private MonkeyController monkeyController;

    //[SetUp]
    public void Setup()
    {
        MonkeyScriptableObject monkeySO = CreateDummyMonkeySO();
        ProjectilePool projectilePool = CreateDummyProjectilePool();
       // SoundService soundService = CreateDummySoundService();
        monkeyController = new MonkeyController(monkeySO, projectilePool);
    }

    private MonkeyScriptableObject CreateDummyMonkeySO()
    {
        MonkeyScriptableObject monkeySO = ScriptableObject.CreateInstance<MonkeyScriptableObject>();
        monkeySO.AttackableBloons = new List<BloonType>();
        monkeySO.AttackableBloons.Add(BloonType.Red);
        monkeySO.Range = 5;
        monkeySO.AttackRate = 1;
        MonkeyView monkeyPrefab = new GameObject().AddComponent<MonkeyView>();
        monkeyPrefab.gameObject.AddComponent<CircleCollider2D>();
        monkeyPrefab.gameObject.AddComponent<Animator>();
        monkeyPrefab.RangeSpriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
        monkeySO.Prefab = monkeyPrefab;
        return monkeySO;
    }

    private ProjectilePool CreateDummyProjectilePool()
    {
        ProjectileView projectilePrefab = new GameObject().AddComponent<ProjectileView>();
        List<ProjectileScriptableObject> projectileSOs = new List<ProjectileScriptableObject>();
        projectileSOs.Add(ScriptableObject.CreateInstance<ProjectileScriptableObject>());
        
        PlayerService playerService = CreaateDummyPlayerService(projectilePrefab, projectileSOs);
        return new ProjectilePool( projectilePrefab, projectileSOs);
    }

    private PlayerService CreaateDummyPlayerService(ProjectileView projectilePrefab, List<ProjectileScriptableObject> projectileSOs)
    {
        PlayerScriptableObject playerSO = ScriptableObject.CreateInstance<PlayerScriptableObject>();
        playerSO.ProjectilePrefab = projectilePrefab;
        playerSO.ProjectileScriptableObjects = projectileSOs;
        return PlayerService.Instance;
    }

    //private SoundService CreateDummySoundService()
    //{
    //    SoundScriptableObject soundSO = ScriptableObject.CreateInstance<SoundScriptableObject>();
    //    soundSO.audioList = new Sounds[0];
    //    return new SoundService(soundSO, new GameObject().AddComponent<AudioSource>(), new GameObject().AddComponent<AudioSource>());
    //}

    //[Test]
    public void CanAttackBloon_CheckCondition()
    {
        // Act
        bool canAttack = monkeyController.CanAttackBloon(BloonType.Red);

        // Assert
        //Assert.IsTrue(canAttack);
    }

}