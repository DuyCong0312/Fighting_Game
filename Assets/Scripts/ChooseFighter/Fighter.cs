using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fighter
{
    [SerializeField] private Sprite fighterImage;
    [SerializeField] private string fighterName;

    public Sprite FighterImage => fighterImage;
    public string FighterName => fighterName;
}
