using System;
using System.Collections.Generic;

namespace Core
{
    public sealed class SpecSetViewModel
    {
        private readonly Dictionary<SpecType, SpecViewModel> _specs = new()
        {
            { SpecType.MoveSpeed, new SpecViewModel(SpecLocalization.Localize(SpecType.MoveSpeed)) },
            { SpecType.Stamina, new SpecViewModel(SpecLocalization.Localize(SpecType.Stamina)) },
            { SpecType.Dexterity, new SpecViewModel(SpecLocalization.Localize(SpecType.Dexterity)) },
            { SpecType.Intelligence, new SpecViewModel(SpecLocalization.Localize(SpecType.Intelligence)) },
            { SpecType.Damage, new SpecViewModel(SpecLocalization.Localize(SpecType.Damage)) },
            { SpecType.Regeneration, new SpecViewModel(SpecLocalization.Localize(SpecType.Regeneration)) }
        };
        
        public SpecViewModel this[SpecType specType] => _specs[specType];

        public event Action OnChanged;
        
        public void Setup(ICharacterSpecs specs)
        {
            _specs[SpecType.MoveSpeed].ChangeValue(specs.MoveSpeed);
            _specs[SpecType.Stamina].ChangeValue(specs.Stamina);
            _specs[SpecType.Dexterity].ChangeValue(specs.Dexterity);
            _specs[SpecType.Intelligence].ChangeValue(specs.Intelligence);
            _specs[SpecType.Damage].ChangeValue(specs.Damage);
            _specs[SpecType.Regeneration].ChangeValue(specs.Regeneration);
            
            OnChanged?.Invoke();
        }
    }
}