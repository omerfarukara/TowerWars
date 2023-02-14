using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeButtonController : MonoBehaviour
{
    [SerializeField] Sprite playSprite;

    RandomizeButton[] buttons;

    int randomIndex, currentIndex;

    private void Awake()
    {
        buttons = GetComponentsInChildren<RandomizeButton>();
    }

    void Start()
    {
        RandomSelect();
    }

    void RandomSelect()
    {
        buttons[currentIndex].ResetButton();

        randomIndex = Random.Range(0, buttons.Length);
        if (randomIndex != currentIndex)
        {
            currentIndex = randomIndex;
            buttons[currentIndex].SetPlayButton(playSprite);
            Invoke(nameof(RandomSelect), 2f);
        }
        else
        {
            RandomSelect();
        }

    }
}
