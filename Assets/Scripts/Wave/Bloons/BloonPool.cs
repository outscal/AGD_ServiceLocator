using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using UnityEditor;

/*  This script demonstrates the implementation of Object Pool design pattern.
 *  If you're interested in learning about Object Pooling, you can find
 *  a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 * */

namespace ServiceLocator.Wave.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private PlayerService playerService;
        private WaveService waveService;
        private SoundService soundService;

        private BloonView bloonPrefab;
        private List<BloonScriptableObject> bloonScriptableObjects;
        private Transform bloonContainer;

        public BloonPool(WaveService waveService, PlayerService playerService, SoundService soundService, WaveScriptableObject waveScriptableObject)
        {
            this.playerService = playerService;
            this.waveService = waveService;
            this.soundService = soundService;

            bloonPrefab = waveScriptableObject.BloonPrefab;
            bloonScriptableObjects = waveScriptableObject.BloonScriptableObjects;
            bloonContainer = new GameObject("Bloon Container").transform;
        }

        public void RemoveBloonPool()
        {
            playerService = null;
            waveService = null;
            soundService = null;
            bloonContainer.gameObject.SetActive(false);
        }

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonController bloon = GetItem();
            BloonScriptableObject scriptableObjectToUse = bloonScriptableObjects.Find(so => so.Type == bloonType);
            bloon.Init(scriptableObjectToUse);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController(playerService, waveService, soundService, bloonPrefab, bloonContainer);
    }
}