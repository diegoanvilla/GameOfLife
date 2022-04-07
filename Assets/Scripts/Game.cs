using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private static int SCREEN_WIDTH = 100;
    private static int SCREEN_HEIGHT = 100;

    public float speed = 0.1f;
    private float timer = 0;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>=speed){
            timer=0f;
            CountNeighbros();
            PopulationControl();   
        }else{
            timer+= Time.deltaTime;
        }
    }
    void PlaceCells (){
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(GetRandomAliveCell());
                
            }
        }
    }

    void CountNeighbros(){
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;
                if(y+1<SCREEN_HEIGHT){
                    if(grid[x, y+1].isAlive){
                        numNeighbors++;
                    }
                }
                if(y-1>=0){
                    if(grid[x, y-1].isAlive){
                        numNeighbors++;
                    }
                }
                if(x+1<SCREEN_WIDTH){
                    if(grid[x+1, y].isAlive){
                        numNeighbors++;
                    }
                }
                if(x-1>=0){
                    if(grid[x-1, y].isAlive){
                        numNeighbors++;
                    }
                }
                if (x+1<SCREEN_WIDTH&&y+1<SCREEN_HEIGHT)
                {
                    if(grid[x+1, y+1].isAlive){
                        numNeighbors++;
                    }
                }
                if (x-1>=0&&y+1<SCREEN_HEIGHT)
                {
                    if(grid[x-1, y+1].isAlive){
                        numNeighbors++;
                    }
                }
                if (x-1>=0&&y-1>=0)
                {
                    if(grid[x-1, y-1].isAlive){
                        numNeighbors++;
                    }
                }
                if (x+1<SCREEN_WIDTH&&y-1>=0)
                {
                    if(grid[x+1, y-1].isAlive){
                        numNeighbors++;
                    }
                }
                grid[x, y].numNeighbors = numNeighbors;
            }
        }
    }

    void PopulationControl(){
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                if(grid[x, y].isAlive){
                    if(grid[x, y].numNeighbors!=2&&grid[x, y].numNeighbors!=3) grid[x, y].SetAlive(false);
                }else{
                    if(grid[x, y].numNeighbors==3)grid[x, y].SetAlive(true);
                }

            }
        }
    }

    bool GetRandomAliveCell(){
        int rand = UnityEngine.Random.Range(0, 100);
        if(rand>75)return true;
        return false;
    }
}