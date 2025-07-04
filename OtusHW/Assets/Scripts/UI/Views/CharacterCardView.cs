using System;
using Core;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    [RequireComponent(typeof(Canvas))]
    public class CharacterCardView: MonoBehaviour, IDisposable
    {
        [SerializeField] private AboutCharacterView aboutCharacterView;
        [SerializeField] private CharacterLevelView characterLevelView;
        [SerializeField] private SpecSetView specSetView;
        [Space(10)]
        [SerializeField] private BigButton levelUpBtn;
        [SerializeField] private Button closeBtn;
        
        private Canvas _canvas;
        
        private CharacterViewModel _viewModel;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            Hide();
        }

        private void OnEnable()
        {
            closeBtn.onClick.AddListener(Hide);
            levelUpBtn.AddListener(LevelUp);
        }

        private void OnDisable()
        {
            closeBtn.onClick.RemoveAllListeners();
            levelUpBtn.RemoveListener(LevelUp);
        }

        public void Init(CharacterViewModel viewModel)
        {
            _viewModel = viewModel;
            
            aboutCharacterView.Init(viewModel.CharacterDescription);
            characterLevelView.Init(viewModel.CharacterLevel);
            specSetView.Init(viewModel.SpecSet);
        }
        
        public void Show()
        {
            Dispose();
            
            aboutCharacterView.Show();
            characterLevelView.Show();
            specSetView.Show();
            
            _viewModel.OnLevelUpAllowed += OnLevelUpAvailableHandler;
            OnLevelUpAvailableHandler(_viewModel.AvailableLevelUp);
            
            _canvas.enabled = true;
        }

        public void Hide()
        {
            Dispose();
            
            aboutCharacterView.Hide();
            characterLevelView.Hide();
            specSetView.Hide();
            
            _canvas.enabled = false;
        }

        public void Dispose()
        {
            _viewModel.OnLevelUpAllowed -= OnLevelUpAvailableHandler;
        }

        private void OnLevelUpAvailableHandler(bool isLvlUpAvailable)
        {
            levelUpBtn.SetAvailable(isLvlUpAvailable);
        }

        private void LevelUp()
        {
            if(_viewModel == null) return;
            _viewModel.LevelUp();
        }
    }
}