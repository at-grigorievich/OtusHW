using ATG.Items;
using ATG.Items.Inventory;
using NUnit.Framework;

[TestFixture]
public class CraftingTests
{
    [Test]
    public void ExampleTest()
    {
        //Assert.Fail();
        Assert.Pass();
    }

    [Test]
    public void CraftTest()
    {
        Inventory inventory = new();
        Item wood = new("wood");
        Item stone = new("stone");
        
        InventoryUseCases.AddItem(inventory, wood.Clone());
        InventoryUseCases.AddItem(inventory, wood.Clone());
        InventoryUseCases.AddItem(inventory, stone.Clone());
        
        Item axe = new ("axe");
        
        CraftingUseCases.Craft(inventory, axe, wood, wood, stone);
        
        bool hasItem = InventoryUseCases.HasItem(inventory, axe);
        Assert.IsTrue(hasItem);
        Assert.IsFalse(InventoryUseCases.HasItem(inventory, wood));
        Assert.IsFalse(InventoryUseCases.HasItem(inventory, stone));
    }
}
