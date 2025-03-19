using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialogOnEnter : MonoBehaviour
{
    public GameObject dialogCanvas;
    public Text hintText;
    public GameController gameController; // Посилання на GameController

    public string hintMessage = "Press 'E' to interact";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogCanvas.SetActive(true);
            hintText.text = hintMessage;

            // Викликаємо метод EnterDialogState у GameController, коли гравець знаходиться у зоні пропсів
            gameController.EnterDialogState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogCanvas.SetActive(false);

            // Викликаємо метод ExitDialogState у GameController, коли гравець виходить із зони пропсів
            gameController.ExitDialogState();
        }
    }
}
