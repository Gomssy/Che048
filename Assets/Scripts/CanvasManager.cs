using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField]
    ClickUI turnEndButton;
    [SerializeField]
    TextMeshProUGUI playerTurnText;

    void Start()
    {
        playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        turnEndButton.AddListener(() =>
        {
            GameManager.Inst.ChangeTurn();
            playerTurnText.text = "Current Player : " + GameManager.Inst.checker.ToString();
        });
    }
}
