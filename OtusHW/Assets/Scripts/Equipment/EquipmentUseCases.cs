using System;
using ATG.OtusHW.Inventory;

namespace ATG.Items.Equipment
{
    public static class EquipmentUseCases
    {
        public static bool HasItem(Equipment equipment, Item item, bool findByRef = false)
        {
            if (item.CanEquip() == false) return false;
            
            if(item.TryGetComponent(out HeroEquipmentComponent component) == false) return false;
            
            EquipType equipType = component.Tag;
            
            if(equipment.Items.TryGetValue(equipType, out var equippedItem) == false) return false;

            return findByRef == false
                ? equippedItem.Id == item.Id
                : ReferenceEquals(item, equippedItem);
        }

        public static bool TryGetItemByTag(Equipment equipment, EquipType equipType, out Item item)
        {
            if (equipment.Items.TryGetValue(equipType, out var equipmentItem) == false)
            {
                item = null;
                return false;
            }
            
            item = equipmentItem;
            return true;
        }
        
        public static void TakeOnItem(Equipment equipment, Item item)
        {
            if(item.CanEquip() == false) return;
            
            if(item.TryGetComponent(out HeroEquipmentComponent component) == false) return;

            if (component.Tag == EquipType.None)
            {
                throw new Exception($"Try to take on {item.Id} with empty equipment tag.");
            }
            
            equipment.Items.Add(component.Tag, item);
            equipment.ItemsDebug.Add(item);
            
            equipment.NotifyItemTakeOn(item);
        }

        public static Item TakeOffItemByTag(Equipment equipment, EquipType equipType)
        {
            if (TryGetItemByTag(equipment, equipType, out var item) == false) return null;
            
            return TakeOffItem(equipment, item);
        }
        
        public static Item TakeOffItem(Equipment equipment, Item item, bool findByRef = false)
        {
            if(item.CanEquip() == false) return null;
            
            if(item.TryGetComponent(out HeroEquipmentComponent component) == false) return null;
            
            if (component.Tag == EquipType.None)
            {
                throw new Exception($"Try to take off {item.Id} with empty equipment tag.");
            }
            
            if (equipment.Items.ContainsKey(component.Tag) == true)
            {
                Item result = equipment.Items[component.Tag];

                if (findByRef == false)
                {
                    if (result.Id != item.Id) return null;
                }
                else
                {
                    if(ReferenceEquals(result, item) == false) return null;
                }
                
                equipment.Items.Remove(component.Tag);
                equipment.ItemsDebug.Remove(result);
                
                equipment.NotifyItemTakeOff(result);

                return result;
            }

            return null;
        }
    }
}