//using NUnit.Framework;
using UnityEngine;
using ServiceLocator.Main;
using ServiceLocator.Player;
using ServiceLocator.Player.Projectile;
using System.Collections.Generic;

public class PlayerService_UnitTest 
{
   // private PlayerService playerService;

    //[SetUp]
    public void Setup()
    {
        PlayerScriptableObject playerSO = CreateDummyPlayerSO();
        Transform projectileContainer = new GameObject().transform;

       // playerService = new PlayerService(playerSO);
        
    }

    private PlayerScriptableObject CreateDummyPlayerSO()
    {
        PlayerScriptableObject playerScriptableObject = ScriptableObject.CreateInstance<PlayerScriptableObject>();
        playerScriptableObject.ProjectilePrefab = new GameObject().AddComponent<ProjectileView>();
        playerScriptableObject.ProjectileScriptableObjects = new List<ProjectileScriptableObject>();
        playerScriptableObject.Health = 10;
        playerScriptableObject.Money = 0;
        return playerScriptableObject;
    }

    //[Test]
    public void GetReward_AddsMoney()
    {
        // Arrange
        var initialMoney = PlayerService.Instance.Money;

        // Act
        PlayerService.Instance.GetReward(100);

        // Assert
        //Assert.AreEqual(initialMoney + 100, playerService.Money);
    }
}
