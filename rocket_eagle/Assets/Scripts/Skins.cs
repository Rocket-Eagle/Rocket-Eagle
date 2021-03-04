using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins
{
    private Image preview;
    private uint cost;
    private bool isPurchased;

    public Skins(Image thePreview, uint theCost)
    {
        preview = thePreview;
        cost = theCost;
        isPurchased = false;
    }

    public Object GetPreviewImage()
    {
        return preview;
    }
}
