using UnityEngine;
using System.Collections;
using System;
using Random = System.Random;
public class Game : MonoBehaviour
{
    private Camera cam;
    public GameObject beyaz;
    public GameObject myPrefab;
    public GameObject bladeTrailPrefab;
    private GameObject holdName;
    private GameObject holdObj;
    private GameObject[,] myPrefabList;
    private float aaa = 0.350f;
    private float baa = 0.615f;
    private GameObject[,] beyazList = new GameObject[16, 7];
    private Random choosencolor = new Random();
    static  int[] intarr = new int[2];
    private int[] indexx = new int[20];
    private int[] indexy = new int[20];
    private string stringname;
    private char[] chararr = new char[3];
    private GameObject currentBladeTrail;
    private SpriteRenderer mr;
    private Vector3 mysecondVector;
    private Vector3 myVector;
    private Vector3 holdVector;
    private Vector3 holdvec;
    private Vector2 oldPosition;
    private Vector2 newPosition;
    private int hexagonheight = 9;
    private int hexagonweight = 8;
    private int givecolor = 0;
    private int controlcolor;
    private int sendindex_i;
    private static int sendindex_j;
    private int counter;
    private int index_x1;
    private int index_y1;
    private int index_x2;
    private int index_y2;
    private int index_x3;
    private int index_y3;
    private int k;
    private int t = 0;
    private int tu = 0;
    private float a = 0.700f;
    private float b = 0.600f;
    public int Point = 0;
    private string point = "";
    void Awake()
    {
        myPrefabList = new GameObject[9, 8];
        holdObj = new GameObject();
        holdName = new GameObject();
        holdVector = new Vector3();
        cam = Camera.main;
        PointScreen();
        CreateHexagonMap();
        CircleCreate();
    }
    void checkHexagon()
    {
        k = 0;
        if(tu == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int u = 0; u < 7; u++)
                {
                    sendindex_i = i;
                    sendindex_j = u;
                    nearestObject(sendindex_i, sendindex_j);

                    if (myPrefabList[index_x1, index_y1].GetComponent<SpriteRenderer>().color == myPrefabList[index_x2, index_y2].GetComponent<SpriteRenderer>().color)
                    {
                        if (myPrefabList[index_x1, index_y1].GetComponent<SpriteRenderer>().color == myPrefabList[index_x3, index_y3].GetComponent<SpriteRenderer>().color)
                        {
                            tu++;
                            indexx[k] = index_x1;
                            indexy[k] = index_y1;
                            k++;
                            counter++;
                            indexx[k] = index_x2;
                            indexy[k] = index_y2;
                            k++;
                            counter++;
                            indexx[k] = index_x3;
                            indexy[k] = index_y3;
                            k++;
                            counter++;
                        }
                    }
                }

            }

            for (int i = 0; i < counter; i++)
            {
                for (int j = 0; j < counter; j++)
                {


                    if (((indexx[i] * 10) + indexy[i]) == ((indexx[j] * 10) + indexy[j]))
                    {
                        if (i != j)
                        {
                            indexx[j] = -1;
                            indexy[j] = -1;
                        }
                    }
                }
            }
        }
        if (counter > 2){ 
            if(t == 0)
            {
                t++;
                StartCoroutine(WaitForLevelSwitch());
            }
        }
    }
    void Update()
    {
        if (t == 0)
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
        intarr = sendarr();
        sendindex_i = intarr[0];

        sendindex_j = intarr[1];
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

        intarr = sendarr();
        sendindex_i = intarr[0];

        sendindex_j = intarr[1];
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
            index_x1 = index_i / 2;
        }
        else
        {
            index_x1 = (index_i / 2) + 1;
        }
        index_y1 = index_j;
        index_x2 = index_x1;
        index_y2 = index_j + 1;
        if ((index_i % 2) == 0)
        {
            index_x3 = (index_i / 2) + 1;
        }
        else
        {
            index_x3 = (index_i / 2);
        }
        if ((index_i % 2) == 0)
        {
            if ((index_j % 2) == 0)
            {
                index_y3 = index_j + 1;
            }
            else
            {
                index_y3 = index_j;
            }
        }
        else
        {
            if ((index_j % 2) == 0)
            {
                index_y3 = index_j;
            }
            else
            {
                index_y3 = index_j + 1;
            }
        }
    }
    void CreateHexagonMap()
    {
        for (int i = 0; i < hexagonheight; i++)
        {
                for (int j = 0; j < hexagonweight; j++)
                {
                    float xPos = i * a;
                    if (j % 2 != 1)
                    {
                        xPos += a / 2f;
                    }
               
                     
                myPrefab.name = "hex" + i + j;
                myVector = new Vector3(j * b - 2.1f , xPos - 2.5f, 0);
                myPrefabList[i, j] = Instantiate(myPrefab, myVector, Quaternion.identity, GameObject.FindGameObjectWithTag("HexMap").transform);
                mr = myPrefab.GetComponent<SpriteRenderer>();

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
            mr.color = Color.red;
        }
        else if (givecolor == 1)
        {
            mr.color = Color.green;
        }
        else if (givecolor == 2)
        {
            mr.color = Color.blue;
        }
        else if (givecolor == 3)
        {
            mr.color = Color.cyan;
        }
        else if (givecolor == 4)
        {
            mr.color = Color.yellow;
        }
        else if (givecolor == 5)
        {
            mr.color = Color.gray;
        }
        else if (givecolor == 6)
        {
            mr.color = Color.black;
        }
    }
    void updateHexagon()
    {
        sýrala();


        for (int i = 0; i< counter; i++)
        {
            int indexxx = indexx[i];
            int indexxy = indexy[i];

            if (indexx[i] != -1)
            {
                holdvec = myPrefabList[hexagonheight - 1, indexxy].transform.position;
                holdName.name = myPrefabList[hexagonheight - 1, indexxy].name;

                for (int j = hexagonheight - 1; j > indexxx; j--)
                {
                    myPrefabList[j, indexxy].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[j, indexxy].transform.position, myPrefabList[j-1, indexxy].transform.position, 1f);
                    myPrefabList[j, indexxy].name = myPrefabList[j-1, indexxy].name;
                    
                }

                myPrefabList[indexxx, indexxy].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[indexxx, indexxy].transform.position, holdvec, 1f);
                myPrefabList[indexxx, indexxy].name = holdName.name;
                myPrefabList[indexxx, indexxy].GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        for (int i = 0; i < hexagonheight; i++)
        {
            for (int j = 0; j < hexagonweight ; j++)
            {
                myPrefabList[i,j] = GameObject.Find("HexMap").transform.Find("hex" + (i) + (j) + "(Clone)").gameObject;
            }
        }
        k = 0;
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
                    if (indexx[j] < indexx[t])
                    {
                        hold1 = indexx[j];
                        hold2 = indexy[j];

                        indexx[j] = indexx[t];
                        indexy[j] = indexy[t];

                        indexx[t] = hold1;
                        indexy[t] = hold2;
                    }
                    else if (indexx[j] == indexx[t])
                    {
                        if (indexy[j] < indexy[t])
                        {
                            hold1 = indexx[j];
                            hold2 = indexy[j];

                            indexx[j] = indexx[t];
                            indexy[j] = indexy[t];

                            indexx[t] = hold1;
                            indexy[t] = hold2;
                        }
                    }
                }
            }
        }
    }
    public void counterclockwiseRotation()
    {

        holdName.name = myPrefabList[index_x1, index_y1].name;

        holdVector = myPrefabList[index_x1, index_y1].transform.position;
        myPrefabList[index_x1, index_y1].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[index_x1, index_y1].transform.position, myPrefabList[index_x2, index_y2].transform.position, 1f);
        myPrefabList[index_x1, index_y1].name = myPrefabList[index_x2, index_y2].name;

        myPrefabList[index_x2, index_y2].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[index_x2, index_y2].transform.position, myPrefabList[index_x3, index_y3].transform.position, 1f);
        myPrefabList[index_x2, index_y2].name = myPrefabList[index_x3, index_y3].name;

        myPrefabList[index_x3, index_y3].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[index_x3, index_y3].transform.position, holdVector, 1f);
        myPrefabList[index_x3, index_y3].name = holdName.name;

        holdObj = myPrefabList[index_x2, index_y2];

        myPrefabList[index_x2, index_y2] = myPrefabList[index_x1, index_y1];
        myPrefabList[index_x1, index_y1] = myPrefabList[index_x3, index_y3];
        myPrefabList[index_x3, index_y3] = holdObj;



    }
    public void clockwiseRotation()
    {
        holdName.name = myPrefabList[index_x1, index_y1].name;

        holdVector = myPrefabList[index_x1, index_y1].transform.position;

        myPrefabList[index_x1, index_y1].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[index_x1, index_y1].transform.position, myPrefabList[index_x3, index_y3].transform.position, 1f);
        myPrefabList[index_x1, index_y1].name = myPrefabList[index_x3, index_y3].name;

        myPrefabList[index_x3, index_y3].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[index_x3, index_y3].transform.position, myPrefabList[index_x2, index_y2].transform.position, 1f);
        myPrefabList[index_x3, index_y3].name = myPrefabList[index_x2, index_y2].name;
        myPrefabList[index_x2, index_y2].GetComponent<Transform>().position = Vector3.Lerp(myPrefabList[index_x2, index_y2].transform.position, holdVector, 1f);

        myPrefabList[index_x2, index_y2].name = holdName.name;

        holdObj = myPrefabList[index_x1, index_y1];

        myPrefabList[index_x1, index_y1] = myPrefabList[index_x2, index_y2];
        myPrefabList[index_x2, index_y2] = myPrefabList[index_x3, index_y3];
        myPrefabList[index_x3, index_y3] = holdObj;

    }
    void StartCutting()
    {
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        currentBladeTrail.GetComponent<Renderer>().enabled = false;
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
            TrailRenderer tr = currentBladeTrail.GetComponent(typeof(TrailRenderer)) as TrailRenderer;
            tr.endWidth = 0;
            float min = 500;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    float xx = cam.ScreenToWorldPoint(Input.mousePosition).x;
                    float yy = cam.ScreenToWorldPoint(Input.mousePosition).y;
                    float aa = beyazList[i, j].transform.position.x;
                    float bb = beyazList[i, j].transform.position.y;
                    float totalhipoxa = xx - aa;
                    totalhipoxa = totalhipoxa * totalhipoxa;
                    float totalhipoyb = yy - bb;
                    totalhipoyb = totalhipoyb * totalhipoyb;
                    float distancefar = totalhipoxa + totalhipoyb;
                    distancefar = (float)Math.Sqrt(distancefar);
                    if (distancefar < min)
                    {
                        min = distancefar;
                        var rig = beyazList[i, j].GetComponent<Rigidbody>();

                        stringname = beyazList[i, j].transform.name;

                        if (stringname.Length < 16)
                        {
                            chararr[0] = stringname[6];
                            chararr[1] = '0';
                            chararr[2] = stringname[7];
                            intarr[0] = (int)char.GetNumericValue(chararr[0]);
                            intarr[1] = (int)char.GetNumericValue(chararr[2]);
                        }
                        else
                        {
                            chararr[0] = stringname[6];
                            chararr[1] = stringname[7];
                            chararr[2] = stringname[8];
                            intarr[0] = (int)char.GetNumericValue(chararr[0]) * 10 + (int)char.GetNumericValue(chararr[1]);
                            intarr[1] = (int)char.GetNumericValue(chararr[2]);
                        }
                        sendarr();
                    }
                    currentBladeTrail.transform.SetParent(null);
                }
            }
            currentBladeTrail.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
            Destroy(currentBladeTrail, 2f);
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
                beyaz.name = "circle" + i + j;
                mysecondVector = new Vector3((myPrefabList[index_x1, index_y1].transform.position.x + myPrefabList[index_x2, index_y2].transform.position.x + myPrefabList[index_x3, index_y3].transform.position.x) / 3, (myPrefabList[index_x1, index_y1].transform.position.y +myPrefabList[index_x2, index_y2].transform.position.y +myPrefabList[index_x3, index_y3].transform.position.y) / 3, -1);
                beyazList[i, j] = Instantiate(beyaz, mysecondVector, Quaternion.identity, GameObject.FindGameObjectWithTag("CircleMap").transform);
            }
        }
    }
    static public int[] sendarr()
    {
        return intarr;
    }
    IEnumerator WaitForLevelSwitch()
    {
        yield return new WaitForSeconds(1);
        int atb;
        int ata;
        for (int i = 0; i < counter; i++)
        {
            ata = indexx[i];
            atb = indexy[i];
            if (ata != -1)
            {
                Point++;
                myPrefabList[ata, atb].GetComponent<SpriteRenderer>().enabled = false;
                mr = myPrefabList[ata, atb].GetComponent<SpriteRenderer>();
                
                chooseColor();
            }

        }
                PointScreen();
                updateHexagon();
        t = 0;
        tu = 0;
    }
    void PointScreen()
    {
        point = "" + Point;
        GameObject.Find("Point").GetComponent<TextMesh>().text = point;
    }
}