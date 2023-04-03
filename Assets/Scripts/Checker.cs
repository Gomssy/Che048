using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public CheckerEnum curChecker;
    public Coordinate coord { get; set; }
    public Piece curPiece;
    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    public void OnMouseDown()
    {
        if (GameManager.Inst.playerActed || GameManager.Inst.isGameOver) return;

        if(GameManager.Inst.selected != null)
        {
            Piece temp = GameManager.Inst.selected;
            foreach(var movable in GameManager.Inst.movableList)
            {
                if(movable.X == coord.X && movable.Y == coord.Y)
                {
                    //이동한 위치에 상대 기물이 있다면 제거
                    if (GameManager.Inst.boardState[movable.X, movable.Y].curChecker != CheckerEnum.Empty)
                    {
                        GameManager.Inst.boardState[movable.X, movable.Y].RemovePiece();
                    }
                    GameManager.Inst.boardState[temp.coord.X, temp.coord.Y].MovePiece(this);
                    Board.Inst.ResetHighlight();
                    GameManager.Inst.selected = null;
                    GameManager.Inst.movableList = null;

                    GameManager.Inst.playerActed = true;
                    CanvasManager.Inst.SetTurnEndButton();
                }
            }
            return;
        }

        if(curChecker == CheckerEnum.Empty)
        {
            if(GameManager.Inst.checker == CheckerEnum.White)
            {
                if (GameManager.Inst.white_idx > 15) return;
                Spawn(GameManager.Inst.spawnList[GameManager.Inst.white_idx++]);

                GameManager.Inst.playerActed = true;
                CanvasManager.Inst.SetTurnEndButton();
            }
            else
            {
                if (GameManager.Inst.black_idx > 15) return;
                Spawn(GameManager.Inst.spawnList[GameManager.Inst.black_idx++]);

                GameManager.Inst.playerActed = true;
                CanvasManager.Inst.SetTurnEndButton();
            }
            CanvasManager.Inst.UpdateNextPieceImage();
        }
    }

    public void MovePiece(Checker _checker)
    {
        if(this ==  _checker) return;

        curPiece.transform.position = _checker.transform.position;
        curPiece.coord = _checker.coord;
        _checker.curChecker = curChecker;
        _checker.curPiece = curPiece;

        curChecker = CheckerEnum.Empty;
        curPiece = null;
    }

    public void RemovePiece()
    {
        Destroy(curPiece.gameObject);
        curChecker = CheckerEnum.Empty;
        curPiece= null;
    }

    private void Spawn(PieceEnum pieceEnum)
    {
        GameObject piece = GameManager.Inst.GetPiece(pieceEnum);
        curPiece = Instantiate(piece, transform.position, Quaternion.identity, GameManager.Inst.pieces).GetComponent<Piece>();

        curChecker = GameManager.Inst.checker;

        curPiece.coord = coord;
        curPiece.Init(GameManager.Inst.checker);

    }

    public void Highlight(Color color)
    {
        sp.color = color;
    }
}

