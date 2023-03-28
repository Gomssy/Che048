using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{    public override void Init(CheckerEnum _checker)
    {
        base.Init(_checker);

        diff = new List<Coordinate>()
        {
            new Coordinate(1,2),
            new Coordinate(1,-2),
            new Coordinate(2,1),
            new Coordinate(2,-1),
            new Coordinate(-1,2),
            new Coordinate(-1,-2),
            new Coordinate(-2,1),
            new Coordinate(-2,-1)
        };
        range = 1;
    }
}
