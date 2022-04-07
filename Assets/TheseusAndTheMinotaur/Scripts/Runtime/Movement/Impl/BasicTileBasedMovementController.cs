using System;
using UnityEngine;

namespace TheseusAndTheMinotaur.Movement
{
    internal class BasicTileBasedMovementController : ITileBasedMovement
    {
        public event Action GotExhausted;

        public int MaxMoveCount => _humbleObject.Humble_MaxMoveCountLeft;
        public int CurrentMoveCountLeft => _humbleObject.Humble_CurrentMoveCountLeft;
        public IGameEntity Entity => _humbleObject.Entity;

        private readonly ITileBasedMovementHumbleObject _humbleObject;

        public BasicTileBasedMovementController(ITileBasedMovementHumbleObject humbleObject)
        {
            _humbleObject = humbleObject;
        }

        public void ResetMoveCount()
        {
            SetMoveCount(MaxMoveCount);
        }

        public MoveResult Move(Direction direction)
        {
            if (CheckIsExhausted())
            {
                return GetExhaustedMoveResult();
            }

            ITile currentTile = Entity.CurrentTile;
            if (!CheckCanMoveToDirection(direction))
            {
                return GetFailedMoveResult();
            }

            ITile newTile = currentTile.ParentMap.GetNeighbourTile(currentTile, direction);

            OnMoveSuccessful(currentTile, newTile);

            return new MoveResult(
                currentTile, newTile, MoveResultType.Success);
        }

        private void OnMoveSuccessful(ITile currentTile, ITile newTile)
        {
            Entity.SetCurrentTile(newTile);

            currentTile.UnlinkEntity(Entity);
            newTile.AddEntity(Entity);

            SetMoveCount(CurrentMoveCountLeft - 1);
            NotifyIfExhausted();
        }

        private void NotifyIfExhausted()
        {
            if (!CheckIsExhausted())
            {
                return;
            }

            NotifyExhausted();
        }

        private void NotifyExhausted()
        {
            GotExhausted?.Invoke();
        }

        private void SetMoveCount(int value)
        {
            value = Mathf.Clamp(value, 0, MaxMoveCount);
            _humbleObject.Humble_CurrentMoveCountLeft = value;
        }

        private bool CheckIsExhausted()
        {
            bool result = CurrentMoveCountLeft == 0;
            return result;
        }

        private MoveResult GetFailedMoveResult()
        {
            return new MoveResult(
                Entity.CurrentTile,
                Entity.CurrentTile,
                MoveResultType.Failure);
        }

        private MoveResult GetExhaustedMoveResult()
        {
            return new MoveResult(
                Entity.CurrentTile,
                Entity.CurrentTile,
                MoveResultType.Exhausted);
        }

        public bool CheckCanMoveToDirection(Direction direction)
        {
            ITile currentTile = Entity.CurrentTile;
            bool result = !currentTile.CheckIsObstacleOfDirectionActive(direction) &&
                          currentTile.ParentMap.CheckHasValidNeighbourTile(currentTile, direction);

            return result;
        }
    }
}