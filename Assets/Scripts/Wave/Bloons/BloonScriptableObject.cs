using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Wave.Bloon
{
    [CreateAssetMenu(fileName = "BloonScriptableObject", menuName = "ScriptableObjects/BloonScriptableObject")]
    public class BloonScriptableObject : ScriptableObject
    {
        public BloonType Type;
        public int Health; // max health at the start
        public int HealthRegenerationValue; // value of health regeneration
        public float HealthRegenerationAfter; // will start regeneration of health if not hit for the provided seconds
        public int Damage;
        public int Reward;
        public float Speed;
        public Sprite FullHealthSprite;
        public Sprite LowHealthSprite;
        public List<BloonType> LayeredBloons;
        public float LayerBloonSpawnRate;
    }
}