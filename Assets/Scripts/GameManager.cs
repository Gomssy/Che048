using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Inst;
    [SerializeField]
    private List<GameObject> piecePrefab = new();
    private Dictionary<PieceEnum, GameObject> pieceDict = new();

    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        foreach (var piece in piecePrefab)
        {
            pieceDict.Add(piece.GetComponent<Piece>().pieceType, piece);
        }
    }

    public GameObject GetPiece(PieceEnum pieceEnum)
    {
        if (pieceEnum == PieceEnum.Empty) return null;
        return pieceDict[pieceEnum];
    }

}
