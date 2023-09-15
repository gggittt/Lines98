using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Field.ItemGeneration.FieldItem;
using UnityEngine;

namespace Field
{
public class PositionsFinder //для генерации smallPreview и стартовых. для перемещения мелкого после перекрытия - др скрипт - туда передавать ItemSizeType?[]
{
    public IList ForbiddenToSpawn { get; set; } //ForbiddenToSpawn big or small?
    // public IEnumerable<Vector2Int> ReservedCell { get; set; }

    // readonly IReadOnlyList<Ball> - нет, т.к. там вообще array _itemList;
    //public static System.Collections.ObjectModel.ReadOnlyCollection<T>    Array.AsReadOnly(T[] array);
    //readonly IList<int> _itemList2;

    readonly IList _itemList; //=TCell[] Cells
    public PositionsFinder( IList itemList ) =>
        _itemList = itemList;

    public List<int> GetCellsWithoutItemsIndexes( ) //getNullCells //immatureItemPosition
    {
        List<int> freeCellIndexes = new List<int>(); //мб HashSet?

        for ( int i = 0; i < _itemList.Count; i++ )
        {
            if ( _itemList[ i ] != null )
                continue;

            //if ( ForbiddenToSpawn.Contains( _itemList[ i ] ) )
            //    continue;

            freeCellIndexes.Add( i );
        }

        Console.WriteLine( "Console.WriteLine" );

        return freeCellIndexes;
    }





    Grid<Ball> _grid;
    public List<Vector2Int> GetEmptyCells( )
    {
        // Grid<Ball>.Filter func2 = GetFilterFunc( ItemSizeType.Big );
        // Grid<Ball>.Filter func23 = NullOrImmature;
        Grid<Ball>.Filter func = IsItemRipped;
        return _grid.GetCoordsOfFilteredItems( func ).ToList(); //обращается к grid по ссылке
    }

    //Func<Ball, bool> Cannot cast expression of type 'System.Func<Field.ItemGeneration.FieldItem.Ball,bool>' to type 'Grid<Ball>.Filter'
    Grid<Ball>.Filter IsItemOfType( ItemSizeType value ) => ball => ball.ItemSizeType == value;


    Grid<Ball>.Filter GetFilterFunc( Ball ball, ItemSizeType itemSizeType ) //всё это нагородил чтобы не передавать в этот класс ссылку на Grid<TCell>  TCell[] Cells. чтобы Cells никто не мог изменить
    {
        Grid<Ball>.Filter dele = IsItemRipped;
        Func<Ball, bool> func = IsItemRipped;
        return dele;
    }

    bool IsItemRipped( Ball ball ) //maturated. Ripped=разорванный
    {
        return ball.ItemSizeType == ItemSizeType.Big;
    }

    bool NullOrImmature( Ball ball )
    {
        if ( ball == null )
            return true;

        return ball.ItemSizeType == ItemSizeType.Small;
    }
}
}