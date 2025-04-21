using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public sealed class HeroInventoryDebug : MonoBehaviour
{
    [Inject, ShowInInspector] private HeroInventory heroInventory;
}