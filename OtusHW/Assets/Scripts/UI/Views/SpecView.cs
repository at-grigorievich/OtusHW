using Core;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public sealed class SpecView: MonoBehaviour
    {
        [SerializeField] private TMP_Text statOutput;

        private SpecViewModel _viewModel;

        public void Init(SpecViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Show()
        {
            statOutput.text = $"{_viewModel.Tag}: {_viewModel.Value}";
        }
    }
}