using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public PieceEnum pieceType;
    public CheckerEnum checker;
    [SerializeField]
    Sprite[] spriteList = new Sprite[2];
    public Coordinate coord;

    protected List<Coordinate> diff;
    protected int range;

    public virtual void Init(CheckerEnum _checker)
    {
        checker = _checker;
        GetComponent<SpriteRenderer>().sprite = spriteList[(int)checker];
    }

    public List<Coordinate> MovableCoord()
    {
        List<Coordinate> res = new();

        for(int i = 0; i < diff.Count; i++)
        {
            for(int j = 1; j  <range + 1; j++)
            {
                Coordinate tempCoord = coord + diff[i]*j;
                if (tempCoord.X < 0 || tempCoord.Y < 0 || tempCoord.X > 7 || tempCoord.Y > 7) continue;

                if (!GameManager.Inst.PieceExist(tempCoord))
                {
                    res.Add(tempCoord);
                }
                else if (GameManager.Inst.OppoPieceExist(tempCoord, checker))
                {
                    res.Add(tempCoord);
                    break;
                }
                else
                    break;

            }
        }

        return res;
    }

    private void OnMouseDown()
    {
        if (GameManager.Inst.checker != checker) return;
        if (GameManager.Inst.playerActed) return;

        if (GameManager.Inst.isHighlighted)
        {
            Board.Inst.ResetHighlight();
            GameManager.Inst.movableList = null;
        }
        if (GameManager.Inst.selected == this)
        {
            GameManager.Inst.selected = null;
            return;
        }
        GameManager.Inst.selected = this;
        GameManager.Inst.movableList = MovableCoord();
        Board.Inst.HighlightMovable(MovableCoord());
    }
}
