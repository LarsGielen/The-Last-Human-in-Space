using UnityEngine;

namespace Project.Entity.CoreSystem
{
    public class Senses : CoreComponent
    {
        public bool CheckGrounded()
        {
            Transform playerTransform = core.parentTransform;
            EntityDataSO playerData = core.EntityData;

            Vector3 spherePosition = new Vector3(playerTransform.position.x, playerTransform.position.y - playerData.GroundedOffset, playerTransform.position.z);
            return Physics.CheckSphere(spherePosition, playerData.GroundedRadius, playerData.GroundLayers, QueryTriggerInteraction.Ignore);
        }
    }
}
