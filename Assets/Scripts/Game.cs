using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
public class Game : MonoBehaviour
{

    private static int SCREEN_WIDTH = 100;
    private static int SCREEN_HEIGHT = 100;

    public bool simulationEnable = false;


    public float speed = 0.1f;
    private float timer = 0;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells(false);
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
        if(!simulationEnable) return;
        if(timer>=speed){
            timer=0f;
            CountNeighbros();
            PopulationControl();   
        }else{
            timer+= Time.deltaTime;
        }
    }
    void PlaceCells (bool rand){
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(rand?GetRandomAliveCell():false);
                
            }
        }
    }

       void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mousePoint.x);
            int y = Mathf.RoundToInt(mousePoint.y);

                if (x >= 0 && y >= 0 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
            {
                grid[x, y].SetAlive(!grid[x, y].isAlive);
            }
        }
        if(Input.GetKeyUp(KeyCode.P)){
            simulationEnable = !simulationEnable;
        }
        if(Input.GetKeyUp(KeyCode.W)){
            WriteGrid();
        }
        if(Input.GetKeyUp(KeyCode.R)){
            PlaceCells(true);
        }
        if(Input.GetKeyUp(KeyCode.A)){
            ReadGrid("Assets/Resources/grid.txt");
        }
        if(Input.GetKeyUp(KeyCode.O)){
            ReadGrid("Assets/Resources/osciladores.txt");
        }
        if(Input.GetKeyUp(KeyCode.E)){
            ReadGrid("Assets/Resources/estaticos.txt");
        }   
        if(Input.GetKeyUp(KeyCode.N)){
            ReadGrid("Assets/Resources/naves.txt");
        }
        if(Input.GetKeyUp(KeyCode.M)){
            ReadGrid("Assets/Resources/matusalenes.txt");
        }
    }

    void WriteGrid(){
        Debug.Log("Creando");
        StreamWriter writer = new StreamWriter("Assets/Resources/grid.txt", false);
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                writer.Write(grid[x, y].isAlive?1:0);
            }
            writer.WriteLine();
        }
        writer.Close();
        Debug.Log("Listo");
    }

    void ReadGrid(string path){
        string[] lines = File.ReadAllLines(path);
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            char[] values = lines[y].ToCharArray();
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                if(values[x]=='1'){
                    grid[x, y].SetAlive(true);
                }else{
                    grid[x, y].SetAlive(false);
                }
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