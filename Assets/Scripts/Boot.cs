using System;
using Objects;
using UnityEngine;

public class Boot : MonoBehaviour
{  [SerializeField] private ObjectsController controller;

    private void Awake()
    {
        controller.Init();
    }
}
