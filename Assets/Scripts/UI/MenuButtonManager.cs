using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButtonManager : MonoBehaviour
{
    //! Components
    [SerializeField] List<Button> buttons;
    [SerializeField] private float delaySound;
    private string sceneToLoad;
    private int currentButtonIndex = 0;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color normalColor;

    private void Start()
    {
        SetButtonVisibility(0, false);
        AddButtonListeners();
    }

    private void Update()
    {
        HandleKeyBoardNavigation();
    }

    private void AddButtonListeners()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;

            GameObject buttonGameObject = buttons[i].gameObject;

            EventTrigger trigger = buttonGameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => { OnPointerEnter(index); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => { OnPointerExit(index); });
            trigger.triggers.Add(entryExit);

            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnPointerEnter(int index)
    {
        ChangeButtonColor(buttons[index], hoverColor);
        PlayProgressSound();
    }

    private void OnPointerExit(int index)
    {
        ChangeButtonColor(buttons[index], normalColor);
    }

    private void ChangeButtonColor(Button button, Color newColor)
    {
        Image buttonImage = button.GetComponent<Image>();
        buttonImage.color = newColor;
    }

    private void OnButtonClicked(int index)
    {
        switch (index)
        {
            case 0:
                PlayEndSound();
                break;

            case 1:
                PlayEndSound();
                sceneToLoad = "Gameplay Scene";
                Invoke("LoadScene", delaySound);
                break;

            case 2:
                PlayEndSound();
                break;

            case 3:
                PlayEndSound();
                break;
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void HandleKeyBoardNavigation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToPreviousActiveButton();
            PlayProgressSound();
            UpdateButton();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToNextActiveButton();
            PlayProgressSound();
            UpdateButton();
        }
    }

    private void UpdateButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            UpdateButtonState(buttons[i], i == currentButtonIndex);
        }
    }

    private void UpdateButtonState(Button button, bool isSelected)
    {
        if (isSelected)
        {
            button.Select();
        }
        else
        {
            button.OnDeselect(null);
        }
    }

    private void SetButtonVisibility(int index, bool isVisible)
    {
        if (index >= 0 && index < buttons.Count)
        {
            buttons[index].gameObject.SetActive(isVisible);
        }
    }

    private void MoveToPreviousActiveButton()
    {
        currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;

        while (!buttons[currentButtonIndex].gameObject.activeSelf)
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
        }
    }

    private void MoveToNextActiveButton()
    {
        currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;

        while (!buttons[currentButtonIndex].gameObject.activeSelf)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
        }
    }

    private void PlayEndSound()
    {
        SoundManager.Instance.PlayMenuButtonEndSound();
    }

    private void PlayProgressSound()
    {
        SoundManager.Instance.PlayMenuButtonProgressSound();
    }
}
