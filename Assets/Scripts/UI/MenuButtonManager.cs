using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuButtonManager : MonoBehaviour
{
    //! Components
    [SerializeField] Canvas canvas;
    [SerializeField] List<Button> buttons;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {

    }

    private void Start()
    {
        AddButtonListeners();
    }

    private void Update()
    {
        
    }

    private void AddButtonListeners()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Need to save the index number in local variable
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    public void OnButtonClicked(int index)
    {
        Debug.Log("Button " + index + " is pressed");

        switch (index)
        {
            case 0:
                Debug.Log("New Game");
                break;

            case 1:
                Debug.Log("Option");
                break;

            case 2:
                Debug.Log("Quit");
                break;
        }
    }

}
