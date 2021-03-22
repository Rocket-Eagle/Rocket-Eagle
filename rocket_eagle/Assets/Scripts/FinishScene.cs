using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    /*
     * go to the main menu from the finish game scene
     */
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void playAgain()
    {
        //TODO: find a way to dynamically reload this current scene
        string scene = SaveGameData.LoadSelectedLevel();
        SceneManager.LoadScene(scene);
    }
}
