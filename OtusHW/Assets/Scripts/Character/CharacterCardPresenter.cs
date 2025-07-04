using System;
using Core.Model;
using UI.Views;
using UnityEngine;
using VContainer;

namespace Core
{
    [Serializable]
    public sealed class CharacterCardCreator
    {
        [SerializeField] private CharacterCardView view;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<CharacterCardPresenter>(Lifetime.Singleton)
                .WithParameter(view);
        }
    }
    
    public sealed class CharacterCardPresenter
    {
        private readonly CharacterCardView _cardView;
        private readonly CharacterViewModel _characterViewModel;
        
        private CharacterModel _characterModel;
        
        public CharacterCardPresenter(CharacterCardView cardView)
        {
            _cardView = cardView;
            
            _characterViewModel = new CharacterViewModel();
            
            _cardView.Init(_characterViewModel);
        }

        public void OpenPopup()
        {
            if(_characterModel == null)
            {
                //throw new NullReferenceException("The character is not selected.");
                Debug.LogWarning("The character is not selected.");
                return;
            }
            
            _cardView.Show();
        }

        public void OpenPopup(CharacterModel nextModel)
        {
            _characterModel = nextModel;
            
            _characterViewModel.ChangeModel(_characterModel);
            _cardView.Show();
        }

        public void HidePopup()
        {
            _cardView.Hide();
        }

        public void AddExpToCharacter(int experience)
        {
            if (_characterViewModel == null)
            {
                //throw new NullReferenceException("no character selected");
                Debug.LogWarning("The character is not selected.");
                return;
            }
            
            _characterViewModel.AddExperience(experience);
        }
        
    }
}