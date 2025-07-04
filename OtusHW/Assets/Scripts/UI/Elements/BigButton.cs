using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class BigButton: MonoBehaviour
    {
        [SerializeField] private Sprite availableSprite;
        [SerializeField] private Sprite unavailableSprite;
        
        private Image _img;
        private Button _btn;

        private Action _onClick;

        private void Awake()
        {
            _img = GetComponent<Image>();
            _btn = GetComponent<Button>();

            _btn.transition = Selectable.Transition.ColorTint;
            _btn.targetGraphic = _img;
        }
        
        private void OnEnable()
        {
            _btn.onClick.AddListener(InvokeClickCallback);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveListener(InvokeClickCallback);
        }
        
        public void SetAvailable(bool isAvailable)
        {
            _btn.interactable = isAvailable;
            _img.sprite = isAvailable ? availableSprite : unavailableSprite;
        }
        
        public void AddListener(Action onClick) => _onClick += onClick;
        public void RemoveListener(Action onClick) => _onClick -= onClick;

        private void InvokeClickCallback()
        {
            _onClick?.Invoke();
        }
    }
}