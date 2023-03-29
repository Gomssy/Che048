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

    void Start()
    {
        playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        turnEndButton.AddListener(() =>
        {
            GameManager.Inst.ChangeTurn();
            playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        });
    }

    public void SetTurnEndButton()
    {
        Image img = turnEndButton.GetComponent<Image>();        
        img.color = GameManager.Inst.playerActed ? Color.green : Color.white;
    }

    public void UpdateNextPieceImage()
    {
        whiteNext.sprite = whiteList[(int)GameManager.Inst.spawnList[GameManager.Inst.white_idx]];
        blackNext.sprite = blackList[(int)GameManager.Inst.spawnList[GameManager.Inst.black_idx]];
    }
}
