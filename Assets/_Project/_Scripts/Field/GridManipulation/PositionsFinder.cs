using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field.GridManipulation
{
public class PositionsFinder //для генерации smallPreview и стартовых. для перемещения мелкого после перекрытия - др скрипт - туда передавать ItemSizeType?[]
{
    //!! c# List _someList хранится в одном Core классе. как передать на класс выше ImmutableList, но который бы всегда ссылался на _someList gpt выдал муть. https://www.perplexity.ai/search/d8e4b9f0-58e7-4d55-b747-64c372f2747c?s=u
    //!! тупо передай IReadOnlyList
    public IList ForbiddenToSpawn { get; set; } //ForbiddenToSpawn big or small?
    // public IEnumerable<Vector2Int> ReservedCell { get; set; }

    // readonly IReadOnlyList<Ball> - нет, т.к. там вообще array _itemList;
    //public static System.Collections.ObjectModel.ReadOnlyCollection<T>    Array.AsReadOnly(T[] array);
    //readonly IList<int> _itemList2;
    Grid<Ball> _grid;

    readonly IList _itemList; //=TCell[] Cells
    public PositionsFinder( IList itemList ) =>
        _itemList = itemList;





    public List<Vector2Int> GetEmptyCells123( )
    {
        // Grid<Ball>.Filter func2 = GetFilterFunc( ItemSizeType.Big );
        // Grid<Ball>.Filter func23 = NullOrImmature;
        Grid<Ball>.Filter func = IsItemRipped;
        return _grid.GetCoordsOfFilteredItems( func ).ToList(); //обращается к grid по ссылке
    }

    //Func<Ball, bool> Cannot cast expression of type 'System.Func<Field.ItemGeneration.FieldItem.Ball,bool>' to type 'Grid<Ball>.Filter'
    Grid<Ball>.Filter IsItemOfType( ItemRipeType value ) => ball => ball.RipedType == value;


    Grid<Ball>.Filter GetFilterFunc( Ball ball, ItemRipeType itemRipeType ) //всё это нагородил чтобы не передавать в этот класс ссылку на Grid<TCell>  TCell[] Cells. чтобы Cells никто не мог изменить
    {
        Grid<Ball>.Filter dele = IsItemRipped;
        Func<Ball, bool> func = IsItemRipped;
        return dele;
    }

    bool IsItemRipped( Ball ball )
    {
        return ball.RipedType == ItemRipeType.Big;
    }

    bool NullOrImmature( Ball ball )
    {
        if ( ball == null )
            return true;

        return ball.RipedType == ItemRipeType.Small;
    }
}
}