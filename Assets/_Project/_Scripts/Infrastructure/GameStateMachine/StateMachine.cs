using System;
using System.Collections.Generic;

namespace Infrastructure.GameStateMachine
{
public class StateMachine
{
    IEnterable _currentState;

    Dictionary<Type, IEnterable> _states;


    public void Initialize( params IEnterable[] states )
    {
        _states = new Dictionary<Type, IEnterable>( states.Length );

        foreach ( IEnterable state in states )
        {
            // _states.Add( typeof(state), state );
            _states.Add( state.GetType(), state );
        }
    }

    public void SwitchState<TState>( )
        where TState : IEnterable
    {
        TryExitPreviousState();

        SetNewState<TState>();

        TryEnterNewState();
    }

    void TryExitPreviousState( )
    {
        if ( _currentState is IExitable exitable )
        {
            exitable.OnExit(); //при первом выходе не будет null?
        }
    }

    void TryEnterNewState( )
    {
        if ( _currentState is IEnterable enterable )
        {
            enterable.OnEnter();
        }
    }

    void SetNewState<TState>( )
        where TState : IEnterable
    {
        TState newState = GetState<TState>();
        _currentState = newState;
    }


    TState GetState<TState>( )
        where TState : IEnterable
    {
        return ( TState ) _states[ typeof( TState ) ];
    }

}
}