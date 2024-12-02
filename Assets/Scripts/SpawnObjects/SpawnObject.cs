using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [System.Serializable]
    public class ObjectSpawnConfig
    {
        public GameObject objectToSpawn;       // El objeto a instanciar
        public Transform[] spawnPoints;       // Sus puntos de spawn específicos
    }
    public ObjectSpawnConfig[] spawnConfigs; // Lista de objetos y sus spawn points
    void Start()
    {
        foreach (ObjectSpawnConfig config in spawnConfigs)
        {
            // Iterar a través de los puntos de spawn específicos para este objeto
            foreach (Transform spawnPoint in config.spawnPoints)
            {
                // Si el objeto tiene puntos de spawn definidos
                if (config.spawnPoints.Length > 0)
                {
                    // Seleccionar un punto aleatorio
                    int randomIndex = Random.Range(0, config.spawnPoints.Length);
                    Transform randomSpawnPoint = config.spawnPoints[randomIndex];

                    // Instanciar el objeto en el punto seleccionado
                    Instantiate(config.objectToSpawn, randomSpawnPoint.position, randomSpawnPoint.rotation, randomSpawnPoint);
                }
            }
        }
    }
}
