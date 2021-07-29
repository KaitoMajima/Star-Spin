using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class SpawnObject : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawn;

        [SerializeField] private Transform spawnPoint;

        public GameObject Spawn()
        {
            var instantiatedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
            return instantiatedObject;
        }

        public GameObject ChangeObject(GameObject obj)
        {
            objectToSpawn = obj;
            return objectToSpawn;
        }
    }
}
