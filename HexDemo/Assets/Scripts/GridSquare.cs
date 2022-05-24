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
    HexaObject[] hexaObjects;
    public Color[] colors;
    private void Awake()
    {
        gridSquareCanvas = GetComponentInChildren<Canvas>();
        hexaMesh = GetComponentInChildren<HexaMesh>();

        hexaObjects = new HexaObject[height * width];
    }
    private void Start()
    {
        for (int y = 0, i = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHexaObj(x, y, i++);
            }
        }


        hexaMesh.Triangulate(hexaObjects);
       
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
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
        Debug.Log("Touch " + position);
       
    }

    public void CreateHexaObj(int x, int y, int i)
    {

        Vector2 position;
        position.x = (x + y * 0.5f - y / 2) * (HexaInfo.innerRadius * 2f); 
        position.y = y * (HexaInfo.outerRadius * 1.5f);
        HexaObject hexa = hexaObjects[i] = Instantiate<HexaObject>(objectPrefab);
        int randomIndex = Random.Range(0, colors.Length);
        hexa.color = colors[randomIndex];
        hexa.transform.SetParent(transform, false);
        hexa.transform.localPosition = position;
        hexa.hexCord = HexaCord.FromOffsetCoordinates(x, y);


        Text label = Instantiate<Text>(hexaInfoTextPrefab);
        label.rectTransform.SetParent(gridSquareCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
        label.text = hexa.hexCord.ToStringOnSeparateLines();

        
        
        
    }
}
