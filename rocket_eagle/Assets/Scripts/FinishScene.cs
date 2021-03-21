using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScene : MonoBehaviour
{
    [SerializeField] Text birdCoinMessage;

    private const string BIRDCOIN_MESSAGE = "Y o u   W o n   {0}   B i r d C o i n!";

    // Start is called before the first frame update
    void Start()
    {
        //grab the updateCoin.save file to see how much Birdcoin the player just won
        uint bc = SaveGameData.LoadUpdateCoin();

        birdCoinMessage.text = string.Format(BIRDCOIN_MESSAGE, bc);
    }
}
