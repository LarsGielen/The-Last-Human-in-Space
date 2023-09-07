using Project.Entity.Player.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ParticleManager : CoreComponent
    {
        [SerializeField] private List<GameObject> spawnObjects = new List<GameObject>();
        [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

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
        public void SpawnParticleOnIndexSpawnPoint(int index)
        {
            SpawnParticleOnSpawnPoint(spawnObjects[index], particleSystems[index]);
        }



        [ContextMenu("Spawn Particle Test")]
        public void SpawnParticleTest()
        {
            SpawnParticleOnIndexSpawnPoint(0);
        }
    }
}
