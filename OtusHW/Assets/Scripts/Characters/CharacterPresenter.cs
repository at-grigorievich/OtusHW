using ATG.Animators;
using ATG.Move;
using UnityEngine;

namespace ATG.Characters
{
    public abstract class CharacterPresenter
    {
        private readonly IMoveableService _moveableService;
        private readonly IAnimatorService _animatorService;

        public Vector3 CurrentPosition => _moveableService.CurrentPosition;
        public float CurrentSpeed => _moveableService.Speed;

        public CharacterPresenter(IMoveableService moveService, IAnimatorService animatorService)
        {
            _moveableService = moveService;
            _animatorService = animatorService;
        }
        
        public float DistanceTo(Vector3 point) => Vector3.Distance(CurrentPosition, point);
        public void MoveTo(Vector3 position) => _moveableService.MoveTo(position);
        public void MoveStop() => _moveableService.Stop();
        public void AnimateIdle() => _animatorService.Idle();
        public void AnimateWalk() => _animatorService.Walk();
    }
}