using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    //Coordinate간 연산자 오버로딩
    public static Coordinate operator +(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.X + c2.X, c1.Y + c2.Y);
    }
    public static Coordinate operator -(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.X - c2.X, c1.Y - c2.Y);
    }

}
