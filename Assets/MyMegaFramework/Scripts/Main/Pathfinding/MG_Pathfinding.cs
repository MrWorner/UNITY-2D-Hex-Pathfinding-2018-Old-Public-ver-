using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Pathfinding : MonoBehaviour
{
    [SerializeField]
    private int coord_start_x; //Начальная точка (по координате X)
    [SerializeField]
    private int coord_start_y; //Начальная точка (по координате Y)
    [SerializeField]
    private int coord_target_x; //Конечная точка (по координате X)
    [SerializeField]
    private int coord_target_y; //Конечная точка (по координате X)

    [SerializeField]
    private bool errorFound;
    [SerializeField]
    private string textResult;

    [SerializeField]
    private MG_hexagonCell comp_StartCell; //Начальная точка
    [SerializeField]
    private MG_hexagonCell comp_TargetCell;  //Конечная точка

    [SerializeField]
    GameObject gridCell;

    public int Coord_start_x
    {
        get
        {
            return coord_start_x;
        }

        set
        {
            coord_start_x = value;
        }
    }

    public int Coord_start_y
    {
        get
        {
            return coord_start_y;
        }

        set
        {
            coord_start_y = value;
        }
    }

    public int Coord_target_x
    {
        get
        {
            return coord_target_x;
        }

        set
        {
            coord_target_x = value;
        }
    }

    public int Coord_target_y
    {
        get
        {
            return coord_target_y;
        }

        set
        {
            coord_target_y = value;
        }
    }

    public bool ErrorFound
    {
        get
        {
            return errorFound;
        }

        set
        {
            errorFound = value;
        }
    }

    public string TextResult
    {
        get
        {
            return textResult;
        }

        set
        {
            textResult = value;
        }
    }

    public MG_hexagonCell Comp_StartCell
    {
        get
        {
            return comp_StartCell;
        }

        set
        {
            comp_StartCell = value;
        }
    }

    public MG_hexagonCell Comp_TargetCell
    {
        get
        {
            return comp_TargetCell;
        }

        set
        {
            comp_TargetCell = value;
        }
    }

    public GameObject GridCell
    {
        get
        {
            return gridCell;
        }

        set
        {
            gridCell = value;
        }
    }

    //===================================== МЕТОДЫ

    private void Start()
    {
        GridCell = GameObject.FindWithTag("MG_Grid_tag");
    }

    public void Start_Pathfinding() //Вызывается в кнопке "START"
    {
        ErrorFound = false;
        if (!Comp_StartCell) ErrorFound = true;
        if (!Comp_TargetCell) ErrorFound = true;
        TextResult = "";

        if (ErrorFound)
        {
            TextResult = "Start_Pathfinding(): Начальная или конечная точка не задана для Pathfinding'а; (НЕ ЗАДАНЫ ОБЪЕКТЫ!)";
            Debug.LogWarning(TextResult);
        }
        else
        {
            InitPathfinding(); //Настраиваем Pathfinding
            CycleSearch();
            Debug.Log("Start_Pathfinding(): ВЫПОЛНЕНИЕ ЗАКОНЧЕНО!");
        };
    }

    private void InitPathfinding()
    {

        //BEGIN Берем данные из стартовой клетки
        Coord_start_x = Comp_StartCell.GameCoordinate_x;
        Coord_start_y = Comp_StartCell.GameCoordinate_y;
        //print("initPathfinding() Starting point: " + currentCell_startPoint.transform.gameObject.name);
        //END Берем данные из стартовой клетки

        //BEGIN Берем данные из конечной клетки   
        Coord_target_x = Comp_TargetCell.GameCoordinate_x;
        Coord_target_y = Comp_TargetCell.GameCoordinate_y;
        //print("initPathfinding() Target point: " + currentCell_endPoint.transform.gameObject.name);
        //END Берем данные из конечной клетки
    }


    private void CycleSearch()
    {
        bool targetFound = false;
        int currentLevel = 0;

        //Comp_StartCell.Label.text = "" + currentLevel + "";
        Comp_StartCell.Rend.material.color = Color.gray;


        //print("distance_x = " + distance_x + " | distance_y = " + distance_y);

        int move_x;
        int move_y;
        do
        {
            int distance_x = Coord_target_x - Coord_start_x;
            int distance_y = Coord_target_y - Coord_start_y;

            if (Coord_target_x > Coord_start_x) move_x = 1;
            else if (Coord_target_x < Coord_start_x) move_x = -1;
            else move_x = 0;

            if (Coord_target_y > Coord_start_y) move_y = 1;
            else if (Coord_target_y < Coord_start_y) move_y = -1;
            else move_y = 0;

            print("currentLevel = " + currentLevel + "=============");
            print(" move_x = " + move_x + " move_y = " + move_y);
            print(" distance_x = " + distance_x + " distance_y = " + distance_y);

            currentLevel++;

            CycleSearch2_currentCell(currentLevel, move_x, move_y);

            Coord_start_x = Comp_StartCell.GameCoordinate_x;
            Coord_start_y = Comp_StartCell.GameCoordinate_y;
            print("Current coord |" + " Coord_start_x = " + Coord_start_x + " Coord_start_y = " + Coord_start_y);

            if (move_x == 0 && move_y == 0) targetFound = true;

        } while (currentLevel < 200 && !targetFound);//(keepSearching);

    }

    private void CycleSearch2_currentCell(int currentLevel, int move_x, int move_y)
    {
        Comp_StartCell = GridCell.GetComponent<MG_Grid>().ArrayOfCells_2D[Coord_start_x + move_x, Coord_start_y + move_y].GetComponent<MG_hexagonCell>();
        //Comp_StartCell.Label.text = "" + currentLevel + "";
        Comp_StartCell.Rend.material.color = Color.magenta;
        Comp_StartCell.IsSelectedByMouse = true;
    }



}
