using Project.Entity.Player.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ParticleManager : CoreComponent
    {
        [SerializeField] private List<GameObject> spawnObjects = new List<GameObject>();
        [SerializeField] ParticleSystem defaultParticle;

        [Header("Entity Parts")]
        [SerializeField] GameObject leftHand;
        [SerializeField] GameObject rightHand;
        [SerializeField] GameObject leftFoot;
        [SerializeField] GameObject rightFoot;

          public void SpawnParticleOnSpawnPoint(GameObject spawnPoint, ParticleSystem particleSystem)
        {
            ParticleSystem spawnedParticleSystem = Instantiate(particleSystem, spawnPoint.transform.position, Quaternion.identity);
            spawnedParticleSystem.transform.parent = spawnPoint.transform;
            spawnedParticleSystem.Play();
        }

        public void SpawnParticleOnIndexSpawnPoint(int index, ParticleSystem particleSystem)
        {
            SpawnParticleOnSpawnPoint(spawnObjects[index], particleSystem);
        }

        public void SpawnParticleOnLeftHand(ParticleSystem particleSystem)
        {
            SpawnParticleOnSpawnPoint(leftHand, particleSystem);
        }
        public void SpawnParticleOnRightHand(ParticleSystem particleSystem)
        {
            SpawnParticleOnSpawnPoint(rightHand, particleSystem);
        }
        public void SpawnParticleOnLeftFoot(ParticleSystem particleSystem)
        {
            SpawnParticleOnSpawnPoint(leftFoot, particleSystem);
        }
        public void SpawnParticleOnRightFoot(ParticleSystem particleSystem)
        {
            SpawnParticleOnSpawnPoint(rightFoot, particleSystem);
        }


        [ContextMenu("Spawn particle Test 1")]
        public void SpawnParticleTest()
        {
            SpawnParticleOnIndexSpawnPoint(0, defaultParticle);
        }

        [ContextMenu("Spawn particle Test 2")]
        public void SpawnParticleOnSpecificPartTest()
        {
            SpawnParticleOnRightFoot(defaultParticle); //Verander hier de functie zodat je verschillende delen kan testen.
        }
    }
}
