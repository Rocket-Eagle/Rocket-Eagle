using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PurchaseSkin : MonoBehaviour
{

    [SerializeField] public Skins[] possibleSkins;
    [SerializeField] public Text bcWalletDisplay;
    [SerializeField] private uint birdCoinWallet = 0;

    //var to keep track of whether we are currently loading data
    private bool onLoad = false;
    private int currentSelected;

    /*
     * load in the data on start
     */
    public void Start()
    {
        //TEMP: MAKE A SKIN SO THERE IS SOMETHING TO LOAD
        /*
        Skins tempSkin = new Skins("bird", 100, false);
        Skins[] temp = new Skins[1];
        temp[0] = tempSkin;
        possibleSkins = temp;
        SaveGameData.SaveSkins(possibleSkins);
        */
        //TEMP: MAKE A coin SO THERE IS SOMETHING TO LOAD
        //SaveGameData.SavePlayerCoin(250);

        onLoad = true;

        //load in the skins data to know what has already been purchased
        possibleSkins = SaveGameData.LoadSkins();
        if(possibleSkins == null)
        {
            //this is either an error, or a onetime initialization
            Debug.LogError("Error, loaded in zero skin objects!");
            
        }

        //load in the players BirdCoin wallet to know what kind of money they have
        birdCoinWallet = SaveGameData.LoadPlayerCoin();

        onLoad = false;
    }

    /*
     * try to preform purchase on the currently selected item
     * checks to see if the skin is avaliable (not already purchased) and that the player has enough money
     * 
     */
    public void PreformPurchase()
    {
        //first check if the skin has already been purchased or not
        if (!possibleSkins[currentSelected].GetIsPurchased())
        {
            //check if there is enough birdcoin
            if (possibleSkins[currentSelected].GetCost() <= birdCoinWallet)
            {
                //purchase is valid
                unlockSkin(currentSelected);

                //update the users birdcoin
                removeBirdCoin(possibleSkins[currentSelected].GetCost());
            }
            else
            {
                //display message that the player doesn't have enough money

            }
        }
    }

    /*
     * remove birdcoin from the players wallet
     * amount should be positive
     */
    public void removeBirdCoin(uint amount)
    {
        if (amount < 0)
        {
            Debug.LogError("ERROR, removeBirdCoin(amount) shoud be positive");
        }
        else
        {
            birdCoinWallet -= amount;
            updateBirdcoinDisplay();
        }
    }

    /*
     * add birdcoin to the players wallet
     * amount must be positive
     */
    public void addBirdCoin(uint amount)
    {
        if(amount < 0)
        {
            Debug.LogError("ERROR, addBirdCoin(amount) shoud be positive");
        }
        else
        {
            birdCoinWallet += amount;
            updateBirdcoinDisplay();
        }
    }

    /*
     * update the 'BirdCoinCount' prefab on the customization menu's page
     */
    private void updateBirdcoinDisplay()
    {
        bcWalletDisplay.text = birdCoinWallet.ToString();

        if (!onLoad)
        {
            //I AM UNSURE IF THE UPDATEBIRDCOIDISPLAY() WILL CAUSE PROBLEMS IF THE SCENE IS NOT LOADED
            //IF THIS IS THE CASE, MOVE THIS SAVE LINE TO INSIDE TEH ADD AND REMOVE BIRDCOIN()'S
            SaveGameData.SavePlayerCoin(birdCoinWallet);
        }
    }

    /*
     * go to the birdController class and update the Image that it is using
     */
    private void updatePlayerSkin()
    {
        
    }

    /*
     * unlock the skin in the array at spot 'theSkinIndex'
     */
    public void unlockSkin(int theSkinIndex)
    {
        //unlock the skin
        possibleSkins[theSkinIndex].unlockSkin();

        //save the game data
        SaveGameData.SaveSkins(possibleSkins);
    }

    /*
     * move to the previous preview image
     */
    public void CycleLeft()
    {
        Debug.Log("Going right from index:" + currentSelected);
        if (currentSelected == 0)
        {
            currentSelected = possibleSkins.Length;
        }

        currentSelected--;
        Debug.Log("To:" + currentSelected);

        //reload the preview image
        ReloadPreviewImage();
    }

    /*
     * move to the next preview image
     */
    public void CycleRight()
    {
        Debug.Log("Going right from index:" + currentSelected);
        if (currentSelected == possibleSkins.Length - 1)
        {
            currentSelected = 0;
        }
        else
        {
            currentSelected++;
        }
        Debug.Log("To:" + currentSelected);

        //reload the preview image
        ReloadPreviewImage();
    }

    /*
     * change the image in the preview window
     */
    private void ReloadPreviewImage()
    {
        GetComponent<Image>().sprite = possibleSkins[currentSelected].GetPreviewImage() as Sprite;
    }
}
