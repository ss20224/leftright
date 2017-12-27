using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class map : MonoBehaviour {
    
    public GameObject baseCube;
    private cubeControl[] Cubes;

    private int Cubelimimt = 10;
    private int maxCube ;
    private bool bSetFirstRoad = false;

    private int nowPlayerAdress = 0;
    private int roadAdress = 0;

    private bool bMakeMapX = false;
    private bool bMakeMapY = false;
    private int mapX = 0;
    private int mapY = 0;

    private bool bLife = true;

    public GameObject player;

	// Use this for initialization
	void Start () {
        resetMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && bLife)
        {
            if (nowPlayerAdress + Cubelimimt < maxCube)
            {

            }
            else
            {
                nowPlayerAdress -= maxCube;
            }

            nowPlayerAdress += Cubelimimt;
            player.transform.localPosition += new Vector3(0, 0, 1);
            this.transform.localPosition -= new Vector3(0, 0, 1);

            if (bMakeMapX)
            {
                DisRoad(0);
            }
            else if ((nowPlayerAdress / Cubelimimt) > 3)
            {
                bMakeMapX = true;
                DisRoad(0);
            }
            check();

        }
        else if (Input.GetKeyDown(KeyCode.D) && bLife)
        {
            if ((nowPlayerAdress + 1) % Cubelimimt != 0)
            {

            }
            else
            {
                nowPlayerAdress -= Cubelimimt;
            }

            nowPlayerAdress += 1;
            player.transform.localPosition += new Vector3(1, 0, 0);
            this.transform.localPosition -= new Vector3(1, 0, 0);

            if (bMakeMapY)
            {
                DisRoad(1);
            }
            else if ((nowPlayerAdress % 10) > 3)
            {
                bMakeMapY = true;
                DisRoad(1);
            }
            check();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetMap();
        }
    }

    void setFirstRoad() {
        System.Random crandRandom = new System.Random(Guid.NewGuid().GetHashCode());
        int way = crandRandom.Next(2);  // 方向 0 向左+10  方向 1 向右 +1

        if (way == 0)
        {
                if (roadAdress + Cubelimimt < maxCube)
                {
                    roadAdress += Cubelimimt;
                    Cubes[roadAdress].changeMode(cubeControl.cubeMode.noraml);
                }
                else
                {
                    bSetFirstRoad = true;
                }
        }
        else
        {
                if ((roadAdress + 1)% Cubelimimt != 0)
                {
                    roadAdress += 1;
                    Cubes[roadAdress].changeMode(cubeControl.cubeMode.noraml);
                }
                else
                {
                    bSetFirstRoad = true;
                }
        }

        if (!bSetFirstRoad)
            setFirstRoad();
    }

    void setNewRoad(int lastWay)
    {
        System.Random crandRandom = new System.Random(Guid.NewGuid().GetHashCode());
        int way = crandRandom.Next(2);  // 方向 0 向左+10  方向 1 向右 +1

        if (way == 0)
        {
            if (((roadAdress+ Cubelimimt)/ Cubelimimt)% Cubelimimt != mapX)
            {
                roadAdress += Cubelimimt;
                if (roadAdress > maxCube)
                    roadAdress = roadAdress % maxCube;
                Cubes[roadAdress].changeMode(cubeControl.cubeMode.noraml);
                Debug.Log(roadAdress);
                setNewRoad(0);
            }
        }
        else if(way == 1)
        {
            if ((roadAdress + 1) % Cubelimimt != mapY)
            {
                roadAdress += 1;
                if ((roadAdress )% Cubelimimt == 0)
                    roadAdress = roadAdress - Cubelimimt;
                Cubes[roadAdress].changeMode(cubeControl.cubeMode.noraml);
                Debug.Log(roadAdress);
                setNewRoad(1);
            }
        }
        
    }

    void DisRoad(int way)
    {
        if (way == 0)
        {
            for (int x =0 ; x < Cubelimimt; x++ )
            {
                Cubes[mapX* Cubelimimt + x].changeMode(cubeControl.cubeMode.close);
                Cubes[mapX* Cubelimimt + x].transform.localPosition = Cubes[(mapX* Cubelimimt + x+ (Cubelimimt-1)* Cubelimimt) %maxCube].transform.localPosition + new Vector3(0,0,1);
            }
            mapX++;
            if (mapX == Cubelimimt)
                mapX = 0;
            setNewRoad(0);
        }
        else
        {
            for (int x = 0; x < Cubelimimt; x++)
            {
                Cubes[mapY + x* Cubelimimt].changeMode(cubeControl.cubeMode.close);
                Cubes[mapY + x* Cubelimimt].transform.localPosition = Cubes[x * Cubelimimt+(mapY-1+ Cubelimimt) % Cubelimimt].transform.localPosition + new Vector3(1, 0, 0);
            }
            mapY++;
            if (mapY == Cubelimimt)
                mapY = 0;
            setNewRoad(1);
        }

        
    }

    void check()
    {
        if (Cubes[nowPlayerAdress].checkLive())
        {

        }
        else
        {
            bLife = false;
            Debug.Log("die");
        }
    }

    void resetMap()
    {
        if (Cubes != null && Cubes.Length != 0) {
            for (int x= Cubes.Length-1; x >=0; x--)
            {
                DestroyObject(Cubes[x].gameObject);
            }
        }
        player.transform.localPosition = new Vector3(0, 1, 0);
        this.transform.localPosition = Vector3.zero;
        maxCube = Cubelimimt * Cubelimimt;
        Cubes = new cubeControl[maxCube];
        for (int x = 0; x < Cubelimimt; x++)
        {
            for (int y = 0; y < Cubelimimt; y++)
            {
                GameObject obj = GameObject.Instantiate(baseCube);
                obj.transform.parent = this.transform;
                obj.transform.localPosition = new Vector3(y, 0, x);
                obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                obj.name = "cube" + x.ToString() + y.ToString();
                Cubes[x * Cubelimimt + y] = obj.GetComponent<cubeControl>();
                Cubes[x * Cubelimimt + y].changeMode(cubeControl.cubeMode.close);
            }
        }
        nowPlayerAdress = 0;
        roadAdress = 0;

        bMakeMapX = false;
        bMakeMapY = false;
        mapX = 0;
        mapY = 0;
        bLife = true;
        Cubes[roadAdress].changeMode(cubeControl.cubeMode.noraml);
        bSetFirstRoad = false;
        setFirstRoad();
    }
}
