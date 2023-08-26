using UnityEngine;

namespace _Project._Scripts.Field
{
public class Cell : MonoBehaviour
{
    public Vector2Int LocalCoord { get; set; }
    public bool HasItem { get; } //для анализа логики при клике. для проверки линий - см Grid<Item>

    public event System.Action<Vector2Int> Clicked;

    void OnMouseUpAsButton()
    {
        Clicked?.Invoke( LocalCoord );

    }



}
}
