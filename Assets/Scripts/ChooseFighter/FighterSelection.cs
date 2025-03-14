using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FighterSelection : MonoBehaviour
{
    [SerializeField] private List<Fighter> fighterList = new List<Fighter>();   

    [SerializeField] private TextMeshProUGUI fighterName;
    [SerializeField] private Image fighterImage;

    public void ChooseFighter(int fighterIndex)
    {
        fighterImage.sprite = fighterList[fighterIndex].FighterImage;
        fighterName.text = fighterList[fighterIndex].FighterName;
    }
}
