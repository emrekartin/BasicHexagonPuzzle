using UnityEngine;
using System.Collections;
using System;
using Random = System.Random;
public class Game : MonoBehaviour
{

    private int hexagonheight = 9;
    private int hexagonweight = 8;

    private int triplehexagon_x1;
    private int triplehexagon_y1;
    private int triplehexagon_x2;
    private int triplehexagon_y2;
    private int triplehexagon_x3;
    private int triplehexagon_y3;

    private GameObject[,] nodelist = new GameObject[16, 7];
    private GameObject[,] hexagonlist;

    public GameObject hexagons;
    public GameObject nodes;
    public GameObject touchsensor;
    private GameObject currenttouchsensor;

    private Camera cam;

    private GameObject holdName;
    private GameObject holdObj;
    private Random choosencolor = new Random();
    static int[] nodeindex = new int[2];
    


    private int[] closesthexagonindex_i = new int[20];
    private int[] closesthexagonindex_y = new int[20];

    private string stringname;
    private char[] chararr = new char[3];
    
    private SpriteRenderer spriteRenderer;


    private Vector3 Circle_Vector;
    private Vector3 Hex_Vector;
    private Vector3 hold_Hex_Vector;
    private Vector3 hold_Vector;
    
    
    private Vector2 oldPosition;
    private Vector2 newPosition;
    
    
    private int givecolor = 0;
    private int controlcolor;
    private int sendindex_i;
    private static int sendindex_j;
    private int counter;


    private int listindex;
    private int updatehexagon = 0;
    private int checkhexagon = 0;
    private float coefficient_height = 0.700f;
    private float coefficient_weight = 0.600f;


    private int currentPoint = 0;
    private string text_point = "";

    void Awake()
    {
        hexagonlist = new GameObject[9, 8];
        holdObj = new GameObject();
        holdName = new GameObject();
        hold_Hex_Vector = new Vector3();
        cam = Camera.main;
        PointScreen();
        CreateHexagonMap();
        CircleCreate();
    }
    void checkHexagon()
    {
        listindex = 0;
        if(checkhexagon == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int u = 0; u < 7; u++)
                {
                    sendindex_i = i;
                    sendindex_j = u;
                    nearestObject(sendindex_i, sendindex_j);

                    if (hexagonlist[triplehexagon_x1, triplehexagon_y1].GetComponent<SpriteRenderer>().color == hexagonlist[triplehexagon_x2, triplehexagon_y2].GetComponent<SpriteRenderer>().color)
                    {
                        if (hexagonlist[triplehexagon_x1, triplehexagon_y1].GetComponent<SpriteRenderer>().color == hexagonlist[triplehexagon_x3, triplehexagon_y3].GetComponent<SpriteRenderer>().color)
                        {
                            checkhexagon++;
                            closesthexagonindex_i[listindex] = triplehexagon_x1;
                            closesthexagonindex_y[listindex] = triplehexagon_y1;
                            listindex++;
                            counter++;
                            closesthexagonindex_i[listindex] = triplehexagon_x2;
                            closesthexagonindex_y[listindex] = triplehexagon_y2;
                            listindex++;
                            counter++;
                            closesthexagonindex_i[listindex] = triplehexagon_x3;
                            closesthexagonindex_y[listindex] = triplehexagon_y3;
                            listindex++;
                            counter++;
                        }
                    }
                }

            }

            for (int i = 0; i < counter; i++)
            {
                for (int j = 0; j < counter; j++)
                {


                    if (((closesthexagonindex_i[i] * 10) + closesthexagonindex_y[i]) == ((closesthexagonindex_i[j] * 10) + closesthexagonindex_y[j]))
                    {
                        if (i != j)
                        {
                            closesthexagonindex_i[j] = -1;
                            closesthexagonindex_y[j] = -1;
                        }
                    }
                }
            }
        }
        if (counter > 2){ 
            if(updatehexagon == 0)
            {
                updatehexagon++;
                StartCoroutine(WaitForLevelSwitch());
            }
        }
    }
    void Update()
    {
        if (updatehexagon == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCutting();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCutting();
            }
        }
        checkHexagon();
    }
    public void counterclockwiseRotationButton()
    {
        nodeindex = sendarr();
        sendindex_i = nodeindex[0];

        sendindex_j = nodeindex[1];
        nearestObject(sendindex_i, sendindex_j);
        if (sendindex_i % 2 == 1)
        {
            clockwiseRotation();
        }
        else
        {
            counterclockwiseRotation();
        }
    }
    public void clockwiseRotationButton()
    {

        nodeindex = sendarr();
        sendindex_i = nodeindex[0];

        sendindex_j = nodeindex[1];
        nearestObject(sendindex_i, sendindex_j);
        if (sendindex_i % 2 == 1)
        {
            counterclockwiseRotation();
        }
        else
        {
            clockwiseRotation();
        }
    }
    void nearestObject(int index_i, int index_j)
    {

        if ((index_i % 2) == 0)
        {
            triplehexagon_x1 = index_i / 2;
        }
        else
        {
            triplehexagon_x1 = (index_i / 2) + 1;
        }
        triplehexagon_y1 = index_j;
        triplehexagon_x2 = triplehexagon_x1;
        triplehexagon_y2 = index_j + 1;
        if ((index_i % 2) == 0)
        {
            triplehexagon_x3 = (index_i / 2) + 1;
        }
        else
        {
            triplehexagon_x3 = (index_i / 2);
        }
        if ((index_i % 2) == 0)
        {
            if ((index_j % 2) == 0)
            {
                triplehexagon_y3 = index_j + 1;
            }
            else
            {
                triplehexagon_y3 = index_j;
            }
        }
        else
        {
            if ((index_j % 2) == 0)
            {
                triplehexagon_y3 = index_j;
            }
            else
            {
                triplehexagon_y3 = index_j + 1;
            }
        }
    }
    void CreateHexagonMap()
    {
        for (int i = 0; i < hexagonheight; i++)
        {
                for (int j = 0; j < hexagonweight; j++)
                {
                    float xPos = i * coefficient_height;
                    if (j % 2 != 1)
                    {
                        xPos += coefficient_height / 2f;
                    }
               
                     
                hexagons.name = "hex" + i + j;
                Hex_Vector = new Vector3(j * coefficient_weight - 2.1f , xPos - 2.5f, 0);
                hexagonlist[i, j] = Instantiate(hexagons, Hex_Vector, Quaternion.identity, GameObject.FindGameObjectWithTag("HexMap").transform);
                spriteRenderer = hexagons.GetComponent<SpriteRenderer>();

                chooseColor();
                }
        }
        if (Screen.height > 1920)
        {
            GameObject.FindGameObjectWithTag("HexMap").transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        }
    }
    void chooseColor()
    {
        while (true)
        {
            controlcolor = choosencolor.Next(0, 7);
            if (givecolor != controlcolor)
            {
                givecolor = controlcolor;
                break;
            }
        }
        if (givecolor == 0)
        {
            spriteRenderer.color = Color.red;
        }
        else if (givecolor == 1)
        {
            spriteRenderer.color = Color.green;
        }
        else if (givecolor == 2)
        {
            spriteRenderer.color = Color.blue;
        }
        else if (givecolor == 3)
        {
            spriteRenderer.color = Color.cyan;
        }
        else if (givecolor == 4)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (givecolor == 5)
        {
            spriteRenderer.color = Color.gray;
        }
        else if (givecolor == 6)
        {
            spriteRenderer.color = Color.black;
        }
    }
    void updateHexagon()
    {
        sýrala();


        for (int i = 0; i< counter; i++)
        {
            int indexxx = closesthexagonindex_i[i];
            int indexxy = closesthexagonindex_y[i];

            if (closesthexagonindex_i[i] != -1)
            {
                hold_Vector = hexagonlist[hexagonheight - 1, indexxy].transform.position;
                holdName.name = hexagonlist[hexagonheight - 1, indexxy].name;

                for (int j = hexagonheight - 1; j > indexxx; j--)
                {
                    hexagonlist[j, indexxy].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[j, indexxy].transform.position, hexagonlist[j-1, indexxy].transform.position, 1f);
                    hexagonlist[j, indexxy].name = hexagonlist[j-1, indexxy].name;
                    
                }

                hexagonlist[indexxx, indexxy].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[indexxx, indexxy].transform.position, hold_Vector, 1f);
                hexagonlist[indexxx, indexxy].name = holdName.name;
                hexagonlist[indexxx, indexxy].GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        for (int i = 0; i < hexagonheight; i++)
        {
            for (int j = 0; j < hexagonweight ; j++)
            {
                hexagonlist[i,j] = GameObject.Find("HexMap").transform.Find("hex" + (i) + (j) + "(Clone)").gameObject;
            }
        }
        listindex = 0;
        counter = 0;
    }
    void sýrala()
    {
        int hold1, hold2;
        
        for (int i = 0; i < counter; i++)
        {
            for (int j = 0; j < counter; j++)
            {
                for (int t = 0; t < counter; t++)
                {
                    if (closesthexagonindex_i[j] < closesthexagonindex_i[t])
                    {
                        hold1 = closesthexagonindex_i[j];
                        hold2 = closesthexagonindex_y[j];

                        closesthexagonindex_i[j] = closesthexagonindex_i[t];
                        closesthexagonindex_y[j] = closesthexagonindex_y[t];

                        closesthexagonindex_i[t] = hold1;
                        closesthexagonindex_y[t] = hold2;
                    }
                    else if (closesthexagonindex_i[j] == closesthexagonindex_i[t])
                    {
                        if (closesthexagonindex_y[j] < closesthexagonindex_y[t])
                        {
                            hold1 = closesthexagonindex_i[j];
                            hold2 = closesthexagonindex_y[j];

                            closesthexagonindex_i[j] = closesthexagonindex_i[t];
                            closesthexagonindex_y[j] = closesthexagonindex_y[t];

                            closesthexagonindex_i[t] = hold1;
                            closesthexagonindex_y[t] = hold2;
                        }
                    }
                }
            }
        }
    }
    public void counterclockwiseRotation()
    {

        holdName.name = hexagonlist[triplehexagon_x1, triplehexagon_y1].name;

        hold_Hex_Vector = hexagonlist[triplehexagon_x1, triplehexagon_y1].transform.position;
        hexagonlist[triplehexagon_x1, triplehexagon_y1].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[triplehexagon_x1, triplehexagon_y1].transform.position, hexagonlist[triplehexagon_x2, triplehexagon_y2].transform.position, 1f);
        hexagonlist[triplehexagon_x1, triplehexagon_y1].name = hexagonlist[triplehexagon_x2, triplehexagon_y2].name;

        hexagonlist[triplehexagon_x2, triplehexagon_y2].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[triplehexagon_x2, triplehexagon_y2].transform.position, hexagonlist[triplehexagon_x3, triplehexagon_y3].transform.position, 1f);
        hexagonlist[triplehexagon_x2, triplehexagon_y2].name = hexagonlist[triplehexagon_x3, triplehexagon_y3].name;

        hexagonlist[triplehexagon_x3, triplehexagon_y3].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[triplehexagon_x3, triplehexagon_y3].transform.position, hold_Hex_Vector, 1f);
        hexagonlist[triplehexagon_x3, triplehexagon_y3].name = holdName.name;

        holdObj = hexagonlist[triplehexagon_x2, triplehexagon_y2];

        hexagonlist[triplehexagon_x2, triplehexagon_y2] = hexagonlist[triplehexagon_x1, triplehexagon_y1];
        hexagonlist[triplehexagon_x1, triplehexagon_y1] = hexagonlist[triplehexagon_x3, triplehexagon_y3];
        hexagonlist[triplehexagon_x3, triplehexagon_y3] = holdObj;



    }
    public void clockwiseRotation()
    {
        holdName.name = hexagonlist[triplehexagon_x1, triplehexagon_y1].name;

        hold_Hex_Vector = hexagonlist[triplehexagon_x1, triplehexagon_y1].transform.position;

        hexagonlist[triplehexagon_x1, triplehexagon_y1].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[triplehexagon_x1, triplehexagon_y1].transform.position, hexagonlist[triplehexagon_x3, triplehexagon_y3].transform.position, 1f);
        hexagonlist[triplehexagon_x1, triplehexagon_y1].name = hexagonlist[triplehexagon_x3, triplehexagon_y3].name;

        hexagonlist[triplehexagon_x3, triplehexagon_y3].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[triplehexagon_x3, triplehexagon_y3].transform.position, hexagonlist[triplehexagon_x2, triplehexagon_y2].transform.position, 1f);
        hexagonlist[triplehexagon_x3, triplehexagon_y3].name = hexagonlist[triplehexagon_x2, triplehexagon_y2].name;
        hexagonlist[triplehexagon_x2, triplehexagon_y2].GetComponent<Transform>().position = Vector3.Lerp(hexagonlist[triplehexagon_x2, triplehexagon_y2].transform.position, hold_Hex_Vector, 1f);

        hexagonlist[triplehexagon_x2, triplehexagon_y2].name = holdName.name;

        holdObj = hexagonlist[triplehexagon_x1, triplehexagon_y1];

        hexagonlist[triplehexagon_x1, triplehexagon_y1] = hexagonlist[triplehexagon_x2, triplehexagon_y2];
        hexagonlist[triplehexagon_x2, triplehexagon_y2] = hexagonlist[triplehexagon_x3, triplehexagon_y3];
        hexagonlist[triplehexagon_x3, triplehexagon_y3] = holdObj;

    }
    void StartCutting()
    {
        currenttouchsensor = Instantiate(touchsensor, transform);
        currenttouchsensor.GetComponent<Renderer>().enabled = false;
        oldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void StopCutting()
    {
        newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (newPosition.x > oldPosition.x + 1.5f || newPosition.y > oldPosition.y + 1.5f)
        {
            clockwiseRotationButton();

        }
        else if (newPosition.x + 1.5f < oldPosition.x || newPosition.y + 1.5f < oldPosition.y)
        {
            counterclockwiseRotationButton();
        }
        else
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            TrailRenderer tr = currenttouchsensor.GetComponent(typeof(TrailRenderer)) as TrailRenderer;
            tr.endWidth = 0;
            float min = 500;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    float xx = cam.ScreenToWorldPoint(Input.mousePosition).x;
                    float yy = cam.ScreenToWorldPoint(Input.mousePosition).y;
                    float aa = nodelist[i, j].transform.position.x;
                    float bb = nodelist[i, j].transform.position.y;
                    float totalhipoxa = xx - aa;
                    totalhipoxa = totalhipoxa * totalhipoxa;
                    float totalhipoyb = yy - bb;
                    totalhipoyb = totalhipoyb * totalhipoyb;
                    float distancefar = totalhipoxa + totalhipoyb;
                    distancefar = (float)Math.Sqrt(distancefar);
                    if (distancefar < min)
                    {
                        min = distancefar;
                        var rig = nodelist[i, j].GetComponent<Rigidbody>();

                        stringname = nodelist[i, j].transform.name;

                        if (stringname.Length < 16)
                        {
                            chararr[0] = stringname[6];
                            chararr[1] = '0';
                            chararr[2] = stringname[7];
                            nodeindex[0] = (int)char.GetNumericValue(chararr[0]);
                            nodeindex[1] = (int)char.GetNumericValue(chararr[2]);
                        }
                        else
                        {
                            chararr[0] = stringname[6];
                            chararr[1] = stringname[7];
                            chararr[2] = stringname[8];
                            nodeindex[0] = (int)char.GetNumericValue(chararr[0]) * 10 + (int)char.GetNumericValue(chararr[1]);
                            nodeindex[1] = (int)char.GetNumericValue(chararr[2]);
                        }
                        sendarr();
                    }
                    currenttouchsensor.transform.SetParent(null);
                }
            }
            currenttouchsensor.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
            Destroy(currenttouchsensor, 2f);
        }
    }
    void CircleCreate()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                sendindex_i = i;
                sendindex_j = j;
                nearestObject(sendindex_i, sendindex_j);
                nodes.name = "circle" + i + j;
                Circle_Vector = new Vector3((hexagonlist[triplehexagon_x1, triplehexagon_y1].transform.position.x + hexagonlist[triplehexagon_x2, triplehexagon_y2].transform.position.x + hexagonlist[triplehexagon_x3, triplehexagon_y3].transform.position.x) / 3, (hexagonlist[triplehexagon_x1, triplehexagon_y1].transform.position.y +hexagonlist[triplehexagon_x2, triplehexagon_y2].transform.position.y +hexagonlist[triplehexagon_x3, triplehexagon_y3].transform.position.y) / 3, -1);
                nodelist[i, j] = Instantiate(nodes, Circle_Vector, Quaternion.identity, GameObject.FindGameObjectWithTag("CircleMap").transform);
            }
        }
    }
    static public int[] sendarr()
    {
        return nodeindex;
    }
    IEnumerator WaitForLevelSwitch()
    {
        yield return new WaitForSeconds(1);
        int atb;
        int ata;
        for (int i = 0; i < counter; i++)
        {
            ata = closesthexagonindex_i[i];
            atb = closesthexagonindex_y[i];
            if (ata != -1)
            {
                currentPoint++;
                hexagonlist[ata, atb].GetComponent<SpriteRenderer>().enabled = false;
                spriteRenderer = hexagonlist[ata, atb].GetComponent<SpriteRenderer>();
                
                chooseColor();
            }

        }
                PointScreen();
                updateHexagon();
        updatehexagon = 0;
        checkhexagon = 0;
    }
    void PointScreen()
    {
        text_point = "" + currentPoint;
        GameObject.Find("Point").GetComponent<TextMesh>().text = text_point;
    }
}