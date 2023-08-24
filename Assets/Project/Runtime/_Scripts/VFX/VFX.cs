using Project.Utility;
using UnityEngine;
using UnityEngine.VFX;

namespace Project
{
    public class VFX : MonoBehaviour
    {
        [SerializeField] private float duration;

        private void Awake()
        {
            Destroy(gameObject, duration);
        }
    }
}
