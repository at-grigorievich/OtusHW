using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public sealed class AboutCharacterView: MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text nicknameOutput;
        [SerializeField] private Image avatarImage;
        [SerializeField] private TMP_Text descriptionOutput;
        
        private AboutCharacterViewModel _viewModel;

        public void Init(AboutCharacterViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public void Show()
        {
            Dispose();
            Visualize();
            
            _viewModel.OnChanged += Visualize;
        }

        public void Hide()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            _viewModel.OnChanged -= Visualize;
        }
        
        private void Visualize()
        {
            nicknameOutput.text = $"@{_viewModel.Nick}";
            descriptionOutput.text = _viewModel.Description;
            
            avatarImage.sprite = _viewModel.Avatar;
        }
    }
}