using Signals;
using UnityEngine;
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
    }
}