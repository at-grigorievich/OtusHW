using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.Animators
{
    public enum CrossfadeAnimationType
    {
        Idle = 0,
        Walk = 1
    }
    
    [Serializable]
    public sealed class CrossfadeAnimatorPair
    {
        [SerializeField] private CrossfadeAnimationType tag;
        [SerializeField] private string animatorStateName;
        
        public KeyValuePair<CrossfadeAnimationType, int> GetPair() => new (tag, Animator.StringToHash(animatorStateName));
    }

    [Serializable]
    public sealed class CrossfadeAnimatorFactory
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CrossfadeAnimatorPair[] pairs;

        public IAnimatorService Create() => 
            new CrossfadeAnimator(animator, pairs.Select(p => p.GetPair()));
    }
    
    public sealed class CrossfadeAnimator: IAnimatorService
    {
        private readonly Animator _animator;
        private readonly Dictionary<CrossfadeAnimationType, int> _crossfadeDic;
        
        public CrossfadeAnimator(Animator animator, IEnumerable<KeyValuePair<CrossfadeAnimationType, int>> pairs)
        {
            _animator = animator;
            _crossfadeDic = new Dictionary<CrossfadeAnimationType, int>(pairs);
        }
        
        public void Idle()
        {
            int hash = GetHashCodeByTag(CrossfadeAnimationType.Idle);
            if(hash == -1) return;
            
            PlayCrossfade(0, hash);
        }

        public void Walk()
        {
            int hash = GetHashCodeByTag(CrossfadeAnimationType.Walk);
            if(hash == -1) return;
            
            PlayCrossfade(0, hash);
        }

        private int GetHashCodeByTag(CrossfadeAnimationType tag)
        {
            return _crossfadeDic.GetValueOrDefault(tag, -1);
        }

        private void PlayCrossfade(int layer, int stateHash)
        {
            if (_animator.HasState(layer, stateHash) == false)
            {
                throw new System.ArgumentException("Animator does not have state");
            }
            
            _animator.CrossFade(stateHash, layer);
        }
    }
}