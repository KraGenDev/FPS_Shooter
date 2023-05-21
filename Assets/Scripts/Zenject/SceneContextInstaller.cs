using UnityEngine;
using Zenject;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;
    public override void InstallBindings()
    {
        Container.Bind<PlayerFacade>().FromComponentOn(_player).AsCached();
    }
}