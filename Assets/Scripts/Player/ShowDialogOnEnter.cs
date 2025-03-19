using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialogOnEnter : MonoBehaviour
{
    public GameObject dialogCanvas;
    public Text hintText;
    public GameController gameController; // ��������� �� GameController

    public string hintMessage = "Press 'E' to interact";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogCanvas.SetActive(true);
            hintText.text = hintMessage;

            // ��������� ����� EnterDialogState � GameController, ���� ������� ����������� � ��� ������
            gameController.EnterDialogState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogCanvas.SetActive(false);

            // ��������� ����� ExitDialogState � GameController, ���� ������� �������� �� ���� ������
            gameController.ExitDialogState();
        }
    }
}
