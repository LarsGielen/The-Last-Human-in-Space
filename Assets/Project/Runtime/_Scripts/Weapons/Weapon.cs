using UnityEngine;

namespace Project.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public void Trigger()
        {
            print($"{transform.name} enter");
        }
    }
}
