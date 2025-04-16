using UI;
using VContainer.Unity;

namespace ATG.RealtimeChests
{
    public sealed class ChestPresenter: IStartable
    {
        private readonly ChestView _view;
        private readonly Chest _model;

        public ChestPresenter(ChestConfig chestConfig, ChestView view)
        {
            _model = chestConfig.Create();
            _view = view;
        }

        public void Start()
        {
            _view.ShowMetaData(_model.Meta);
        }
    }
}