using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using UnityEngine;

namespace Infrastructure
{
public class Bootstrapper : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    //public StateMachine StateMachine { get; } = new StateMachine( );

    void Start( )
    {
        StateMachine = new StateMachine();

        StateMachine.Initialize(
            // new GameplayState GameLoopState( StateMachine ),
            // new PlayerTurn( StateMachine ),
            // new BlockInputState( StateMachine ),
            new InitialState( StateMachine ));

        StateMachine.SwitchState<InitialState>();

        DontDestroyOnLoad( this );
    }
}
}