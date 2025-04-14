using System;
using ATG.Items;
using ATG.Items.Inventory;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CraftingTests
{
    #region Добавление стакиваемых/нестакиваемых обьектов
    //Когда в инвентарь добавляются два нестакиваемых одинаковых предмета, они отображаются как два нестакиваемых предмета
    [Test]
    public void WhenAddNoStackableItem_AndHasTwoNonStackableItems_ThenTwoCellsInInventory()
    {
        int count = 3;
        Inventory inventory = new();
        
        Item moneyBag = new("money_bag");
        
        InventoryUseCases.AddItems(inventory, moneyBag.Clone(), count);
        
        int usedCells = InventoryUseCases.GetUsedCellsForItem(inventory, moneyBag);
        int totalCount = InventoryUseCases.GetTotalCountForItem(inventory, moneyBag);
        
        Assert.IsTrue(usedCells == count);
        Assert.IsTrue(totalCount == count);
    }
    
    //Когда в инвентарь добавляются n стакиваемых одинаковых предмета, они не перевывашают один лимит
    [Test]
    public void WhenAddStackableItem_AndNoLimitExceed_PlaceOneCellInInventory()
    {
        int currentCount = 1;
        int maxCount = 5;
        
        Inventory inventory = new();

        StackableItemComponent stackComponent = new()
        {
            Count = currentCount,
            MaxCount = maxCount
        };
        
        Item ham = new("money_bag", ItemFlags.Stackable, stackComponent);
        
        InventoryUseCases.AddItems(inventory, ham.Clone(), 4);
        
        int usedCells = InventoryUseCases.GetUsedCellsForItem(inventory, ham);
        int totalCount = InventoryUseCases.GetTotalCountForItem(inventory, ham);
        
        Assert.IsTrue(usedCells == 1);
        Assert.IsTrue(totalCount == 4);
    }
    
    //Когда в инвентарь добавляются n стакиваемых одинаковых предмета, они перевывашают один лимит
    [Test]
    public void WhenAdd14StackableItem_AndLimitExceed_PlaceThreeCellInInventory()
    {
        int currentCount = 1;
        int maxCount = 5;
        
        Inventory inventory = new();

        StackableItemComponent stackComponent = new()
        {
            Count = currentCount,
            MaxCount = maxCount
        };
        
        Item ham = new("ham", ItemFlags.Stackable, stackComponent);
        
        InventoryUseCases.AddItems(inventory, ham, 14);
        
        int usedCells = InventoryUseCases.GetUsedCellsForItem(inventory, ham);
        int totalItems = InventoryUseCases.GetTotalCountForItem(inventory, ham);
        Assert.IsTrue(usedCells == 3);
        Assert.IsTrue(totalItems == 14);
    }
    
    //Когда в инвентарь добавляются n стакиваемых одинаковых предмета, на них есть компонент StackableItemComponent, 
    // но нет флага "Stackable"
    [Test]
    public void WhenAddStackableItem_AndAddStackableItemComponentWithNoAddingStackableFlag_ThrowException()
    {
        int currentCount = 1;
        int maxCount = 5;
        
        Inventory inventory = new();

        StackableItemComponent stackComponent = new()
        {
            Count = currentCount,
            MaxCount = maxCount
        };
        
        Item ham = new("ham", stackComponent);
        
        var exception = Assert.Throws<Exception>(() => InventoryUseCases.AddItems(inventory, ham, 10));
        Assert.AreEqual($"Item {ham.Id} no has stack flag, but has StackableItemComponent", exception.Message);
    }
    
    //Когда в инвентарь добавляются n стакиваемых одинаковых предмета, на них нет компонента StackableItemComponent, 
    // но есть флаг "Stackable"
    [Test]
    public void WhenAddStackableItem_AndAddStackableFlagWithNoAddingStackableItemComponent_ThrowException()
    {
        Inventory inventory = new();
        
        Item ham = new("ham", ItemFlags.Stackable, null);
        
        var exception = Assert.Throws<Exception>(() => InventoryUseCases.AddItems(inventory, ham, 10));
        Assert.AreEqual($"Item {ham.Id} has stack flag, but no has StackableItemComponent", exception.Message);
    }
    #endregion
    #region Удаление стакиваемых/нестакиваемых обьектов
    // Когда добавили два одинаковых нестакиваемых предмета, а затем один удалили, должен остаться только один предмет
    [Test]
    public void WhenAddTwoNoStackableItems_AndRemoveOneItem_ThenOneCellInInventory()
    {
        int count = 2;
        Inventory inventory = new();
        
        Item moneyBag = new("money_bag");
        
        InventoryUseCases.AddItems(inventory, moneyBag.Clone(), count);
        InventoryUseCases.RemoveItem(inventory, moneyBag);
        
        int newCount = InventoryUseCases.GetUsedCellsForItem(inventory, moneyBag);
        int totalCount = InventoryUseCases.GetTotalCountForItem(inventory, moneyBag);
        
        Assert.IsTrue(newCount == count - 1);
        Assert.IsTrue(totalCount == count - 1);
    }
    
    // Когда добавили два одинаковых нестакиваемых предмета, а затем три удалили, должно остаться ноль предметов
    [Test]
    public void WhenAddTwoNoStackableItems_AndRemoveThreeItems_ThenZeroCellInInventory()
    {
        int count = 2;
        Inventory inventory = new();
        
        Item moneyBag = new("money_bag");
        
        InventoryUseCases.AddItems(inventory, moneyBag.Clone(), count);
        InventoryUseCases.RemoveItems(inventory, moneyBag, 3);
        
        int newCount = InventoryUseCases.GetUsedCellsForItem(inventory, moneyBag);
        int totalCount = InventoryUseCases.GetTotalCountForItem(inventory, moneyBag);
        
        Assert.IsTrue(newCount == 0);
        Assert.IsTrue(totalCount == 0);
    }
    
    //Когда добавили 14 одинаковых стакивыемых предмета, а затем 7 удалили, должно остаться 2 ячейки и 7 предметов.
    [Test]
    public void WhenAdd14StackableItems_AndRemove7Item_ThenTwoCellInInventory()
    {
        int currentCount = 1;
        int maxCount = 5;
        
        Inventory inventory = new();

        StackableItemComponent stackComponent = new()
        {
            Count = currentCount,
            MaxCount = maxCount
        };
        
        Item ham = new("ham", ItemFlags.Stackable, stackComponent);
        
        InventoryUseCases.AddItems(inventory, ham, 14);
        InventoryUseCases.RemoveItems(inventory, ham, 7);
        
        int usedCells = InventoryUseCases.GetUsedCellsForItem(inventory, ham);
        int totalItems = InventoryUseCases.GetTotalCountForItem(inventory, ham);
        
        Assert.IsTrue(usedCells == 2);
        Assert.IsTrue(totalItems == 7);
    }
    
    // Когда добавили 14 одинаковых стакиваемых предмета, а затем удалили 17, должно остаться 0 ячеек и 0 предметов.
    [Test]
    public void WhenAdd14StackableItems_AndRemove17Item_ThenZeroCellInInventory()
    {
        int currentCount = 1;
        int maxCount = 5;
        
        Inventory inventory = new();

        StackableItemComponent stackComponent = new()
        {
            Count = currentCount,
            MaxCount = maxCount
        };
        
        Item ham = new("ham", ItemFlags.Stackable, stackComponent);
        
        InventoryUseCases.AddItems(inventory, ham, 14);
        InventoryUseCases.RemoveItems(inventory, ham, 17);
        
        int usedCells = InventoryUseCases.GetUsedCellsForItem(inventory, ham);
        int totalItems = InventoryUseCases.GetTotalCountForItem(inventory, ham);
        
        Assert.IsTrue(usedCells == 0);
        Assert.IsTrue(totalItems == 0);
    }
    
    #endregion

    #region Получение/удаление эффектов для героя
    // Когда добавили предмет в инвентарь с эффектами, герой получает баф статам
    [Test]
    public void WhenAddEffectableItem_ThenHeroGetEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        Inventory inventory = new();
        IInventoryObserver effectsController = new HeroItemsEffectsController(inventory, hero);

        HeroSpeedEffectComponent speedEffectComponent = new()
        {
            SpeedEffect = 10
        };

        HeroDamageEffectComponent damageEffectComponent = new()
        {
            DamageEffect = 10
        };

        HeroHitPointsEffectComponent hitPointsEffectComponent = new()
        {
            HitPointsEffect = 10
        };
        
        Item food = new Item("food", ItemFlags.Effectable, speedEffectComponent, damageEffectComponent, hitPointsEffectComponent);
        
        InventoryUseCases.AddItem(inventory, food);
        
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 20f));
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 20f));
        Assert.IsTrue(Mathf.Approximately(hero.HitPoints, 20f));
    }
    
    // Когда добавили предметы в инвентарь с эффектами, герой получает сумму бафов к статам
    [Test]
    public void WhenAddSomeEffectableItem_ThenHeroGetSumEffect()
    {
        IHero hero = new HeroData()
        {
            Damage = 10f,
            Speed = 10f,
            HitPoints = 10f
        };
        Inventory inventory = new();
        IInventoryObserver effectsController = new HeroItemsEffectsController(inventory, hero);

        HeroSpeedEffectComponent speedEffectComponent = new()
        {
            SpeedEffect = 10
        };

        HeroDamageEffectComponent damageEffectComponent = new()
        {
            DamageEffect = 10
        };

        HeroHitPointsEffectComponent hitPointsEffectComponent = new()
        {
            HitPointsEffect = 10
        };
        
        Item food = new Item("food", ItemFlags.Effectable, speedEffectComponent, damageEffectComponent, hitPointsEffectComponent);
        Item fish = new Item("fish", ItemFlags.Effectable, speedEffectComponent, hitPointsEffectComponent);
        InventoryUseCases.AddItem(inventory, food);
        InventoryUseCases.AddItem(inventory, fish);
        
        Assert.IsTrue(Mathf.Approximately(hero.Damage, 20f));
        Assert.IsTrue(Mathf.Approximately(hero.Speed, 30f));
        Assert.IsTrue(Mathf.Approximately(hero.HitPoints, 30f));
    }
    #endregion
}
