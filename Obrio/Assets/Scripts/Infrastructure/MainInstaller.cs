using Logic;
using Logic.Generating;
using Signals;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        // Container.Bind<SignalBus>().AsSingle();
        DeclareSignals();

        Container.Bind<Player>().AsSingle();
        Container.Bind<ILevelCreator>().To<RandomLevelCreator>().AsSingle();
        Container.Bind<IGameSpeedController>().To<GameSpeedController>().AsSingle();
    }


    private void DeclareSignals()
    {
        Container.DeclareSignal<CollisionWithObstacle>();
        Container.DeclareSignal<StartMoving>();
        Container.DeclareSignal<PickUpCoin>();
        Container.DeclareSignal<LevelFailing>();
        Container.DeclareSignal<LevelStarting>();
        Container.DeclareSignal<LevelRestarting>();
        Container.DeclareSignal<PlayerJump>();
        Container.DeclareSignal<PlayerTryJump>();
        Container.DeclareSignal<PlayerLanded>();
        Container.DeclareSignal<UpdateScore>();
        Container.DeclareSignal<UpdateSpeed>();
        Container.DeclareSignal<SpeedUpdated>();
        Container.DeclareSignal<SpawnTile>();
    }
}