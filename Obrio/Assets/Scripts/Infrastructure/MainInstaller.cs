using Signals;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        DeclareSignals();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<CollisionWithObstacle>();
        Container.DeclareSignal<PickUpCoin>();
        Container.DeclareSignal<LevelFailing>();
        Container.DeclareSignal<LevelStarting>();
        Container.DeclareSignal<PlayerJump>();
        Container.DeclareSignal<PlayerTryJump>();
        Container.DeclareSignal<PlayerLanded>();
        Container.DeclareSignal<UpdateScore>();
        Container.DeclareSignal<UpdateSpeed>();
    }
}