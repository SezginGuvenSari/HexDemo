using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    HexaMesh hexaMesh;
    public int width = 8;
    public int height = 9;
    public HexaObject objectPrefab;
    public Text hexaInfoTextPrefab;
    Canvas gridSquareCanvas;
   public HexaObject[] hexaObjects;
    public GameObject cornerSprite;

    private void Awake()
    {
        gridSquareCanvas = GetComponentInChildren<Canvas>();
        hexaMesh = GetComponentInChildren<HexaMesh>();

        hexaObjects = new HexaObject[height * width];

        for (int y = 0, i = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHexaObj(x, y, i++);
            }
        }

    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            HandleInput();
        }
    }


    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchHexa(hit.point);
        }
    }

    void TouchHexa(Vector2 position)
    {
        position = transform.InverseTransformPoint(position);
        HexaCord cord = HexaCord.FromPosition(position);
        int index = cord.X + cord.Y * width + cord.Y / 2;
        HexaObject hexa = hexaObjects[index];
        Vector2 center = hexa.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            Vector2 corner = (center + HexaInfo.corners[i]);
            Instantiate(cornerSprite, corner, Quaternion.identity);

        }
        Debug.Log(index);
        Debug.Log("Touch " + position);
       
    }

  
    public void CreateHexaObj(int x, int y, int i)
    {

        Vector2 position;
        position.x = (x + y * 0.5f - y / 2) * (HexaInfo.innerRadius * 2f); 
        position.y = y * (HexaInfo.outerRadius * 1.5f);
        HexaObject hexa = hexaObjects[i] = Instantiate<HexaObject>(objectPrefab);
        if (x > 0)
        {
            hexa.SetNeighbor(HexaDirection.W, hexaObjects[i-1]);
        }
        if (y > 0)
        {
            if ((y & 1) == 0)
            {
                hexa.SetNeighbor(HexaDirection.SE, hexaObjects[i - width]);
                if (x > 0)
                {
                    hexa.SetNeighbor(HexaDirection.SW, hexaObjects[i - width - 1]);
                }
            }
            else
            {
                hexa.SetNeighbor(HexaDirection.SW, hexaObjects[i - width]);
                if (x < width - 1)
                {
                    hexa.SetNeighbor(HexaDirection.SE, hexaObjects[i - width + 1]);
                }
            }
        }
        hexa.transform.SetParent(transform, false);
        hexa.transform.localPosition = position;
        hexa.hexCord = HexaCord.FromOffsetCoordinates(x, y);


        Text label = Instantiate<Text>(hexaInfoTextPrefab);
        label.rectTransform.SetParent(gridSquareCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
        label.text = hexa.hexCord.ToStringOnSeparateLines();

        
        
        
    }
}
