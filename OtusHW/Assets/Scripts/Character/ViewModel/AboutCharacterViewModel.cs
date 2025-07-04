using System;
using Core.Model;
using UnityEngine;

namespace Core
{
    public sealed class AboutCharacterViewModel
    {
        public string Nick { get; private set; }
        public Sprite Avatar { get; private set; }
        public string Description { get; private set; }

        public event Action OnChanged;
        
        public void Setup(IDescriptive descriptive)
        {
            Nick = descriptive.Nick;
            Avatar = descriptive.Avatar;
            Description = descriptive.Description;
            
            OnChanged?.Invoke();
        }
    }
}