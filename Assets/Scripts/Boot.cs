using System;
using Objects;
using UI;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private ObjectsController controller;
    [SerializeField] private ObjectControllerUI controllerUI;

    private void Awake()
    {
        controller.Init();
        controllerUI.Init();
    }
}