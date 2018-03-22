using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
public class MG_AP_Pathfinding : MonoBehaviour
{

    [SerializeField]
    private MG_Grid comp_gridCell;
    [SerializeField]
    private GameObject prefab_pathNode;
    //[SerializeField]
    private MG_AP_PathNode[] arrayOfNodes;

    [SerializeField]
    private int totalVisitedCells;

    [SerializeField]
    private int step;

    [SerializeField]
    int totalCellcount;
    //============

    public MG_AP_PathNode[] ArrayOfNodes
    {
        get
        {
            return arrayOfNodes;
        }

        set
        {
            arrayOfNodes = value;
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

    public GameObject Prefab_pathNode
    {
        get
        {
            return prefab_pathNode;
        }

        set
        {
            prefab_pathNode = value;
        }
    }

    public int TotalVisitedCells
    {
        get
        {
            return totalVisitedCells;
        }

        set
        {
            totalVisitedCells = value;
        }
    }

    public int Step
    {
        get
        {
            return step;
        }

        set
        {
            step = value;
        }
    }

    public int TotalCellcount
    {
        get
        {
            return totalCellcount;
        }

        set
        {
            totalCellcount = value;
        }
    }


    //==================================================МЕТОДЫ

    private void Start()
    {    
        Comp_gridCell = GameObject.FindWithTag("MG_Grid_tag").GetComponent<MG_Grid>();
        TotalCellcount = (Comp_gridCell.Y * Comp_gridCell.X);
        ArrayOfNodes = new MG_AP_PathNode[TotalCellcount];       
    }


    private void RemoveAllNodes()
    {
        //BEGIN Удаляем сами объекты нодов
        var children = new List<GameObject>();
        foreach (Transform cell in this.transform)
        {
                children.Add(cell.gameObject);
        }
        children.ForEach(c => DestroyImmediate(c));
        //END Удаляем сами объекты нодов
    }

    public List<MG_hexagonCell> FindPath(MG_hexagonCell start, MG_hexagonCell goal)
    {
        //Шаг 0. ПОДГОТОВКА
        RemoveAllNodes();


        // Шаг 1.
        Debug.Log("FindPath(): Шаг 1.");
        var closedSet = new Collection<MG_AP_PathNode>();
        var openSet = new Collection<MG_AP_PathNode>();
        TotalVisitedCells = 0;
        Step = 0;
        // Шаг 2.
        Debug.Log("FindPath(): Шаг 2.");
        GameObject obj_pathNode = Instantiate(Prefab_pathNode); //Создание нода
        obj_pathNode.transform.parent = this.transform; //Вставляем данный child в Parent
        obj_pathNode.name = "Pathnode №" + TotalVisitedCells + ": (" + start.GameCoordinate_x + ", " + start.GameCoordinate_y + ")"; //Задаем имя объекту

        MG_AP_PathNode startNode = obj_pathNode.GetComponent<MG_AP_PathNode>();
        start.Label.text = "" + Step + "";
        ArrayOfNodes[TotalVisitedCells] = startNode;//кладем в массив
        startNode.Position = start.transform;
        startNode.CameFrom = null;
        startNode.PathLengthFromStart = 0;
        startNode.HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal);
        startNode.Current_compCell = start;

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            // Шаг 3.
            Debug.Log("FindPath(): Шаг 3.");
            var currentNode = openSet.OrderBy(node =>
              node.EstimateFullPathLength).First();
            // Шаг 4.
            Debug.Log("FindPath(): Шаг 4.");
            if (currentNode.Current_compCell == goal)
            {
                List<MG_hexagonCell> listOfCells = GetPathForNode(currentNode);
                ReColorPatch(listOfCells);
                start.Rend.material.color = Color.green;
                goal.Rend.material.color = new Color(0.2F, 0.3F, 0.4F);
                start.Label.text = "->BEGIN<-";
                goal.Label.text = "->GOAL<-";
                return listOfCells;
            }
                
            // Шаг 5.
            Debug.Log("FindPath(): Шаг 5.");
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            // Шаг 6.
            Debug.Log("FindPath(): Шаг 6.");
            foreach (var neighbourNode in GetNeighbours(currentNode, goal))
            {
                // Шаг 7.
                Debug.Log("FindPath(): Шаг 7.");
                if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;
                var openNode = openSet.FirstOrDefault(node =>
                  node.Position == neighbourNode.Position);
                // Шаг 8.
                Debug.Log("FindPath(): Шаг 8.");
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else
                  if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    // Шаг 9.
                    Debug.Log("FindPath(): Шаг 9.");
                    openNode.CameFrom = currentNode;
                    openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }
        // Шаг 10.
        Debug.LogWarning("FindPath(): Шаг 10. Путь не найден!");

        return null;

    }

    private float GetHeuristicPathLength(MG_hexagonCell from, MG_hexagonCell to)
    {
        return Math.Abs(from.transform.position.x - to.transform.position.x) + Math.Abs(from.transform.position.y - to.transform.position.y);
    }

    private Collection<MG_AP_PathNode> GetNeighbours(MG_AP_PathNode pathNode,
  MG_hexagonCell goal)
    {
        Step++;
        var result = new Collection<MG_AP_PathNode>();
        
        // Соседними точками являются соседние по стороне клетки.
        MG_hexagonCell[] neighbourPoints = new MG_hexagonCell[6];
        neighbourPoints[0] = pathNode.Current_compCell.NeighbourArray[0];
        neighbourPoints[1] = pathNode.Current_compCell.NeighbourArray[1];
        neighbourPoints[2] = pathNode.Current_compCell.NeighbourArray[2];
        neighbourPoints[3] = pathNode.Current_compCell.NeighbourArray[3];
        neighbourPoints[4] = pathNode.Current_compCell.NeighbourArray[4];
        neighbourPoints[5] = pathNode.Current_compCell.NeighbourArray[5];    
        foreach (var neighbour in neighbourPoints)
        {

            if (!neighbour)//если существует соседняя клетка
                continue;
            if (!neighbour.Walkable)//если можно ходить по ней
                continue;

            // Заполняем данные для точки маршрута.
            TotalVisitedCells++;
            if (TotalVisitedCells == TotalCellcount)//если слишком много шагов, то перестать искать!
                continue;

            GameObject obj_pathNode = Instantiate(Prefab_pathNode); //Создание нода
            obj_pathNode.transform.parent = this.transform; //Вставляем данный child в Parent
            obj_pathNode.name = "Pathnode №" + TotalVisitedCells + ": (" + neighbour.GameCoordinate_x + ", " + neighbour.GameCoordinate_y + ")"; //Задаем имя объекту

            MG_AP_PathNode neighbourNode = obj_pathNode.GetComponent<MG_AP_PathNode>();
            ArrayOfNodes[TotalVisitedCells] = neighbourNode;//кладем в массив  
            neighbourNode.Position = neighbour.transform;
            neighbourNode.CameFrom = pathNode;
            neighbourNode.PathLengthFromStart = pathNode.PathLengthFromStart + 1;// neighbourNode.GetDistanceBetweenNeighbours();
            neighbourNode.HeuristicEstimatePathLength = GetHeuristicPathLength(neighbour, goal);
            neighbourNode.Current_compCell = neighbour;

            result.Add(neighbourNode);
            neighbour.Rend.material.color = Color.magenta;
            //neighbour.Label.text = "" + Step + "";
        }    
        return result;
    }

    private List<MG_hexagonCell> GetPathForNode(MG_AP_PathNode pathNode)
    {
        var result = new List<MG_hexagonCell>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Current_compCell);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }

    private void ReColorPatch(List<MG_hexagonCell> listOfCells)
    {
        foreach (var cell in listOfCells)
        {
            cell.Rend.material.color = Color.blue;
            cell.Label.text = "+";
        }
        
    }
}
