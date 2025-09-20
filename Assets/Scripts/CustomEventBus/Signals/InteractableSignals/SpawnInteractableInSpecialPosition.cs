using Interactables;
using UnityEngine;

namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал-команда - "Заспавни Interactable в специальной позиции"
    /// </summary>
    public class SpawnInteractableInSpecialPosition
    {
        public readonly InteractableType InteractableType;
        public readonly int Grade;
        public readonly Transform SpawnPosition;

        public SpawnInteractableInSpecialPosition(InteractableType type, int grade, Transform spawnPosition)
        {
            InteractableType = type;
            Grade = grade;
            SpawnPosition = spawnPosition;
        }
    }
}