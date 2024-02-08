using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace ServiceLocator.Wave.Bloon
{
    public class BloonController
    {
        // Dependencies:
      
       
        

        private BloonView bloonView;
        private BloonScriptableObject bloonScriptableObject;

        private const float waypointThreshold = 0.1f;
        private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;
        private BloonState currentState;
        private CancellationTokenSource regenerationCancellationTokenSource;


        public Vector3 Position => bloonView.transform.position;

        public BloonController(   BloonView bloonPrefab, Transform bloonContainer)
        {
           
          

            bloonView = UnityEngine.Object.Instantiate(bloonPrefab, bloonContainer);
            bloonView.Controller = this;
        }

        public void Init(BloonScriptableObject bloonScriptableObject)
        {
            this.bloonScriptableObject = bloonScriptableObject;
            InitializeVariables();
            SetState(BloonState.ACTIVE);
        }

        private void InitializeVariables()
        {
            bloonView.SetRenderer(bloonScriptableObject.FullHealthSprite);
            currentHealth = bloonScriptableObject.Health;
            waypoints = new List<Vector3>();
        }

        public void SetPosition(Vector3 spawnPosition)
        {
            bloonView.transform.position = spawnPosition;
            bloonView.gameObject.SetActive(true);
        }

        public void SetWayPoints(List<Vector3> waypointsToSet, int startingWaypointIndex)
        {
            waypoints = waypointsToSet;
            currentWaypointIndex = startingWaypointIndex;
        }

        public void SetOrderInLayer(int orderInLayer) => bloonView.SetSortingOrder(orderInLayer);

        public void TakeDamage(int damageToTake)
        {
            if(regenerationCancellationTokenSource != null){
                regenerationCancellationTokenSource.Cancel();
                regenerationCancellationTokenSource.Dispose();
            }

            var reducedHealth = currentHealth - damageToTake;
            currentHealth = reducedHealth <= 0 ? 0 : reducedHealth;

            if(currentHealth <= 0 && currentState == BloonState.ACTIVE)
            {
                PopBloon();
                SoundService.Instance.PlaySoundEffects(SoundType.BloonPop);
            }else if(currentHealth <= currentHealth * 0.25 && currentState == BloonState.ACTIVE){
                bloonView.SetRenderer(bloonScriptableObject.LowHealthSprite);
            }else{
                bloonView.SetRenderer(bloonScriptableObject.FullHealthSprite);
            }

            regenerationCancellationTokenSource = new CancellationTokenSource();
            RegenerateHealth(regenerationCancellationTokenSource.Token);
        }

        private async void RegenerateHealth(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(bloonScriptableObject.HealthRegenerationAfter), cancellationToken);
                if (!cancellationToken.IsCancellationRequested)
                {
                    currentHealth += bloonScriptableObject.HealthRegenerationValue;
                    currentHealth = Mathf.Min(currentHealth, bloonScriptableObject.Health); 
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        public void FollowWayPoints()
        {
            if(HasReachedFinalWaypoint())
            {
                ResetBloon();
            }
            else
            {
                Vector3 direction = GetDirectionToMoveTowards();
                MoveBloon(direction);
                if (HasReachedNextWaypoint(direction.magnitude))
                    currentWaypointIndex++;
            }
        }

        private bool HasReachedFinalWaypoint() => currentWaypointIndex == waypoints.Count;

        private bool HasReachedNextWaypoint(float distance) => distance < waypointThreshold;

        private void ResetBloon()
        {
            WaveService.Instance.RemoveBloon(this);
            PlayerService.Instance.TakeDamage(bloonScriptableObject.Damage);
            bloonView.gameObject.SetActive(false);
        }

        private Vector3 GetDirectionToMoveTowards() => waypoints[currentWaypointIndex] - bloonView.transform.position;

        private void MoveBloon(Vector3 moveDirection) => bloonView.transform.Translate(moveDirection.normalized * bloonScriptableObject.Speed * Time.deltaTime);

        private void PopBloon()
        {
            SetState(BloonState.POPPED);
            bloonView.PopBloon();
        }

        public void OnPopAnimationPlayed()
        {
            if (HasLayeredBloons())
                SpawnLayeredBloons();

            PlayerService.Instance.GetReward(bloonScriptableObject.Reward);
            WaveService.Instance.RemoveBloon(this);
        }

        private bool HasLayeredBloons() => bloonScriptableObject.LayeredBloons.Count > 0;

        private void SpawnLayeredBloons() => WaveService.Instance.SpawnBloons(bloonScriptableObject.LayeredBloons,
                                                                     bloonView.transform.position,
                                                                     currentWaypointIndex,
                                                                     bloonScriptableObject.LayerBloonSpawnRate);

        public BloonType GetBloonType() => bloonScriptableObject.Type;

        private void SetState(BloonState state) => currentState = state;
    }

    public enum BloonState
    {
        ACTIVE,
        POPPED
    }
}