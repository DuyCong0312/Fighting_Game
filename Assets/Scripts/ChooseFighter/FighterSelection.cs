using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FighterSelection : MonoBehaviour
{
    [SerializeField] private List<Fighter> fighterList = new List<Fighter>();   

    [SerializeField] private TextMeshProUGUI fighterName;
    [SerializeField] private Image fighterImage;
    [SerializeField] private int selectedFighterIndex = 0;

    private void Start()
    {
        if (fighterList.Count > 0)
        {
            selectedFighterIndex = fighterList.Count - 1;
            ChooseFighter(selectedFighterIndex);
        }
        else
        {
            Debug.LogWarning("Fighter list is empty!");
        }
    }
    public void ChooseFighter(int fighterIndex)
    {
        fighterImage.sprite = fighterList[fighterIndex].FighterSpite;
        fighterName.text = fighterList[fighterIndex].FighterName;
        selectedFighterIndex = fighterIndex;
        SaveFighter();
    }

    private void SaveFighter()
    {
        PlayerPrefs.SetInt("SelectedFighterIndex",selectedFighterIndex);
        PlayerPrefs.Save();
    }
}
