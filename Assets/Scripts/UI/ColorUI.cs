using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ColorUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;
        public event Action<float> OnChannelValueChanged;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveAllListeners();
        }

        private void OnValueChanged(float val)
        {
            text.text = val.ToString();
            float v = val / slider.maxValue;
            OnChannelValueChanged?.Invoke(v);
        }
    }
}