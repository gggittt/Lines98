using System;
using System.Collections.Generic;
using Field;
using Field.ItemGeneration;
using Field.ItemGeneration.FieldItem;
using Infrastructure;
using UnityEngine;
using Zenject;

public class SceneMainInstaller : MonoInstaller
{
    [ SerializeField ] GameData _data;

    public override void InstallBindings( )
    {
        Debug.Log($"<color=cyan> {nameof(SceneMainInstaller)} </color> InstallBindings");

        // Container.Bind<IMatchReaper>().To<MatchReaper>().FromNew().AsSingle();
        // //мне интуитивно проще Bind<Xxx>().To<IXxx>()

        // Container.Bind<MatchReaper>().AsSingle().NonLazy(); //без интерфейса, тупо класс
        Container.Bind<SceneContextTest>().AsSingle().NonLazy();
        // get as https://youtu.be/jVFXnDd40CE?t=722 [Inject] void Construct(MatchReaper reaper){}
        // Container.Bind<ObjectsPoolMono>().AsSingle().NonLazy(); //без интерфейса, тупо класс
        //.FromNew() = и так по умолчанию
        // Container.BindInterfacesAndSelfTo<MatchReaper>().FromNew().AsSingle().NonLazy(); //все реализуемые интерфейсы параметра
        // Container.Bind<MyPool>().To<>().FromNew().AsSingle(); //IPool


        // Container.Bind( typeof( ObjectsPoolGeneric<> ) )
        //    .ToSelf()
        //    .FromMethod( ObjectsPoolGeneric )
        //   // .FromInstance(new ObjectsPoolGeneric<Ball>())
        //    .AsSingle();

    }

    ObjectsPoolGeneric<Ball> ObjectsPoolGeneric( InjectContext context )
    {//chatGPT
        Ball prefab = context.Container.Resolve<Ball>();
        int prewarmObjectsAmount = 10;
        return new ObjectsPoolGeneric<Ball>( prefab, prewarmObjectsAmount );
    }

}