using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PathfindingModeGUI : MonoBehaviour
{

    [SerializeField]
    private bool checkBox_Disabled;
    [SerializeField]
    private bool checkBox_A_point;
    [SerializeField]
    private bool checkBox_B_point;
    [SerializeField]
    private bool checkBox_Water;
    [SerializeField]
    private bool checkBox_Grass;
    [SerializeField]
    private MG_Grid comp_gridCell;
    [SerializeField]
    private MG_KeyboardMouseControl comp_KeyboardMouseControl;

    [SerializeField]
    private int notWalkableChance = 43;


    public bool CheckBox_Disabled
    {
        get
        {
            return checkBox_Disabled;
        }

        set
        {
            checkBox_Disabled = value;
        }
    }

    public bool CheckBox_A_point
    {
        get
        {
            return checkBox_A_point;
        }

        set
        {
            checkBox_A_point = value;
        }
    }

    public bool CheckBox_B_point
    {
        get
        {
            return checkBox_B_point;
        }

        set
        {
            checkBox_B_point = value;
        }
    }

    public bool CheckBox_Water
    {
        get
        {
            return checkBox_Water;
        }

        set
        {
            checkBox_Water = value;
        }
    }

    public bool CheckBox_Grass
    {
        get
        {
            return checkBox_Grass;
        }

        set
        {
            checkBox_Grass = value;
        }
    }

    public MG_KeyboardMouseControl Comp_KeyboardMouseControl
    {
        get
        {
            return comp_KeyboardMouseControl;
        }

        set
        {
            comp_KeyboardMouseControl = value;
        }
    }

    public MG_Grid Comp_gridCell
    {
        get
        {
            return comp_gridCell;
        }

        set
        {
            comp_gridCell = value;
        }
    }

    public int NotWalkableChance
    {
        get
        {
            return notWalkableChance;
        }

        set
        {
            notWalkableChance = value;
        }
    }

    //----------------------------------МЕТОДЫ


    private void Start()
    {
        comp_KeyboardMouseControl = GameObject.FindWithTag("MG_MiniInterface_tag").GetComponent<MG_KeyboardMouseControl>();
        Comp_gridCell = GameObject.FindWithTag("MG_Grid_tag").GetComponent<MG_Grid>();
    }

    public void ButtonPressed_Refresh()
    {
        //Chosen_Disabled();

        foreach (MG_hexagonCell comp_cell in Comp_gridCell.ArrayOfCells_2D)
        {
            comp_cell.CancelSelect(); //Берем объект Клетки и вызываем в его скрипте Отмену подсветки
        }

        //BEGIN reset PATHFINDING       
        GetComponent<MG_Pathfinding>().Comp_StartCell = null; // reset обькета Стартовой клетки
        GetComponent<MG_Pathfinding>().Comp_TargetCell = null;  // reset обькета Цели клетки
                                                                //END reset PATHFINDING

        //*--ПЕРЕСОЗДАДИМ КАРТУ, ЧТОБЫ ВСЕ РЕСЕТНУТЬ!
        Comp_gridCell.RemoveAllCells();//
        Comp_gridCell.GenerateGrid();
        //*--
        print("ButtonPressed_Refresh: Карта пересоздана");
    }

    public void ButtonPressed_Start()
    {
        //GetComponent<MG_Pathfinding>().Start_Pathfinding(); //OLD

        MG_hexagonCell start = GetComponent<MG_Pathfinding>().Comp_StartCell;
        MG_hexagonCell end = GetComponent<MG_Pathfinding>().Comp_TargetCell;

        if (start && end)
        {
            List<MG_hexagonCell> way = GetComponent<MG_AP_Pathfinding>().FindPath(start, end);
        }
        else
        {
            Debug.LogWarning("ButtonPressed_Start(): Не задана начальная или контрольная точка!");
        }

    }

    public void ButtonPressed_Auto()
    {
        foreach (MG_hexagonCell cell in Comp_gridCell.ArrayOfCells_2D)
        {
            if (Random.Range(0, 100) < NotWalkableChance)
            {
                cell.IsSelectedByMouse = true;
                cell.Label.text = "X";
                cell.Rend.material.color = Color.grey;
                cell.Walkable = false;
            }

        }


        int x = Random.Range(0, comp_gridCell.X);
        int y = Random.Range(0, comp_gridCell.Y);
        GetComponent<MG_PathfindingCells>().SetCell_StartPoint(Comp_gridCell.ArrayOfCells_2D[x, y]);

        int x2;
        int y2;
        do
        {
            do
            {
                x2 = Random.Range(0, comp_gridCell.X);
                y2 = Random.Range(0, comp_gridCell.Y);

            } while ((x.Equals(x2)) && (y2.Equals(y)));
        } while ((x == x2) && (y2 == y));
        GetComponent<MG_PathfindingCells>().SetCell_TargetPoint(Comp_gridCell.ArrayOfCells_2D[x2, y2]);
    }

    public void Chosen_Disabled()
    {
        CheckBox_Disabled = true; //ВЫБРАН
        CheckBox_A_point = false;
        CheckBox_B_point = false;
        CheckBox_Water = false;
        CheckBox_Grass = false;
    }

    public void Chosen_A_point()
    {
        CheckBox_Disabled = false;
        CheckBox_A_point = true;//ВЫБРАН
        CheckBox_B_point = false;
        CheckBox_Water = false;
        CheckBox_Grass = false;
    }

    public void Chosen_B_point()
    {
        CheckBox_Disabled = false;
        CheckBox_A_point = false;
        CheckBox_B_point = true;//ВЫБРАН
        CheckBox_Water = false;
        CheckBox_Grass = false;
    }

    public void Chosen_Water()
    {
        CheckBox_Disabled = false;
        CheckBox_A_point = false;
        CheckBox_B_point = false;
        CheckBox_Water = true; //ВЫБРАН
        CheckBox_Grass = false;
    }

    public void Chosen_Grass()
    {
        CheckBox_Disabled = false;
        CheckBox_A_point = false;
        CheckBox_B_point = false;
        CheckBox_Water = false;
        CheckBox_Grass = true; //ВЫБРАН
    }
}
