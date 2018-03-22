using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MG_hexagonCell : MonoBehaviour
{
    [SerializeField]
    private bool flatTopped;
    [SerializeField]
    private int gameCoordinate_x;//Игровые координаты По горизонтали PRIVATE
    [SerializeField]
    private int gameCoordinate_y;//Игровые координаты По вертикали   PRIVATE
    [SerializeField]
    private bool isSelectedByMouse = false;//Если клинкули мышкой
    [SerializeField]
    private Vector3 editorCoordinate; //Позиция на экране
    [SerializeField]
    private Renderer rend;   //Для смена цвета
    [SerializeField]
    private Text label; //Текст клетки
    [SerializeField]
    private int moveCost;
    [SerializeField]
    private bool coordStyle_isVar1; //Для более корректного взятия соседних клеток по координатам из массива

    [SerializeField]
    private bool walkable;

    [SerializeField]
    //private List<MG_hexagonCell> neighbourList;
    private MG_hexagonCell[] neighbourArray;
    [SerializeField]
    private MG_Grid gridCell;

    void Awake()
    {
        Rend = GetComponent<Renderer>();
    }

    public int GameCoordinate_x
    {
        get
        {
            return gameCoordinate_x;
        }

        set
        {
            gameCoordinate_x = value;
        }
    }

    public int GameCoordinate_y
    {
        get
        {
            return gameCoordinate_y;
        }

        set
        {
            gameCoordinate_y = value;
        }
    }

    public Vector3 EditorCoordinate
    {
        get
        {
            return editorCoordinate;
        }

        set
        {
            editorCoordinate = value;
        }
    }

    public Text Label
    {
        get
        {
            return label;
        }

        set
        {
            label = value;
        }
    }

    public Renderer Rend
    {
        get
        {
            return rend;
        }

        set
        {
            rend = value;
        }
    }

    public bool IsSelectedByMouse
    {
        get
        {
            return isSelectedByMouse;
        }

        set
        {
            isSelectedByMouse = value;
        }
    }


    public int MoveCost
    {
        get
        {
            return moveCost;
        }

        set
        {
            moveCost = value;
        }
    }

    public bool CoordStyle_isVar1
    {
        get
        {
            return coordStyle_isVar1;
        }

        set
        {
            coordStyle_isVar1 = value;
        }
    }

    public MG_hexagonCell[] NeighbourArray
    {
        get
        {
            return neighbourArray;
        }

        set
        {
            neighbourArray = value;
        }
    }

    public MG_Grid GridCell
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

    public bool FlatTopped
    {
        get
        {
            return flatTopped;
        }

        set
        {
            flatTopped = value;
        }
    }

    public bool Walkable
    {
        get
        {
            return walkable;
        }

        set
        {
            walkable = value;
        }
    }



    //===========================================МЕТОДЫ КЛАССА

    public void SetCoordinates(int x, int y)
    {
        GameCoordinate_x = x;
        GameCoordinate_y = y;
    }

    void OnMouseEnter()
    {
        // if (!isSelectedByMouse)
        //    Rend.material.color = Color.red;
    }
    void OnMouseOver()
    {
        //rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime; //Круо
        if (!IsSelectedByMouse)
            Rend.material.color = Color.yellow;
        //rend.material.color -= Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
    }
    void OnMouseExit()
    {
        if (!IsSelectedByMouse)
            Rend.material.color = Color.white;
    }

    //BEGIN Используется в MG_KeyboardMouseControl для изменения цвета клетки после клика
    public void SelectedByMouse()
    {
        if (!IsSelectedByMouse)
        {
            IsSelectedByMouse = true;
            Rend = GetComponent<Renderer>();
            Rend.material.color = Color.red;
        }
    }
    public void CancelSelect()
    {
        IsSelectedByMouse = false;
        Rend.material.color = Color.white;
    }
    //END Используется в MG_KeyboardMouseControl для изменения цвета клетки после клика


    public void SetNeigbrours()
    {
        NeighbourArray = new MG_hexagonCell[6];

        int x_plus = gameCoordinate_x + 1;
        int x_minus = gameCoordinate_x - 1;

        int y_plus = gameCoordinate_y + 1;
        int y_minus = gameCoordinate_y - 1;

        bool NotoutOfRange_x_plus = (GridCell.X > x_plus);
        bool NotoutOfRange_x_minus = (0 <= x_minus);

        bool NotoutOfRange_y_plus = (GridCell.Y > y_plus);
        bool NotoutOfRange_y_minus = (0 <= y_minus);



        if (FlatTopped)
        {

            if (CoordStyle_isVar1)
            {
                if (NotoutOfRange_x_minus)
                    NeighbourArray[0] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y];

                if (NotoutOfRange_x_minus && NotoutOfRange_y_plus)
                    NeighbourArray[1] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y + 1];

                if (NotoutOfRange_y_plus)
                    NeighbourArray[2] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y + 1];

                if (NotoutOfRange_x_plus)
                    NeighbourArray[3] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y];

                if (NotoutOfRange_y_minus)
                    NeighbourArray[4] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y - 1];

                if (NotoutOfRange_x_minus && NotoutOfRange_y_minus)
                    NeighbourArray[5] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y - 1];
            }
            else
            {
                if (NotoutOfRange_x_minus)
                    NeighbourArray[0] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y];

                if (NotoutOfRange_y_plus)
                    NeighbourArray[1] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y + 1];

                if (NotoutOfRange_x_plus && NotoutOfRange_y_plus)
                    NeighbourArray[2] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y + 1];

                if (NotoutOfRange_x_plus)
                    NeighbourArray[3] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y];

                if (NotoutOfRange_x_plus && NotoutOfRange_y_minus)
                    NeighbourArray[4] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y - 1];

                if (NotoutOfRange_y_minus)
                    NeighbourArray[5] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y - 1];
            }

        }
        else
        {

            if (CoordStyle_isVar1)
            {
                if (NotoutOfRange_x_minus && NotoutOfRange_y_minus)
                    NeighbourArray[0] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y - 1];

                if (NotoutOfRange_x_minus)
                    NeighbourArray[1] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y];

                if (NotoutOfRange_y_plus)
                    NeighbourArray[2] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y + 1];

                if (NotoutOfRange_x_plus)
                    NeighbourArray[3] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y];

                if (NotoutOfRange_x_plus && NotoutOfRange_y_minus)
                    NeighbourArray[4] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y - 1];

                if (NotoutOfRange_y_minus)
                    NeighbourArray[5] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y - 1];
            }
            else
            {
                if (NotoutOfRange_x_minus)
                    NeighbourArray[0] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y];

                if (NotoutOfRange_x_minus && NotoutOfRange_y_plus)
                    NeighbourArray[1] = GridCell.ArrayOfCells_2D[gameCoordinate_x - 1, gameCoordinate_y + 1];

                if (NotoutOfRange_y_plus)
                    NeighbourArray[2] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y + 1];

                if (NotoutOfRange_x_plus && NotoutOfRange_y_plus)
                    NeighbourArray[3] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y + 1];

                if (NotoutOfRange_x_plus)
                    NeighbourArray[4] = GridCell.ArrayOfCells_2D[gameCoordinate_x + 1, gameCoordinate_y];

                if (NotoutOfRange_y_minus)
                    NeighbourArray[5] = GridCell.ArrayOfCells_2D[gameCoordinate_x, gameCoordinate_y - 1];
            }
        }
    }

}
