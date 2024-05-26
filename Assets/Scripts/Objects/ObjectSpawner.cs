using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2 boundsX;
        [SerializeField] private Vector2 boundsY;
        [SerializeField] private Vector2 boundsZ;
        [SerializeField] private List<GameObject> prefabs;
        [SerializeField] private SideMenu sideMenu;

        private void Start()
        {
            sideMenu.onSpawnObject += SpawnObject;
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 pos;
            pos.x = Random.Range(boundsX.x, boundsX.y);
            pos.y = Random.Range(boundsY.x, boundsY.y);
            pos.z = Random.Range(boundsZ.x, boundsZ.y);

            return transform.position + pos;
        }

        private Quaternion GetRandomRotation()
        {
            Vector3 rotation;
            rotation.x = Random.Range(-180f, 180f);
            rotation.y = Random.Range(-180f, 180f);
            rotation.z = Random.Range(-180f, 180f);

            return Quaternion.Euler(rotation);
        }

        private GameObject GetRandomGameObject() => prefabs[Random.Range(0, prefabs.Count - 1)];

        public void SpawnObject()
        {
            GameObject go = Instantiate(
                GetRandomGameObject(),
                GetRandomPosition(),
                Quaternion.identity,
                transform);
            if (go.TryGetComponent(out InspectableObject obj))
            {
                obj.View.transform.rotation = GetRandomRotation();
            }
        }
    }
}