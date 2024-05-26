using System;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObjectUI : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Image isVisible;
        [SerializeField] private Slider alphaView;
        [SerializeField] private Image colorView;
        private string _id;
        
        public Action<bool, string> onToggle;
        public string Id => _id;
        public void Init(string id)
        {
            _id = id;
            toggle.onValueChanged.AddListener(OnToggle);
        }

        private void OnToggle(bool isToggled)
        {
            onToggle?.Invoke(isToggled, _id);
        }

        public void SetToggle(bool v)
        {
            toggle.isOn = v;
        }

        public void UpdateView(ObjectData data)
        {
            alphaView.value = data.Alpha;
            colorView.color = data.Color;
            colorView.color = new Color(colorView.color.r, colorView.color.b, colorView.color.g, 1f);
        }

     

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}