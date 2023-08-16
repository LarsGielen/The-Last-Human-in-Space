using UnityEngine;

namespace Project
{
    [RequireComponent (typeof(PlayerHealth))]
    public class PlayerCore : MonoBehaviour
    {
        public PlayerHealth health {  get; private set; }

        private void Start()
        {
            health = GetComponent<PlayerHealth>();
        }
    }
}
