using System;
using Core;
using TMPro;
using UI.Elements;
using UnityEngine;

namespace UI.Views
{
    public sealed class CharacterLevelView: MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text levelOutput;
        [SerializeField] private OutputSlider experienceSlider;

        private LevelCharacterViewModel _viewModel;

        public void Init(LevelCharacterViewModel viewModel)
        {
            _viewModel = viewModel;
            
            experienceSlider.SetPrefix("XP");
        }
        
        public void Show()
        {
            Dispose();
            Visualize();

            _viewModel.OnChanged += Visualize;
            _viewModel.OnLevelChanged += ChangeCurrentLevel;
            _viewModel.OnExperienceChanged += ChangeCurrentLevel;
        }

        public void Hide()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            experienceSlider?.Dispose();
            
            _viewModel.OnChanged -= Visualize;
            _viewModel.OnLevelChanged -= ChangeCurrentLevel;
            _viewModel.OnExperienceChanged -= ChangeCurrentLevel;
        }

        private void Visualize()
        {
            ChangeCurrentLevel();
            ChangeCurrentExperience();
        }

        private void ChangeCurrentLevel()
        {
            levelOutput.text = $"Level: {_viewModel.CurrentLevel}";
            ChangeCurrentExperience();
        }

        private void ChangeCurrentExperience()
        {
            experienceSlider.SetValue(
                _viewModel.CurrentExperience, 
                _viewModel.CurrentMaxExperience,
                _viewModel.PreviousMaxExperience);
        }
    }
}