using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ObjectsController : MonoBehaviour
    {
        public static ObjectsController Instance { get; private set; }
        public event Action<string> onObjectAdded;
        public event Action<string> onObjectRemoved;

        private IObjectRegistry _registry;
        private IObjectSpawner _objectSpawner;

        public void Init(IObjectSpawner objectSpawner, IObjectRegistry objectRegistry)
        {
            if (Instance == null)
            {
                Instance = this;
                _objectSpawner = objectSpawner;
                _registry = objectRegistry;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Add(InspectableObject obj)
        {
            if (_registry.TryRegisterObject(obj))
                onObjectAdded?.Invoke(obj.name);
        }

        public void Remove(string id)
        {
            if (_registry.TryUnregisterObject(id))
                onObjectRemoved?.Invoke(id);
        }

        public void ShowObjects(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (_registry.TryGetObject(id, out var obj))
                    obj.Show();
            }
        }


        public void HideObjects(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (_registry.TryGetObject(id, out var obj))
                    obj.Hide();
            }
        }

        public void SetAlpha(float alpha, List<string> ids)
        {
            alpha = Mathf.Clamp(alpha, 0, 1);

            foreach (var id in ids)
            {
                if (_registry.TryGetObject(id, out var obj))
                    obj.Alpha = alpha;
            }
        }

        public void SetColor(Color color, List<string> ids)
        {
            foreach (var id in ids)
            {
                if (_registry.TryGetObject(id, out var obj))
                    obj.Color = color;
            }
        }

        public ObjectData GetInfo(string id)
        {
            return _registry.GetObject(id).GetData();
        }

        public void SpawnObject()
        {
            _objectSpawner.SpawnObject();
        }

        public void DestroyObjects(List<string> ids)
        {
            List<InspectableObject> objects = _registry.GetObject(ids);

            for (int i = 0; i < objects.Count; i++)
                Destroy(objects[i].gameObject);
        }
    }
}