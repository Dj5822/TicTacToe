using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject turnIndicator;
    public GameObject restartButton;

    private int moveCount = 0;

    private void Awake()
    {
        gameControllerReferenceOnButtons();
        setPlayerSide("X");

        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
    }

    void gameControllerReferenceOnButtons()
    {
        for (int i=0; i<buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().setGameControllerReference(this);
        }
    }

    public string getPlayerSide()
    {
        return playerSide;
    }

    public void endTurn()
    {
        moveCount++;

        // check horizontal win conditions.
        for (int i=0; i<8; i+=3)
        {
            if (buttonList[0+i].text == playerSide && buttonList[1+i].text == playerSide && buttonList[2+i].text == playerSide) gameOver(playerSide);
        }

        // check vertical win conditions.
        for (int i = 0; i < 3; i ++)
        {
            if (buttonList[0+i].text == playerSide && buttonList[3+i].text == playerSide && buttonList[6+i].text == playerSide) gameOver(playerSide);
        }

        // check diagonal win conditions.
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) gameOver(playerSide);
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide) gameOver(playerSide);

        // check if game is a draw.
        if (moveCount >= 9)
        {
            gameOver("draw");
        }

        // change player side.
        if (playerSide == "X") setPlayerSide("O");
        else setPlayerSide("X");

    }

    void gameOver(string winningPlayer)
    {
        setBoardInteractable(false);
        restartButton.SetActive(true);
        turnIndicator.SetActive(false);
        if (winningPlayer == "draw") setGameOverText("Draw");
        else setGameOverText(winningPlayer + " wins!");
    }

    void setGameOverText(string text)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = text;
    }

    public void restartGame()
    {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        turnIndicator.SetActive(true);
        moveCount = 0;
        setPlayerSide("X");
        setBoardInteractable(true);

        // clear the board.
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    void setBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void setPlayerSide(string side)
    {
        if (side == "X")
        {
            playerSide = "X";
            turnIndicator.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "X";
        }
        else
        {
            playerSide = "O";
            turnIndicator.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "O";
        }
    }
}
