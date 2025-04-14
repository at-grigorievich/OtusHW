using System;
using ATG.OtusHW.Inventory;

namespace ATG.Items
{
    public interface IItemComponent
    {
        IItemComponent Clone();
    }

    [Serializable]
    public class StackableItemComponent: IItemComponent
    {
        public int Count;
        public int MaxCount;
        
        public IItemComponent Clone()
        {
            return new StackableItemComponent() { Count = 1, MaxCount = MaxCount };
        }
    }
    
    public abstract class HeroEffectComponent : IItemComponent
    {
        public abstract IItemComponent Clone();
        public abstract void AddEffect(IHero hero);
        public abstract void RemoveEffect(IHero hero);
    }
    
    [Serializable]
    public class HeroDamageEffectComponent : HeroEffectComponent
    {
        public int DamageEffect = 2;
        
        public override IItemComponent Clone()
        {
            return new HeroDamageEffectComponent()
            {
                DamageEffect = this.DamageEffect
            };
        }

        public override void AddEffect(IHero hero)
        {
            hero.Damage += DamageEffect;
        }

        public override void RemoveEffect(IHero hero)
        {
            hero.Damage -= DamageEffect;
        }
    }
    
    [Serializable]
    public class HeroHitPointsEffectComponent : HeroEffectComponent
    {
        public int HitPointsEffect = 2;
        
        public override IItemComponent Clone()
        {
            return new HeroHitPointsEffectComponent()
            {
                HitPointsEffect = this.HitPointsEffect
            };
        }

        public override void AddEffect(IHero hero)
        {
            hero.HitPoints += HitPointsEffect;
        }

        public override void RemoveEffect(IHero hero)
        {
            hero.HitPoints -= HitPointsEffect;
        }
    }
    
    [Serializable]
    public class HeroSpeedEffectComponent : HeroEffectComponent
    {
        public int SpeedEffect = 2;
        
        public override IItemComponent Clone()
        {
            return new HeroSpeedEffectComponent()
            {
                SpeedEffect = this.SpeedEffect
            };
        }

        public override void AddEffect(IHero hero)
        {
            hero.Speed += SpeedEffect;
        }

        public override void RemoveEffect(IHero hero)
        {
            hero.Speed -= SpeedEffect;
        }
    }

    [Serializable]
    public class HeroEquipmentComponent : IItemComponent
    {
        public EquipType Tag;
        
        public IItemComponent Clone()
        {
            return new HeroEquipmentComponent()
            {
                Tag = this.Tag
            };
        }
    }
}