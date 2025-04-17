using System;
using ATG.DateTimers;
using ATG.RealtimeChests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum ChestViewState: byte
    {
        None = 0,
        Unavailable = 1,
        Available = 2
    }
    
    public sealed class ChestView: MonoBehaviour
    {
        private const string AvailableAfter = "Unlock after:";
        private const string UnlockNow = "Unlock now!";
        private const string Lock = "Lock!";
        
        [SerializeField] private TMP_Text chestNameOutput;
        [SerializeField] private TMP_Text chestAvailableOutput;
        [SerializeField] private Image chestResourceIcon;
        [Space(10)]
        [SerializeField] private Button openChestBtn;

        private ViewState _currentState;
        
        public event Action OnChestOpenClicked;

        private void OnEnable() =>
            openChestBtn.onClick.AddListener(() => OnChestOpenClicked?.Invoke());
        
        private void OnDisable() =>
            openChestBtn.onClick.RemoveAllListeners();
        
        public void ShowMetaData(ChestMetaData meta)
        {
            chestNameOutput.text = meta.Name;
            chestResourceIcon.sprite = meta.Sprite;
        }

        public void SetupInitialState(bool isFinished)
        {
            if (isFinished == true)
            {
               SelectAvailableState();
            }
            else
            {
                SelectLockedState();
            }
        }
        
        public void UpdateTimer(CooldownTimerInfo timerInfo)
        {
            if (timerInfo.IsFinished == true)
            {
                SelectAvailableState();
            }
            else
            {
                SelectUnavailableState(timerInfo.TimeLeft.ToStringFormat());   
            }
        }
        
        public void SelectAvailableState()
        {
            if(_currentState == ViewState.Available) return;
            
            chestAvailableOutput.text = UnlockNow;
            openChestBtn.interactable = true;
            
            _currentState = ViewState.Available;
        }
        public void SelectUnavailableState(string timeLeft)
        {
            if (_currentState != ViewState.Unavailable)
            {
                openChestBtn.interactable = false;
            }
            
            chestAvailableOutput.text = $"{AvailableAfter} {timeLeft}";
            
            _currentState = ViewState.Unavailable;
        }

        public void SelectLockedState()
        {
            chestAvailableOutput.text = Lock;
            openChestBtn.interactable = false;
            _currentState = ViewState.Locked;
        }
        
        private enum ViewState: byte
        {
            None = 0,
            Unavailable = 1,
            Available = 2,
            Locked = 3
        }
    }
}