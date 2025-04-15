using System;
using ATG.Items.Equipment;
using UnityEngine;

namespace ATG.Items.Inventory
{
    public static class InventoryToEquipmentUseCases
    {
        public static void EquipItem(Inventory inventory, Equipment.Equipment equipment, Item item, 
            bool equipByRef = true)
        {
            var equipped = InventoryUseCases.RemoveItem(inventory, item, removeByRef: equipByRef);

            if (equipped == null)
                throw new NullReferenceException($"Can't remove item with id {item.Id}");

            if (item.TryGetComponent(out HeroEquipmentComponent equipmentComponent) == true)
            {
                if (EquipmentUseCases.TryGetItemByTag(equipment, equipmentComponent.Tag, out Item equipmentItem) ==
                    true)
                {
                    UnequipItem(inventory, equipment, equipmentItem);
                }
            }
            
            EquipmentUseCases.TakeOnItem(equipment, equipped);
        }

        public static void UnequipItem(Inventory inventory, Equipment.Equipment equipment, Item item)
        {
            Item unequippedItem = EquipmentUseCases.TakeOffItem(equipment, item);
            
            if (unequippedItem == null)
                throw new NullReferenceException($"Can't take off item with id {item.Id}");
            
            InventoryUseCases.AddItem(inventory, unequippedItem);
        }
    }
    
    public class InventoryToEquipmentProvider: IDisposable
    {
        private readonly Inventory _inventory;
        private readonly Equipment.Equipment _equipment;

        private readonly InventoryView _inventoryView;
        private readonly EquipmentSetView _equipmentView;

        public InventoryToEquipmentProvider(Inventory inventory, Equipment.Equipment equipment, 
            InventoryView inventoryView, EquipmentSetView equipmentView)
        {
            _inventory = inventory;
            _equipment = equipment;
            
            _inventoryView = inventoryView;
            _equipmentView = equipmentView;
            
            _inventoryView.OnEquipClicked += OnEquipClicked;
            _equipmentView.OnItemTakeOffClicked += OnTakeOffClickedClicked;
        }

        private void OnEquipClicked(Item obj) => 
            InventoryToEquipmentUseCases.EquipItem(_inventory, _equipment, obj);
        
        private void OnTakeOffClickedClicked(Item obj) => 
            InventoryToEquipmentUseCases.UnequipItem(_inventory, _equipment, obj);
        
        public void Dispose()
        {
            _inventoryView.OnEquipClicked -= OnEquipClicked;
            _equipmentView.OnItemTakeOffClicked -= OnTakeOffClickedClicked;
        }
    }
}