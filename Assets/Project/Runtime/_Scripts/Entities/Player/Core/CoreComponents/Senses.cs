using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Project.Player.Core
{
    public class Senses : CoreComponent
    {
        public bool CheckGrounded()
        {
            Transform playerTransform = core.parentTransform;
            PlayerDataSO playerData = core.PlayerData;

            Vector3 spherePosition = new Vector3(playerTransform.position.x, playerTransform.position.y - playerData.GroundedOffset, playerTransform.position.z);
            return Physics.CheckSphere(spherePosition, playerData.GroundedRadius, playerData.GroundLayers, QueryTriggerInteraction.Ignore);
        }
    }
}
