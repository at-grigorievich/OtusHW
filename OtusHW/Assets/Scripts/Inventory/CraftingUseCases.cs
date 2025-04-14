namespace ATG.Items.Inventory
{
    public static class CraftingUseCases
    {
        public static void Craft(Inventory inventory, Item resultItem, params Item[] ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                InventoryUseCases.RemoveItem(inventory, ingredient);
            }
            
            InventoryUseCases.AddItem(inventory, resultItem);
        }
    }
}