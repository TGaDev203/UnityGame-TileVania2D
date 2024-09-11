using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Collections;

public class MenuButtonManager : MonoBehaviour
{
    //! Components
    [SerializeField] List<Button> buttons;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private float delayLoadScene;
    [SerializeField] private float coolDownTime;
    private string sceneToLoad;
    private int currentButtonIndex;
    private bool canPlayEndSound = true;
    private bool canPlayProgressSound = true;

    private void Start()
    {
        SetButtonVisibility(0, false);
        AddButtonListeners();
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
        currentButtonIndex = index;
        UpdateButton();
    }

    private void OnPointerExit(int index)
    {
        if (EventSystem.current.currentSelectedGameObject == buttons[currentButtonIndex].gameObject)
        {
            buttons[currentButtonIndex].OnDeselect(null);
        }
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
                Invoke("LoadScene", delayLoadScene);
                break;

            case 2:
                PlayEndSound();
                break;

            case 3:
                PlayEndSound();
                QuitGame();
                break;
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
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

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PlayEndSound()
    {
        if (canPlayEndSound)
        {
        SoundManager.Instance.PlayMenuButtonEndSound();
        StartCoroutine(ResetEndSoundCoolDown());
        }
    }

    private void PlayProgressSound()
    {
        if (canPlayProgressSound)
        {
        SoundManager.Instance.PlayMenuButtonProgressSound();
        StartCoroutine(ResetProgressSoundCoolDown());
        }
    }

    private IEnumerator ResetEndSoundCoolDown()
    {
        canPlayEndSound = false;
        yield return new WaitForSeconds(coolDownTime);
        canPlayEndSound = true;
    }

    private IEnumerator ResetProgressSoundCoolDown()
    {
        canPlayProgressSound = false;
        yield return new WaitForSeconds(coolDownTime);
        canPlayProgressSound = true;
    }
}
