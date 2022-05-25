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
        return new HexaCord(x - (y / 2), y);
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
        float z = -x;
        float offset = position.y / (HexaInfo.outerRadius * 3f);
        x -= offset;
        z-= offset;


        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(-x-z);
        int iZ = Mathf.RoundToInt(z);

        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX); 
            float dY = Mathf.Abs(-x -z - iY);
            float dZ = Mathf.Abs(z - iZ);
          
            if(dX>dZ && dX > dY)
            {
                iX = -iZ - iY;

            }
            else if (dY > dZ)
            {
                iY = -iX - iZ;
            }

        }
        return new HexaCord(iX, iY);

    }
}


