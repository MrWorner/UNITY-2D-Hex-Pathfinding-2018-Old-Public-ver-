using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class MG_Grid : MonoBehaviour
{
    [SerializeField]
    private GameObject hexPointyTopped;
    [SerializeField]
    private GameObject hexFlatToppe;
    [SerializeField]
    private GameObject selectedHexagonPrefab;
    [SerializeField]
    private Text cellLabelPrefab;
    [SerializeField]
    private Canvas gridCanvas;

    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    //[SerializeField]
    private MG_hexagonCell[,] arrayOfCells_2D;

    [SerializeField]
    private bool flatTopped = false;

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

    public MG_hexagonCell[,] ArrayOfCells_2D
    {
        get
        {
            return arrayOfCells_2D;
        }

        set
        {
            arrayOfCells_2D = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public int X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    public Canvas GridCanvas
    {
        get
        {
            return gridCanvas;
        }

        set
        {
            gridCanvas = value;
        }
    }

    public Text CellLabelPrefab
    {
        get
        {
            return cellLabelPrefab;
        }

        set
        {
            cellLabelPrefab = value;
        }
    }

    public GameObject SelectedHexagonPrefab
    {
        get
        {
            return selectedHexagonPrefab;
        }

        set
        {
            selectedHexagonPrefab = value;
        }
    }

    public GameObject HexFlatToppe
    {
        get
        {
            return hexFlatToppe;
        }

        set
        {
            hexFlatToppe = value;
        }
    }

    public GameObject HexPointyTopped
    {
        get
        {
            return hexPointyTopped;
        }

        set
        {
            hexPointyTopped = value;
        }
    }

    //=============================МЕТОДЫ

    //BEGIN ДЛЯ ТЕСТА, ЧТОБЫ ПЕРЕСОЗДАТЬ КАРТУ И ПОЛУЧИТЬ НЕ ПУСТЫЕ ПЕРЕМЕННЫЕ.
    void Awake()
    {
        RemoveAllCells();
        GenerateGrid();
        print("MG_Grid: КАРТА ПЕРЕСОЗДАНА, ЧТОБЫ ЗАПОЛНИТЬ ПЕРЕМЕННЫЕ");
    }
    //BEGIN ДЛЯ ТЕСТА, ЧТОБЫ ПЕРЕСОЗДАТЬ КАРТУ И ПОЛУЧИТЬ НЕ ПУСТЫЕ ПЕРЕМЕННЫЕ.


    public void GenerateGrid() //Создано для одного типа Hexagonal cell: Pointy Topped (другой Flat Topped возможен!)
    {
        GridCanvas = GetComponentInChildren<Canvas>();//Canvas для текста
        float x_Pos = 0;//Позиция по горизонтали для новой клетки
        float y_Pos = 0;//Позиция по вертикали для новой клетки
        int xy_fixer = 0;//Коррекция для правильной Hexagon карты (Значение: 0-1), чтобы выглядила как квадратная карта.

        //BEGIN Если выбран тип карты FlatToppe
        if (FlatTopped)
        {
            SelectedHexagonPrefab = HexFlatToppe;
        }
        else
        {
            SelectedHexagonPrefab = HexPointyTopped;
        }
        //END Если выбран тип карты FlatToppe, а не PointyTopped
        ArrayOfCells_2D = new MG_hexagonCell[Y, X];

        //BEGIN Default настрйоки для изменения типа карты, иначе будут глюки
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(0, 0, 0);
        //END Default настрйоки для изменения типа карты, иначе будут глюки

        for (int i = 0; i < Y; i++)
        {

            if (FlatTopped)
            {
                x_Pos = 0;//Для создания правильной квадратной карты  с Hex
                y_Pos = 0 + (-5f) * (i - 1);// + (-5f) * (i - 1);//(y_Pos + - 5f) * (i-1);//Для создания правильной квадратной карты  с Hex (след строка)
                xy_fixer = 0;
            }
            else
            {
                x_Pos = 2.5f * xy_fixer;//Для создания правильной квадратной карты  с Hex
                y_Pos = 4.25f * i * (-1);//Для создания правильной квадратной карты  с Hex (след строка)
            }

            for (int j = 0; j < X; j++)
            {
                this.transform.localScale = new Vector3(1, 1, 1);//Scale самой карты (можно поменять в конце метода если необходимо)

                //BEGIN Создание клетки объекта и его настройка                
                GameObject hexagon = Instantiate(SelectedHexagonPrefab); //Создание самой клетки
                ArrayOfCells_2D[i, j] = hexagon.GetComponent<MG_hexagonCell>();    //кладем в массив
                hexagon.transform.parent = this.transform; //Вставляем данный child в Parent
                hexagon.transform.position = new Vector3(x_Pos, y_Pos, 0); //позиция клетки на карте
                hexagon.transform.localScale = new Vector3(1, 1, 1);    //масштаб клетки

                hexagon.GetComponent<MG_hexagonCell>().GameCoordinate_x = i; //Задаем игровую координату по X
                hexagon.GetComponent<MG_hexagonCell>().GameCoordinate_y = j; //Задаем игровую координату по Y
                hexagon.GetComponent<MG_hexagonCell>().EditorCoordinate = hexagon.transform.position; //Задаем реальную позицию в редакторе
                hexagon.GetComponent<MG_hexagonCell>().GridCell = this;//Назначаем саму карту
                hexagon.GetComponent<MG_hexagonCell>().FlatTopped = FlatTopped;//Вид Hexagon
                hexagon.GetComponent<MG_hexagonCell>().Walkable = true;//Можно ли ходить (для PATHFINDING) 08.03.2018       
                hexagon.name = "Hexagon (" + i + ", " + j + ")"; //Задаем имя объекту

                //-Для лучшего способа получение соседей, чтобы не перепрыгивать через соседа (тот самый баг для Хексагонов).
                if (xy_fixer == 0) hexagon.GetComponent<MG_hexagonCell>().CoordStyle_isVar1 = true;
                else hexagon.GetComponent<MG_hexagonCell>().CoordStyle_isVar1 = false;
                //-end

                //END Создание клетки объекта и его настройка

                //BEGIN Метка координатная на клетке
                Text label = Instantiate<Text>(CellLabelPrefab); //Создание самой метки
                label.rectTransform.SetParent(GridCanvas.transform, false); //Задаем Parent
                label.rectTransform.anchoredPosition = //Задаем позицию
                    new Vector2(x_Pos, y_Pos);
                label.text = i + ", " + j; //Задаем текст
                hexagon.GetComponent<MG_hexagonCell>().Label = label; //Задаем клетки данный текст в память для будущей интеракции
                label.name = "Label (" + i + ", " + j + ")"; //Задаем имя объекту
                //END Метка координатная на клетке

                if (FlatTopped)
                {
                    x_Pos += 4.25f;//Корректировка для следующей клетки
                    if (xy_fixer == 0)
                    {
                        y_Pos += -2.49f;//Корректировка для следующей клетки
                        xy_fixer = 1;
                    }
                    else
                    {
                        y_Pos += 2.49f;//Корректировка для следующей клетки
                        xy_fixer = 0;
                    }
                }
                else
                {
                    x_Pos += 5f;//Корректировка для следующей клетки
                }
                //hexagon.GetComponent<Hexagon>().OffsetCoord = new Vector2(0, 0);       
            }

            //BEGIN Специальная коррекция для Hexagon карты, чтобы выглядила как квадратная карта
            if (FlatTopped)
            {
                // y_Pos += -5f;
            }
            else
            {
                if (xy_fixer == 0)
                    xy_fixer = 1;
                else
                    xy_fixer = 0;
            }
            //END Специальная коррекция для Hexagon карты, чтобы выглядила как квадратная карта
        }

        SetAllNeigbroursForAllCell();//Для установки соседей для всех клеток после генерации карты
    }


    private void SetAllNeigbroursForAllCell()//Для установки соседей для всех клеток после генерации карты
    {
        foreach (MG_hexagonCell comp_cell in ArrayOfCells_2D)
        {
            comp_cell.SetNeigbrours();
        }
    }



    public void RemoveAllCells()
    {
        //BEGIN Удаляем сами объекты клетки
        var children = new List<GameObject>();
        foreach (Transform cell in this.transform)
        {
            if (cell.gameObject.CompareTag("MG_cell_tag"))
                children.Add(cell.gameObject);
        }
        children.ForEach(c => DestroyImmediate(c));
        //END Удаляем сами объекты клетки

        //BEGIN Удаляем координатные метки у удаленных клеток
        GridCanvas = GetComponentInChildren<Canvas>();
        var childrenText = new List<GameObject>();
        foreach (Transform textLabel in GridCanvas.transform)
        {
            childrenText.Add(textLabel.gameObject);
        }
        childrenText.ForEach(c => DestroyImmediate(c));
        //END Удаляем координатные метки у удаленных клеток

        SelectedHexagonPrefab = null;
    }

    public void SnapUnitsToGrid()
    {
        List<Transform> cells = new List<Transform>();

        foreach (Transform cellObj in this.transform)
        {
            if (cellObj.CompareTag("MG_cell_tag"))
                cells.Add(cellObj.transform);
        }

        GameObject[] AllUnits = GameObject.FindGameObjectsWithTag("MG_unit_tag");
        GameObject[] AllBuildings = GameObject.FindGameObjectsWithTag("MG_building_tag");
        AllUnits.Concat(AllBuildings);


        foreach (GameObject unitObj in AllUnits)
        {
            GameObject closestCell = null;
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = unitObj.transform.position;
            foreach (Transform t in cells)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                    closestCell = t.gameObject;
                }
            }
            unitObj.transform.position = tMin.position;
            //closestCell.GetComponent<MG_hexagonCell>().SelectedByMouse();
        }

    }

}