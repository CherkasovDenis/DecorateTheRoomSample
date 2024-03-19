using BlackKakadu.DecorateTheRoom.Data;
using BlackKakadu.DecorateTheRoom.Gameplay.Factories;
using BlackKakadu.DecorateTheRoom.Gameplay.Models;
using BlackKakadu.DecorateTheRoom.Gameplay.Presenters;
using BlackKakadu.DecorateTheRoom.Gameplay.Views;
using BlackKakadu.DecorateTheRoom.Saves;
using BlackKakadu.DecorateTheRoom.Services;
using BlackKakadu.DecorateTheRoom.ThirdParty;
using BlackKakadu.DecorateTheRoom.Ui.Creators;
using BlackKakadu.DecorateTheRoom.Ui.Models;
using BlackKakadu.DecorateTheRoom.Ui.Presenters;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace BlackKakadu.DecorateTheRoom
{
    public class DecorateTheRoomLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameData _gameData;

        [SerializeField]
        private VisualData _visualData;

        [SerializeField]
        private PurchasesData _purchasesData;

        [SerializeField]
        private AdData _adData;

        [SerializeField]
        private Transform _decorParent;

        [SerializeField]
        private Switch _menuSwitch;

        [SerializeField]
        private RoomView _roomView;

        [SerializeField]
        private MenuView _menuView;

        [SerializeField]
        private AdPauseView _adPauseView;

        [SerializeField]
        private Switch _trashSwitch;

        [SerializeField]
        private Button _undoButton;

        [SerializeField]
        private Button _moreGamesButton;

        [SerializeField]
        private Button _screenshotButton;

        [SerializeField]
        private ScreenshotService _screenshotService;

        [SerializeField]
        private SoundService _soundService;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>(Lifetime.Scoped);


            // Data:
            builder.RegisterInstance(_gameData);
            builder.RegisterInstance(_visualData);
            builder.RegisterInstance(_purchasesData);
            builder.RegisterInstance(_adData);


            // Services:
            builder.RegisterInstance(_screenshotService);
            builder.RegisterInstance(_soundService);
            builder.UseEntryPoints(Lifetime.Scoped, services =>
            {
                services.Add<AdService>().AsSelf();
                services.Add<PurchasesService>().AsSelf();
                services.Add<UnlockContentService>().AsSelf();
                services.Add<MoreGamesService>().AsSelf().WithParameter(_moreGamesButton);
                services.Add<SaveService>().AsSelf();
            });


            // Gameplay:
            builder.RegisterInstance(_roomView);

            builder.Register<UnlockContentModel>(Lifetime.Scoped);
            builder.Register<SaveModel>(Lifetime.Scoped);

            builder.Register<RoomModel>(Lifetime.Scoped);
            builder.Register<UndoModel>(Lifetime.Scoped);

            builder.UseEntryPoints(Lifetime.Scoped, presenters =>
            {
                presenters.Add<RoomPresenter>();
                presenters.Add<DecorPresenter>();
                presenters.Add<UndoPresenter>().WithParameter(_undoButton);
            });

            builder.Register<DecorFactory>(Lifetime.Scoped).WithParameter(_decorParent);
            builder.RegisterFactory<DecorElementData, int, DecorView>
                (container => container.Resolve<DecorFactory>().CreateDecorElement, Lifetime.Scoped);


            // Ui:
            builder.RegisterInstance(_menuView);
            builder.RegisterInstance(_adPauseView);

            builder.Register<MenuWindowModel>(Lifetime.Scoped);
            builder.Register<PagesModel>(Lifetime.Scoped);
            builder.Register<CategoriesModel>(Lifetime.Scoped);
            builder.Register<MenuButtonsModel>(Lifetime.Scoped);
            builder.Register<AdPauseModel>(Lifetime.Scoped);
            builder.Register<PurchasesModel>(Lifetime.Scoped);

            builder.UseEntryPoints(Lifetime.Scoped, presenters =>
            {
                presenters.Add<MenuWindowPresenter>().WithParameter(_menuSwitch);
                presenters.Add<PagesPresenter>();
                presenters.Add<MenuButtonsPresenter>();
                presenters.Add<CategoriesPresenter>();
                presenters.Add<TrashPresenter>().WithParameter(_trashSwitch);
                presenters.Add<ScreenShotPresenter>().WithParameter(_screenshotButton);
                presenters.Add<UnlockButtonsPresenter>();
                presenters.Add<AdPausePresenter>();
            });

            builder.Register<MenuCreator>(Lifetime.Scoped);


            // Third Party Sdk:
            builder.Register<DummySdk>(Lifetime.Scoped);
        }
    }
}