using System;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObjectUI : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Image isVisibleImage;
        [SerializeField] private Sprite isVisibleSprite, isInvisibleSprite;
        [SerializeField] private Color isVisibleColor, isInvisibleColor;
        [SerializeField] private Slider alphaView;
        [SerializeField] private Image colorView;
      
        public Action<bool, string> onToggle;

        private string _id;

        public void Init(string id)
        {
            _id = id;
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnToggle);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }

        public string Id => _id;

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
            colorView.color = new Color(data.Color.r, data.Color.g, data.Color.b, 1f);
            SetVisibility(data.IsVisible);
        }

        private void SetVisibility(bool isVisible)
        {
            if (isVisible)
            {
                isVisibleImage.sprite = isVisibleSprite;
                isVisibleImage.color = isVisibleColor;
            }
            else
            {
                isVisibleImage.sprite = isInvisibleSprite;
                isVisibleImage.color = isInvisibleColor;
            }
        }
    }
}