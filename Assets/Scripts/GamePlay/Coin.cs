using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount = 0;

    private void Update()
    {
        if (GridManager.Instance != null 
            && GridManager.Instance.activeLevel != null
            && GridManager.Instance.isWin == true)
        {
            GridManager.Instance.isWin = false;
            coinCount += GridManager.Instance.activeLevel.coin;
        }
        coinText.text = coinCount.ToString() ;
    }
}
