using System;
using ATG.RealtimeChests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class ChestView: MonoBehaviour
    {
        [SerializeField] private TMP_Text chestNameOutput;
        [SerializeField] private TMP_Text chestAvailableOutput;
        [SerializeField] private Image chestResourceIcon;
        [Space(10)]
        [SerializeField] private Button openChestBtn;

        public event Action OnChestOpenClicked;

        public void ShowMetaData(ChestMetaData meta)
        {
            chestNameOutput.text = meta.Name;
            chestResourceIcon.sprite = meta.Sprite;
        }
    }
}