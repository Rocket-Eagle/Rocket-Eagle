using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PurchaseSkin : MonoBehaviour
{
    [SerializeField] Skins[] possibleSkins;

    private int currentSelected;

    public void PreformPurchase()
    {

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
