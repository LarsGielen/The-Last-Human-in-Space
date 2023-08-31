using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class MovementData : AbilityComponentData
    {
        protected override void SetComponentDependency() => ComponentDependency = typeof(MovementComponent);

        public enum MoveTypeEnum { Velocity, Distance }
        public enum DirectionEnum { Character, World }

        [SerializeField] MoveTypeEnum moveType;
        [SerializeField] DirectionEnum directionType;
        [SerializeField] Vector2 direction;
        [SerializeField] float angle;
        [SerializeField] float value;

        public MoveTypeEnum MoveType { get => moveType; }
        public DirectionEnum DirectionType { get => directionType; }
        public Vector3 Direction { get => direction.normalized; }
        public float Angle { get => angle; }
        public float Value { get => value; }
    }
}
