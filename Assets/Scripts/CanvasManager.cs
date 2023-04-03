using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField]
    ClickUI turnEndButton;
    [SerializeField]
    TextMeshProUGUI playerTurnText;
    [SerializeField]
    List<Sprite> whiteList = new();
    [SerializeField]
    List<Sprite> blackList = new();
    [SerializeField]
    Image whiteNext;
    [SerializeField]
    Image blackNext;

    [SerializeField]
    TextMeshProUGUI winText;
    [SerializeField]
    ClickUI gameRestartButton;
    [SerializeField]
    RectTransform resultPanel;

    void Start()
    {
        playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        turnEndButton.AddListener(() =>
        {
            GameManager.Inst.ChangeTurn();
            playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        });

        gameRestartButton.AddListener(() =>
        {
            GameManager.Inst.ResetGame();
            resultPanel.gameObject.SetActive(false);
        });

    }

    public void SetTurnEndButton()
    {
        Image img = turnEndButton.GetComponent<Image>();        
        img.color = GameManager.Inst.playerActed ? Color.green : Color.white;
    }

    public void UpdateNextPieceImage()
    {
        if (GameManager.Inst.white_idx > 15)
        {
            whiteNext.sprite = null;
            whiteNext.enabled = false;
        }
        else
            whiteNext.sprite = whiteList[(int)GameManager.Inst.spawnList[GameManager.Inst.white_idx]];

        if (GameManager.Inst.black_idx > 15)
        {
            blackNext.sprite = null;
            blackNext.enabled = false;
        }
        else
            blackNext.sprite = blackList[(int)GameManager.Inst.spawnList[GameManager.Inst.black_idx]];
    }

    public void ShowResult(CheckerEnum winner)
    {
        resultPanel.gameObject.SetActive(true);
        winText.text = $"{winner.ToString()} Win";
    }

    public void ResetUI()
    {
        playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        whiteNext.enabled = true;
        blackNext.enabled = true;
        SetTurnEndButton();
        UpdateNextPieceImage();
    }
}
