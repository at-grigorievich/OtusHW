using System;
using ATG.Items;
using ATG.Items.Equipment;
using ATG.Items.Inventory;
using ATG.OtusHW.Inventory;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class EquipmentTests
{
    #region Только экипировка
    // Добавление одной экипировки и проверка списка экипировки
    [Test]
    public void WhenTakeOnItem_ThenEquipmentCellFilled()
    {
        Equipment equipment = new Equipment();

        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        Item sword = new Item("sword", ItemFlags.Equippable, equipmentComponent);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, sword));
    }

    // Добавление одной экипировки и ее удаление
    [Test]
    public void WhenTakeOnItem_AndTakeOffItem_ThenEquipmentCellEmpty()
    {
        Equipment equipment = new Equipment();

        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        Item sword = new Item("sword", ItemFlags.Equippable, equipmentComponent);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        EquipmentUseCases.TakeOffItem(equipment, sword);
        
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, sword));
    }
    
    // Добавление одной экипировки и проверка действия эффектов от экипировки
    [Test]
    public void WhenCreateEquipment_AndTakeOnEquipment_ThenHeroGetEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        HeroDamageEffectComponent damageEffectComponent = new() { DamageEffect = 10 };
        
        Item sword = new Item("sword", ItemFlags.Equippable, 
            equipmentComponent, speedEffectComponent, damageEffectComponent);

        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 5f));
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 20f));
        Assert.IsTrue(Mathf.Approximately(hero.HitPoints, 10f));
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, sword));
    }
    
    // Добавление нескольких типов экипировки и проверка суммирования эффектов
    [Test]
    public void WhenCreateEquipments_AndTakeOnEquipments_ThenHeroGetEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent swordEquipmentComponent = new() { Tag = EquipType.RightHand };
        HeroEquipmentComponent bodyEquipmentComponent = new() { Tag = EquipType.Body };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        HeroDamageEffectComponent damageEffectComponent = new() { DamageEffect = 10 };
        HeroHitPointsEffectComponent hitPointsEffectComponent = new() { HitPointsEffect = 10 };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        Item sword = new Item("sword", ItemFlags.Equippable, swordEquipmentComponent, 
            speedEffectComponent, damageEffectComponent);
        Item arm = new Item("arm", ItemFlags.Equippable, bodyEquipmentComponent, 
            speedEffectComponent, hitPointsEffectComponent);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        EquipmentUseCases.TakeOnItem(equipment, arm);
        
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 0f));
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 20f));
        Assert.IsTrue(Mathf.Approximately(hero.HitPoints, 20f));
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, sword));
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, arm));
    }
    
    // Удаление нескольких типов экипировки и проверка отсутствия эффектов от удаленной экипировки
    [Test]
    public void WhenCreateEquipments_AndTakeOffEquipments_ThenHeroRemoveEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent swordEquipmentComponent = new() { Tag = EquipType.RightHand };
        HeroEquipmentComponent bodyEquipmentComponent = new() { Tag = EquipType.Body };
        
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        HeroDamageEffectComponent damageEffectComponent = new() { DamageEffect = 10 };
        HeroHitPointsEffectComponent hitPointsEffectComponent = new() { HitPointsEffect = 10 };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        Item sword = new Item("sword", ItemFlags.Equippable, swordEquipmentComponent, 
            speedEffectComponent, damageEffectComponent);
        Item arm = new Item("arm", ItemFlags.Equippable, bodyEquipmentComponent, 
            speedEffectComponent, hitPointsEffectComponent);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        EquipmentUseCases.TakeOnItem(equipment, arm);
        
        EquipmentUseCases.TakeOffItem(equipment, sword);
        EquipmentUseCases.TakeOffItem(equipment, arm);
        
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 10f));
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 10f));
        Assert.IsTrue(Mathf.Approximately(hero.HitPoints, 10f));
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, sword));
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, arm));
    }
    
    // Добавление типа экипировки с тегом EquipType.None
    [Test]
    public void WhenTakeOnEquipment_AndEquipTypeIsNone_ThenEquipmentCellNotFilled()
    {
        Equipment equipment = new Equipment();
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        HeroEquipmentComponent noneEquipmentComponent = new() { Tag = EquipType.None };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        
        Item none = new Item("none", ItemFlags.Equippable, noneEquipmentComponent, 
            speedEffectComponent);
        
        var exception = Assert.Throws<Exception>(() => EquipmentUseCases.TakeOnItem(equipment, none));
        Assert.AreEqual($"Try to take on {none.Id} with empty equipment tag.", exception.Message);
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 10f));
    }
    
    // Удаление типа экипировки с тегом EquipType.None
    [Test]
    public void WhenTakeOffEquipment_AndEquipTypeIsNone_ThenEquipmentCellNotFilled()
    {
        Equipment equipment = new Equipment();
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        HeroEquipmentComponent noneEquipmentComponent = new() { Tag = EquipType.None };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        
        Item none = new Item("none", ItemFlags.Equippable, noneEquipmentComponent, 
            speedEffectComponent);
        
        var exception = Assert.Throws<Exception>(() => EquipmentUseCases.TakeOffItem(equipment, none));
        Assert.AreEqual($"Try to take off {none.Id} with empty equipment tag.", exception.Message);
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 10f));
    }
    
    // Попытка снять экипировку из пустой ячейки
    [Test]
    public void WhenTakeOffEquipment_AndEquipmentCellZero_ThenReturnNull()
    {
        Equipment equipment = new Equipment();
        HeroEquipmentComponent swordEquipmentComponent = new() { Tag = EquipType.RightHand };
        
        Item sword = new Item("sword", ItemFlags.Equippable, swordEquipmentComponent);
        
        Item removedItem = EquipmentUseCases.TakeOffItem(equipment, sword);
        
        Assert.IsNull(removedItem);
    }
    #endregion
    #region Экипировка вместе с инвентарем
    // Использование предмета экипировки из инвентаря и добавление его в экипировки и применение эффектов
    [Test]
    public void WhenTakeOnEquipment_AndRemoveFromInventory_ThenEquipmentCellFilledAndHasEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        Inventory inventory = new();
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        Item sword = new Item("sword", ItemFlags.Equippable, 
            equipmentComponent, speedEffectComponent);
        
        InventoryUseCases.AddItem(inventory, sword);
        
        InventoryToEquipmentUseCases.EquipItem(inventory, equipment, sword);
        
        Assert.IsFalse(InventoryUseCases.HasItem(inventory, sword));
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, sword));
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 5f));
    }
    
    // Удаление предмета из экипировки и добавление его в инвентарь и сброса эффектов
    [Test]
    public void WhenTakeOffEquipment_AndAddToInventory_ThenEquipmentCellNotFilledAndZeroEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        Inventory inventory = new();
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        Item sword = new Item("sword", ItemFlags.Equippable, 
            equipmentComponent, speedEffectComponent);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        
        InventoryToEquipmentUseCases.UnequipItem(inventory, equipment, sword);
        
        Assert.IsTrue(InventoryUseCases.HasItem(inventory, sword));
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, sword));
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 10f));
    }
    
    // Попытка добавить в экипировки предмет без флага 
    [Test]
    public void WhenCreateItemWithNoEquipFlag_ThenTakeOnEquipment_ThenEquipmentCellNotFilledAndNoHasEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        Inventory inventory = new();
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        HeroSpeedEffectComponent speedEffectComponent = new() { SpeedEffect = -5 };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        Item sword = new Item("sword", ItemFlags.Equippable, 
            equipmentComponent, speedEffectComponent);
        
        EquipmentUseCases.TakeOnItem(equipment, sword);
        
        InventoryToEquipmentUseCases.UnequipItem(inventory, equipment, sword);
        
        Assert.IsTrue(InventoryUseCases.HasItem(inventory, sword));
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, sword));
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 10f));
    }
    
    // Попытка замены экипировки
    [Test]
    public void WhenAddEquipmentItem_AndReplaceEquipmentItem_ThenEquipmentUpdateEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        Inventory inventory = new();
        Equipment equipment = new Equipment();
        
        HeroEquipmentComponent equipmentComponent = new() { Tag = EquipType.RightHand };
        HeroDamageEffectComponent swordDamageEffectComponent = new() { DamageEffect = 5 };
        HeroDamageEffectComponent axeDamageEffectComponent = new() { DamageEffect = 10 };
        
        IEquipmentObserver equipmentObserver = new HeroEquipEffectObserver(equipment, hero);
        
        Item sword = new Item("sword", ItemFlags.Equippable, equipmentComponent, swordDamageEffectComponent);
        Item axe = new Item("axe", ItemFlags.Equippable, equipmentComponent, axeDamageEffectComponent);
        
        InventoryUseCases.AddItem(inventory, sword);
        InventoryUseCases.AddItem(inventory, axe);
        
        InventoryToEquipmentUseCases.EquipItem(inventory, equipment, sword);
        
        Assert.IsFalse(InventoryUseCases.HasItem(inventory, sword));
        Assert.IsTrue(InventoryUseCases.HasItem(inventory, axe));
        
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, axe));
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, sword));
        
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 15f));
        
        // Меняем оружие
        InventoryToEquipmentUseCases.EquipItem(inventory, equipment, axe);
        
        Assert.IsFalse(InventoryUseCases.HasItem(inventory, axe));
        Assert.IsTrue(InventoryUseCases.HasItem(inventory, sword));
        
        Assert.IsTrue(EquipmentUseCases.HasItem(equipment, axe));
        Assert.IsFalse(EquipmentUseCases.HasItem(equipment, sword));
        
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 20f));
    }
    
    #endregion
}
