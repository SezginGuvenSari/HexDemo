using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexaCord
{
    [SerializeField]
    private int x, y;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public HexaCord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static HexaCord FromOffsetCoordinates(int x, int y)
    {
        return new HexaCord(x - y / 2, y);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString();
    }


    public static HexaCord FromPosition(Vector2 position)
    {
        float x = position.x / (HexaInfo.innerRadius * 2f);
        float y = -x;


        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        return new HexaCord(iX, iY);

    }
}


