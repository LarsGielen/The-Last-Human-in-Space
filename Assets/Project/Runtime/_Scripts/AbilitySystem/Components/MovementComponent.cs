using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class MovementComponent : AbilityComponent<MovementData>
    {
        private Entity.CoreSystem.Movement CoreMovement { get => _coreMovement ??= ability.Core.GetCoreComponent<Entity.CoreSystem.Movement>(); }
        private Entity.CoreSystem.Movement _coreMovement;

        Vector2 direction;

        protected override void Enter()
        {
            base.Enter();

            calculate();

            switch (data.MoveType)
            {
                case MovementData.MoveTypeEnum.Velocity:
                    CoreMovement.AddHorizontalVelocity(direction, data.Value);
                    break;
                case MovementData.MoveTypeEnum.Distance:
                    CoreMovement.AddHorizontalVelocityByDistance(direction, data.Value);
                    break;
            }

        }

        protected override void Exit()
        {
            base.Exit();
        }

        protected override void AbilityUpdate()
        {
            base.AbilityUpdate();
        }

        private void calculate()
        {
            if (data.DirectionType == MovementData.DirectionEnum.World)
            {
                direction = data.Direction;
            }
            else
            {
                float angle = CoreMovement.facingRotation + data.Angle;
                Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                direction = new Vector2(dir.x, dir.z);
            }
        }
    }
}
