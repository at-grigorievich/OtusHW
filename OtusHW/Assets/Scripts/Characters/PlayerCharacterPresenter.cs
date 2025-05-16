using System;
using ATG.Animators;
using ATG.Move;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Characters
{
    [Serializable]
    public sealed class PlayerCharacterCreator
    {
        [SerializeField] private CrossfadeAnimatorFactory crossfadeAnimatorFactory;
        [SerializeField] private NavMeshMoveServiceFactory navMeshMoveServiceFactory;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<CharacterPresenter, PlayerCharacterPresenter>(Lifetime.Singleton)
                .WithParameter<IMoveableService>(navMeshMoveServiceFactory.Create())
                .WithParameter<IAnimatorService>(crossfadeAnimatorFactory.Create())
                .AsSelf().AsImplementedInterfaces();
        }
    }
    
    public sealed class PlayerCharacterPresenter: CharacterPresenter, ITickable
    {
        public PlayerCharacterPresenter(IMoveableService moveService, IAnimatorService animatorService) 
            : base(moveService, animatorService)
        {
        }

        public void Tick()
        {
            Vector2 inputAxis = InputService.GetAxis();

            if (inputAxis.sqrMagnitude > 0)
            {
                Move(inputAxis);
            }
            else
            {
                Idle();
            }
        }

        private void Move(Vector2 axis)
        {
            Vector3 direction = new Vector3(axis.x, 0, axis.y).normalized;
            Vector3 targetPosition = CurrentPosition + direction * CurrentSpeed;
            
            MoveTo(targetPosition);
            AnimateWalk();
        }

        private void Idle()
        {
            MoveStop();
            AnimateIdle();
        }
    }
}