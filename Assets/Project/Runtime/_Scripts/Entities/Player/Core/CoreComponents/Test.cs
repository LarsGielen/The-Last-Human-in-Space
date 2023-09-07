using Project.Entity.Player.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Test : CoreComponent
    {
        [SerializeField]
        private List<List<GameObjectAndParticleSystem>> spawnPointLists = new List<List<GameObjectAndParticleSystem>>();

        // Struct om een GameObject en ParticleSystem te koppelen
        [System.Serializable]
        public struct GameObjectAndParticleSystem
        {
            public GameObject gameObject;
            public ParticleSystem particleSystem;
        }

        public void SpawnParticleOnSpawnPoint(Vector3 spawnPoint, ParticleSystem particleSystem)
        {
            ParticleSystem spawnedParticleSystem = Instantiate(particleSystem, spawnPoint, Quaternion.identity);
            spawnedParticleSystem.Play();
        }

        public void SpawnParticleOnIndexSpawnPoint(int listIndex, int elementIndex, ParticleSystem particleSystem)
        {
            if (listIndex >= 0 && listIndex < spawnPointLists.Count)
            {
                List<GameObjectAndParticleSystem> spawnPointList = spawnPointLists[listIndex];
                if (elementIndex >= 0 && elementIndex < spawnPointList.Count)
                {
                    GameObject spawnPointObject = spawnPointList[elementIndex].gameObject;
                    ParticleSystem spawnPointParticleSystem = spawnPointList[elementIndex].particleSystem;

                    if (spawnPointObject != null && spawnPointParticleSystem != null)
                    {
                        SpawnParticleOnSpawnPoint(spawnPointObject.transform.position, particleSystem);
                    }
                    else
                    {
                        Debug.LogError("GameObject or ParticleSystem is null.");
                    }
                }
                else
                {
                    Debug.LogError("Invalid element index.");
                }
            }
            else
            {
                Debug.LogError("Invalid list index.");
            }
        }

        [ContextMenu("Spawn Particle Test")]
        public void SpawnParticleTest()
        {
            ParticleSystem particle = new GameObject("NewParticleSystem").AddComponent<ParticleSystem>();
            SpawnParticleOnIndexSpawnPoint(0, 0, particle);
        }
    }
}
