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

            if (!CheckCanMoveToDirection(direction))
            {
                return GetFailedMoveResult();
            }

            ITile currentTile = Entity.CurrentTile;
            if (!currentTile.ParentMap.TryGetNeighbourTile(
                    currentTile, direction, out ITile newTile))
            {
                return GetFailedMoveResult();
            }

            OnMoveSuccessful(currentTile, newTile);

            return new MoveResult(
                currentTile, newTile, MoveResultType.Success);
        }

        private void OnMoveSuccessful(ITile currentTile, ITile newTile)
        {
            Entity.CurrentTile = newTile;

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

        private bool CheckCanMoveToDirection(Direction direction)
        {
            bool result = Entity.CurrentTile.CheckIsObstacleOfDirectionActive(direction);
            return result;
        }
    }
}