using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private GameController gameController;

    public void setGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void setSpace()
    {
        buttonText.text = gameController.getPlayerSide();
        button.interactable = false;
        gameController.endTurn();
    }
}
