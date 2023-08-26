using UnityEngine;
using UnityEngine.UI;

namespace Field
{
public class CoordinatesNumberUi : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] int _startCountWith = 1;

    public void SetUiSymbol( string symbol )
    {
        _text.text =  symbol;
    }

    public void SetUiSymbol( int symbol )
    {
        _text.text = ( _startCountWith + symbol ).ToString();
    }
}
}
