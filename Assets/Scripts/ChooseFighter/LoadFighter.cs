using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadFighter : MonoBehaviour
{
    [SerializeField] private List<Fighter> fighterList = new List<Fighter>();

    [SerializeField] private TextMeshProUGUI fighterName;
    [SerializeField] private Image fighterImage;
    [SerializeField] private Animator fighterAnimator;
    private int selectedFighterIndex;

    private void Start()
    {
        selectedFighterIndex = PlayerPrefs.GetInt("SelectedFighterIndex",0);
        fighterName.text = fighterList[selectedFighterIndex].FighterName;
        fighterAnimator.runtimeAnimatorController = fighterList[selectedFighterIndex].FighterAnimator;
    }
}
