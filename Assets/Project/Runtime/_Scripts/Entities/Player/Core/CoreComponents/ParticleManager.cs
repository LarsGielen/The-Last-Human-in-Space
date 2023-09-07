using Project.Entity.Player.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ParticleManager : CoreComponent
    {
        [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
        [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        public void SpawnParticleOnSpawnPoint(Vector3 spawnPoint, ParticleSystem particleSystem)
        {
            ParticleSystem spawnedParticleSystem = Instantiate(particleSystem, spawnPoint, Quaternion.identity);
            spawnedParticleSystem.Play();
        }

        public void SpawnParticleOnIndexSpawnPoint(int index, ParticleSystem particleSystem)
        {
            SpawnParticleOnSpawnPoint(spawnPoints[index].transform.position, particleSystem);
        }
        public void SpawnParticleOnIndexSpawnPoint(int index)
        {
            SpawnParticleOnSpawnPoint(spawnPoints[index].transform.position, particleSystems[index]);
        }



        [ContextMenu("Spawn Particle Test")]
        public void SpawnParticleTest()
        {
            SpawnParticleOnIndexSpawnPoint(0);
        }
    }
}
