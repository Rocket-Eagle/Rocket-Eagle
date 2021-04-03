using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public AudioClip audioClip;

    public string Name;
    public string description;

    public Sprite Visual;
}
