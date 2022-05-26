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
    public bool _IsClick = false;
    public List<GameObject> aryCorner = new List<GameObject>();
    GameObject obj;
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
        if (Input.GetMouseButtonDown(0))
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
        #region CornerAreaCalculatePosition
        #region Other 3 points in Corners
        // ilk baþta 6 köþe tanýmlayýp öyle bir sistem kurdum ancak 3 köþe tanýmlamamýn yeterli olduðunu düþünüp bunlarý çýkarttým.


        /*   // HexaCorner[0]

          if (position.y > (center.y + 5) && position.y <= HexaInfo.corners[0].y)
          {

                  Vector2 corner = (center + HexaInfo.corners[0]);
                  Instantiate(cornerSprite, corner, Quaternion.identity);

          }
          _IsClick = false;
          // HexaCorner[1]
          if (position.x > center.x && position.x < HexaInfo.corners[1].x && position.y > center.y && position.y <= HexaInfo.corners[1].y)
          {
              _IsClick = true;
              if (_IsClick == true)
              {
                  Vector2 corner = (center + HexaInfo.corners[1]);
                  Instantiate(cornerSprite, corner, Quaternion.identity);
              }


          }
          _IsClick = false;
       */

        /*  // HexaCorner[5]
        if(position.x<center.x && position.x >= HexaInfo.corners[5].x && position.y>center.y && position.y <= HexaInfo.corners[5].y)
        {
            _IsClick = true;
            if (_IsClick == true)
            {
                Vector2 corner = (center + HexaInfo.corners[5]);
                Instantiate(cornerSprite, corner, Quaternion.identity);
            }
            _IsClick = false;
        }
      */
        #endregion

        // HexaCorner[2]
        if (position.x > center.x && position.x < HexaInfo.corners[2].x && position.y < center.y && position.y >= HexaInfo.corners[2].y)
        {
            _IsClick = true;
            if (_IsClick == true && hexa.GetNeighbor(HexaDirection.E) != null && hexa.GetNeighbor(HexaDirection.SE) != null)
            {

                Vector2 corner = (center + HexaInfo.corners[2]);
                obj = Instantiate(cornerSprite, corner, Quaternion.identity);
                if (aryCorner.Count != 0)
                {
                    while (aryCorner[aryCorner.Count - 1].transform.childCount > 0)
                    {
                        foreach (Transform child in aryCorner[aryCorner.Count - 1].transform)
                        {
                            child.transform.parent = gameObject.transform;
                        }
                    }
                }
                hexa.GetNeighbor(HexaDirection.E).transform.parent = obj.transform;
                hexa.GetNeighbor(HexaDirection.SE).transform.parent = obj.transform;
                hexa.transform.parent = obj.transform.parent;
                if (aryCorner.Count != 0)
                {
                   

                    aryCorner[aryCorner.Count - 1].SetActive(false);
                    Destroy(aryCorner[aryCorner.Count - 1]);
                }
                aryCorner.Add(obj);
            }
            _IsClick = false;
        }
        // HexaCorner[3]
        if (position.y < (center.y - 5) && position.y >= HexaInfo.corners[3].y)
        {
            _IsClick = true;
            if (_IsClick == true && hexa.GetNeighbor(HexaDirection.SW)!=null && hexa.GetNeighbor(HexaDirection.SE)!=null)
            {
                Vector2 corner = (center + HexaInfo.corners[3]);

                obj = Instantiate(cornerSprite, corner, Quaternion.identity);

                if (aryCorner.Count != 0)
                {
                    while (aryCorner[aryCorner.Count - 1].transform.childCount > 0)
                    {
                        foreach (Transform child in aryCorner[aryCorner.Count - 1].transform)
                        {
                            child.transform.parent = gameObject.transform;
                        }
                    }
                }

                hexa.GetNeighbor(HexaDirection.SW).transform.parent = obj.transform;
                hexa.GetNeighbor(HexaDirection.SE).transform.parent = obj.transform;
                hexa.transform.parent = obj.transform;
                if (aryCorner.Count != 0)
                {
                  
                    aryCorner[aryCorner.Count - 1].SetActive(false);
                    Destroy(aryCorner[aryCorner.Count - 1]);
                }              
                aryCorner.Add(obj);
            }
            _IsClick = false;
        }
        // HexaCorner[4]
        if (position.x < center.x && position.x >= HexaInfo.corners[4].x && position.y < center.y && position.y >= HexaInfo.corners[4].y)
        {
            _IsClick = true;
            if (_IsClick == true && hexa.GetNeighbor(HexaDirection.SW) != null && hexa.GetNeighbor(HexaDirection.W) != null)
            {
                Vector2 corner = (center + HexaInfo.corners[4]);
                // GameObject obj;
                obj = Instantiate(cornerSprite, corner, Quaternion.identity);
                if (aryCorner.Count != 0)
                {
                    while (aryCorner[aryCorner.Count - 1].transform.childCount > 0)
                    {
                        foreach (Transform child in aryCorner[aryCorner.Count - 1].transform)
                        {
                            child.transform.parent = gameObject.transform;
                        }
                    }
                }

                hexa.GetNeighbor(HexaDirection.W).transform.parent = obj.transform;
                hexa.GetNeighbor(HexaDirection.SW).transform.parent = obj.transform;
                hexa.transform.parent = obj.transform;

                if (aryCorner.Count != 0)
                {
                   

                    aryCorner[aryCorner.Count - 1].SetActive(false);
                    Destroy(aryCorner[aryCorner.Count - 1]);
                }
                
                aryCorner.Add(obj);

            }
            _IsClick = false;
        }

        #endregion
    }


    public void CreateHexaObj(int x, int y, int i)
    {

        Vector2 position;
        position.x = (x + y * 0.5f - y / 2) * (HexaInfo.innerRadius * 2f);
        position.y = y * (HexaInfo.outerRadius * 1.5f);
        HexaObject hexa = hexaObjects[i] = Instantiate<HexaObject>(objectPrefab);
        if (x > 0)
        {
            hexa.SetNeighbor(HexaDirection.W, hexaObjects[i - 1]);
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
