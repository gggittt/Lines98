using UnityEngine;
using Zenject;

namespace Infrastructure.GameStateMachine.States
{
public class InitialState : IEnterable, IExitable
{
    readonly StateMachine _stateMachine;
    SceneLoader _sceneLoader;

    public InitialState( StateMachine stateMachine )
    {
        _stateMachine = stateMachine;
    }

    [ Inject ] void Construct( SceneLoader sceneLoader )
    {
        _sceneLoader = sceneLoader;
    }

    public void OnEnter( )
    {
        Debug.Log( $"{GetType().Name} Entered" );
        _stateMachine.SwitchState<InitialState>();
    }

    public void OnExit( )
    {
        Debug.Log( $"{GetType().Name} Exited" );
    }

}
}