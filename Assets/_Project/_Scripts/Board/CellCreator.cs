using UnityEngine;

namespace _Project._Scripts.Board
{
public class CellCreator : MonoBehaviour
{
    [ SerializeField ] Cell _cellPrefab;
    [ SerializeField ] BoundCoordinatesCreator _boundCreator;


    //start at top left position
    public Grid<Cell> CreateBoard( int xSize = 9, int ySize = 9 )
    {

        Grid<Cell> cellGrid = new Grid<Cell>( xSize, ySize );

        Vector3 cellLocalScale = _cellPrefab.transform.localScale;
        _boundCreator.CreateBounds( xSize, ySize, cellLocalScale );

        for ( int y = 0; y < ySize; y++ )
        for ( int x = 0; x < xSize; x++ )
        {
            CreateTile( x, y );
        }

        void CreateTile( int x, int y )
        {
            Cell cell = Instantiate( _cellPrefab, transform );
            cell.LocalCoord = new Vector2Int( x, y );
            cell.name = cell.LocalCoord + " " + nameof( Cell );
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