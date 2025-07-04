using System;
using Core;
using UnityEngine;

namespace UI.Views
{
    public sealed class SpecSetView: MonoBehaviour, IDisposable
    {
        [SerializeField] private SpecView moveSpecView;
        [SerializeField] private SpecView staminaSpecView;
        [SerializeField] private SpecView dexteritySpecView;
        [SerializeField] private SpecView intelligenceSpecView;
        [SerializeField] private SpecView damageSpecView;
        [SerializeField] private SpecView regenerationView;

        private SpecSetViewModel _viewModel;
        
        public void Init(SpecSetViewModel viewModel)
        {
            _viewModel = viewModel;
            
            moveSpecView.Init(_viewModel[SpecType.MoveSpeed]);
            staminaSpecView.Init(_viewModel[SpecType.Stamina]);
            dexteritySpecView.Init(_viewModel[SpecType.Dexterity]);
            intelligenceSpecView.Init(_viewModel[SpecType.Intelligence]);
            damageSpecView.Init(_viewModel[SpecType.Damage]);
            regenerationView.Init(_viewModel[SpecType.Regeneration]);
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
            moveSpecView.Show();
            staminaSpecView.Show();
            dexteritySpecView.Show();
            intelligenceSpecView.Show();
            damageSpecView.Show();
            regenerationView.Show();
        }
    }
}