using Zenject;

namespace LogicAI
{
    public class MoveAIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMoveAIExecutor>().To<MoveAIExecutor>().AsSingle().NonLazy();
        }
    }
}

