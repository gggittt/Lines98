using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
public class ProjectInstaller : MonoInstaller
{

    public override void InstallBindings( )
    {
        Debug.Log( $"<color=cyan> {nameof( ProjectInstaller )} </color> InstallBindings из префаба" );

        Container.Bind<ProjectContextTest>().AsSingle().NonLazy();
    }
}
}