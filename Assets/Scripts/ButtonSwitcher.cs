using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSwitcher : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int currentButtonIndex = 0;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithKeyboard();
    }

    private void MoveWithKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Length) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[currentButtonIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }
}
