using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HexaDirection
{
    NE, E, SE, SW, W, NW
}
public static class HexaDirectionExtensions
{
    public static HexaDirection Opposite(this HexaDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }
}
public class HexaObject : MonoBehaviour
{
    public HexaCord  hexCord;
    [SerializeField]
    HexaObject[] neighbors;  
    public Color color;
    public GameObject cornerSprite;

    #region Neigbors
    public HexaObject GetNeighbor(HexaDirection direction)
    {
        return neighbors[(int)direction];
    }
    public void SetNeighbor(HexaDirection direction, HexaObject objects)
    {
        neighbors[(int)direction] = objects;
        objects.neighbors[(int)direction.Opposite()] = this;
    }
    #endregion
   

}

