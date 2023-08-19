using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{

[ InitializeOnLoad ]
static class MultiSelectionHistory
{ //https://github.com/mminer/selection-history-navigator/blob/master/SelectionHistoryNavigator.cs

    //рабочее было в Idle Tycoon Sim
    static SelectionHistoryData _activeSelection;


    static bool _ignoreNextSelectionChangedEvent;
    static EditorWindow _lastWindow;

    public static Stack<SelectionHistoryData> PreviousSelections => _previousSelections;
    //отрубил инспекцию кот предлагала Prop => _field в более многословную. поаккуратнее

    static readonly Stack<SelectionHistoryData> _nextSelections = new Stack<SelectionHistoryData>();
    static readonly Stack<SelectionHistoryData> _previousSelections = new Stack<SelectionHistoryData>();

    static MultiSelectionHistory()
    {
        Selection.selectionChanged += OnSelectionChange;
    }

    static void OnSelectionChange()
    {
        if ( _ignoreNextSelectionChangedEvent )
        {
            //while click Back or Forward
            _ignoreNextSelectionChangedEvent = false;
            return;
        }

        if ( _activeSelection != null )
        {
            _previousSelections.Push( _activeSelection );
        }

        _activeSelection = new SelectionHistoryData( Selection.objects, EditorWindow.focusedWindow );
        _nextSelections.Clear();
    }



    const string _backMenuLabelHotkey = " %[";
    const string _forwardMenuLabelHotkey = " %]";

    const string _backMenuLabel = "Edit/Selection/Multi Back" + _backMenuLabelHotkey;
    const string _forwardMenuLabel = "Edit/Selection/Multi Forward" + _forwardMenuLabelHotkey;

    [ MenuItem( _backMenuLabel ) ]
    static void Back()
    {
        if ( _activeSelection != null )
        {
            SelectionHistoryData next = new SelectionHistoryData( Selection.objects, EditorWindow.focusedWindow );
            _nextSelections.Push( next );
        }

        SelectionHistoryData prev = _previousSelections.Pop();
        Selection.objects = prev.Selected;
        OpenWindow( prev.OpenedWindow ); 

        _ignoreNextSelectionChangedEvent = true;
    }


    [ MenuItem( _forwardMenuLabel ) ]
    static void Forward()
    {
        if ( _activeSelection != null )
        {
            SelectionHistoryData prev = new SelectionHistoryData( Selection.objects, EditorWindow.focusedWindow );
            _previousSelections.Push( prev );
        }

        SelectionHistoryData next = _nextSelections.Pop();
        Selection.objects = next.Selected;
        OpenWindow( next.OpenedWindow ); 

        _ignoreNextSelectionChangedEvent = true;
    }

    static void OpenWindow( EditorWindow nextOpenedWindow )
    {
        EditorWindowHelper.GetWindow( nextOpenedWindow );
    }

    [ MenuItem( _backMenuLabel, true ) ]
    static bool ValidateBack() => _previousSelections.Count > 0;

    [ MenuItem( _forwardMenuLabel, true ) ]
    static bool ValidateForward() => _nextSelections.Count > 0;


}
}