using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [System.Serializable]
    public class ObjectSpawnConfig
    {
        public GameObject objectToSpawn;       // El objeto a instanciar
        public Transform[] spawnPoints;       // Sus puntos de spawn específicos
        public int spawnCount;                //cantidad de objetos a spawnear
    }
    public ObjectSpawnConfig[] spawnConfigs; // Lista de objetos y sus spawn points
    void Start()
    {
        foreach (ObjectSpawnConfig config in spawnConfigs)
        {

           if(config.spawnPoints.Length == 0)
           {
              Debug.LogWarning($"El objeto {config.objectToSpawn.name} no tiene puntos de spawn definidos.");
              continue;
           }

           //Creo una lista que me permita saber que puntos ya hay objetos y que puntos no hay
           List<Transform> availablePoints = new List<Transform>(config.spawnPoints);
           
           //Limito la cantidad de objetos a spawnear a la cantidad de puntos que hay
           int spawnLimit = Mathf.Min(config.spawnCount, availablePoints.Count);

            for (int i = 0; i < spawnLimit; i++)
            {
                // Elegir un punto aleatorio entre los disponibles
                int randomIndex = Random.Range(0, availablePoints.Count);
                Transform selectedSpawnPoint = availablePoints[randomIndex];
                // Instanciar el objeto en el punto seleccionado
                Instantiate(config.objectToSpawn, selectedSpawnPoint.position, selectedSpawnPoint.rotation, selectedSpawnPoint);
                //Eliminar el punto utilizado para evitar repeticiones
                availablePoints.RemoveAt(randomIndex);
                
            }







        }
    }
}
