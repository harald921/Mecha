using System.Collections;
using System.Collections.Generic;
using System.IO;

public struct Vector2DInt 
{
    public int x, y;

    public static Vector2DInt Zero  => new Vector2DInt(0, 0);
    public static Vector2DInt One   => new Vector2DInt(1);
    public static Vector2DInt Up    => new Vector2DInt(0, 1);
    public static Vector2DInt Down  => new Vector2DInt(0, -1);
    public static Vector2DInt Left  => new Vector2DInt(-1, 0);
    public static Vector2DInt Right => new Vector2DInt(1, 0);


    public Vector2DInt(int inXandY)
    {
        x = inXandY;
        y = inXandY;
    }

    public Vector2DInt(int inX, int inY)
    {
        x = inX;
        y = inY;
    }

    public void SetXandYto100()
    {
        x = 100;
        y = 100;
    }

    public bool IsWithinZeroAnd(Vector2DInt inOtherVector) // TODO: Change this, this is kinda ugly
    {
        if (x > inOtherVector.x ||
            y > inOtherVector.y ||
            x < 0 || y < 0)
            return false;

        return true;
    }


    public void BinarySave(BinaryWriter inWriter)
    {
        inWriter.Write(x);
        inWriter.Write(y);
    }

    public void BinaryLoad(BinaryReader inReader)
    {
        x = inReader.ReadInt32();
        y = inReader.ReadInt32();
    }

    public override string ToString() =>
        "(" + x + ", " + y + ")";

    public override bool Equals(object obj)
    {
        if (obj is Vector2DInt)
        {
            Vector2DInt objAsVector2DInt = (Vector2DInt)obj;
            if (x == objAsVector2DInt.x &&
                y == objAsVector2DInt.y)
                return true;
        }

        return false;
    }

    public static Vector2DInt operator +(Vector2DInt inVector1, Vector2DInt inVector2) =>
        new Vector2DInt()
        {
            x = inVector1.x + inVector2.x,
            y = inVector1.y + inVector2.y
        };

    public static Vector2DInt operator -(Vector2DInt inVector1, Vector2DInt inVector2) =>
        new Vector2DInt()
        {
            x = inVector1.x - inVector2.x,
            y = inVector1.y - inVector2.y
        };

    public static Vector2DInt operator *(Vector2DInt inVector1, int inInt) =>
        new Vector2DInt()
        {
            x = inVector1.x * inInt,
            y = inVector1.y * inInt
        };

    public static Vector2DInt operator *(Vector2DInt inVector1, Vector2DInt inVector2) =>
        new Vector2DInt()
        {
            x = inVector1.x * inVector2.x,
            y = inVector1.y * inVector2.y
        };

    public static Vector2DInt operator /(Vector2DInt inVector1, Vector2DInt inVector2) =>
        new Vector2DInt()
        {
            x = inVector1.x / inVector2.x,
            y = inVector1.y / inVector2.y
        };

    public static Vector2DInt operator /(Vector2DInt inVector1, int inInt) =>
        new Vector2DInt()
        {
            x = inVector1.x / inInt,
            y = inVector1.y / inInt
        };

    public static Vector2DInt operator %(Vector2DInt inVector1, int inInt) =>
        new Vector2DInt()
        {
            x = inVector1.x % inInt,
            y = inVector1.y % inInt
        };

    public static bool operator ==(Vector2DInt inVector1, Vector2DInt inVector2) =>
        inVector1.x == inVector2.x &&
        inVector1.y == inVector2.y;

    public static bool operator !=(Vector2DInt inVector1, Vector2DInt inVector2) =>
        inVector1.x != inVector2.x ||
        inVector1.y != inVector2.y;
}