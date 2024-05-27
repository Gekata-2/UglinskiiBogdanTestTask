using Objects;
using UI;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private ObjectsController controller;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private ObjectControllerUI controllerUI;
    [SerializeField] private int startNumberOfObjects;

    private void Awake()
    {
        controller.Init(objectSpawner, new DictionaryRegistry());
        controllerUI.Init();

        for (int i = 0; i < startNumberOfObjects; i++)
            controller.SpawnObject();
    }
}