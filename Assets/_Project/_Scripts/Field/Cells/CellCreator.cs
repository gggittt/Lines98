using UnityEngine;

namespace Field.Cells
{
public class CellCreator : MonoBehaviour
{
    [ SerializeField ] Cell _cellPrefab;
    [ SerializeField ] BoundCoordinatesCreator _boundCreator;
    ClickManager _clickManager;



    //start at top left position
    public Grid<Cell> CreateBoard( ClickManager clickManager, Vector2Int size )
    {
        _clickManager = clickManager;

        Grid<Cell> cellGrid = new Grid<Cell>( size.x, size.y );

        Vector3 cellLocalScale = _cellPrefab.transform.localScale;
        _boundCreator.CreateBounds( size.x, size.y, cellLocalScale );

        for ( int y = 0; y < size.y; y++ )
        for ( int x = 0; x < size.x; x++ )
        {
            CreateTile( x, y );
        }

        void CreateTile( int x, int y )
        {
            Cell cell = Instantiate( _cellPrefab, transform );
            cell.Init( new Vector2Int( x, y ) );
            cell.Clicked += _clickManager.OnCellClick;
            cellGrid.Set( x, y, cell );

            SetPosition();

            void SetPosition( )
            {
                //const int worldToLocalCoefficient = -1;
                float yPos = - y * cellLocalScale.y;
                Vector3 shift = new Vector3( x * cellLocalScale.x, yPos );

                cell.transform.position = transform.position + shift;
            }
        }

        return cellGrid;
    }


}
}