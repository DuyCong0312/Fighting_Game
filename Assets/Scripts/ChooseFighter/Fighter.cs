using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fighter
{
    [SerializeField] private Sprite fighterSprite;
    [SerializeField] private string fighterName;
    [SerializeField] private RuntimeAnimatorController fighterAnimator;

    public Sprite FighterSpite => fighterSprite;
    public string FighterName => fighterName;
    public RuntimeAnimatorController FighterAnimator => fighterAnimator;
}
