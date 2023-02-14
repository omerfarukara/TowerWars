using GameFolders.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomizeButton : MonoBehaviour
{
    internal Image _image;
    private TextMeshProUGUI _buttonText;
    private Sprite defaultSprite;
    private Button _button;

    private string defaultText;
    private bool isPlayButton;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

        defaultText = _buttonText.text;
        defaultSprite = _image.sprite;

        _button.onClick.AddListener(Play);
    }

    private void Play()
    {
        if (!isPlayButton) return;
        SceneManager.LoadScene(GameManager.Instance.Level);
    }

    internal void SetPlayButton(Sprite playSprite)
    {
        isPlayButton = true;
        _image.sprite = playSprite;
        _buttonText.text = $"Play";
    }

    internal void ResetButton()
    {
        isPlayButton = false;
        _image.sprite = defaultSprite;
        _buttonText.text = defaultText;
    }
}
