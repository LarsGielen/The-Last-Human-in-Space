using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Weapon : MonoBehaviour
    {
        public void Enter()
        {
            print($"Weapon Name: {transform.name}");
        }
    }
}
