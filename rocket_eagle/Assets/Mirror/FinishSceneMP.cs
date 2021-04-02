using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class FinishSceneMP : NetworkBehaviour
{
    [SerializeField] Text winnerMessage;

    private string winner = "Y o u   W o n   t h e   R a c e!";

    [SyncVar]
    private string winnerName = "";

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //grab the updateCoin.save file to see how much Birdcoin the player just won
        uint bc = SaveGameData.LoadUpdateCoin();

        winnerName = Room.getWinnerName();

        if( winnerName.Equals("") )
            winner = "Y o u   L o s t   t h e   R a c e";

        winnerMessage.text = winner;
    }

    public void NextMatch()
    {
        Room.NextMatch();
    }


    public void EndSession()
    {
        Room.EndSession();
    }
}
