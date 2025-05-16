using ATG.Animators;
using ATG.Move;

namespace ATG.Characters
{
    public sealed class BotCharacterPresenter: CharacterPresenter
    {
        public BotCharacterPresenter(IMoveableService moveService, IAnimatorService animatorService) 
            : base(moveService, animatorService)
        {
        }
    }
}