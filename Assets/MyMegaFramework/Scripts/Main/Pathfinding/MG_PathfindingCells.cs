using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PathfindingCells : MonoBehaviour {

    //BEGIN PATHFINDING Инициализация при клике на клетку (используется в MG_KeyboardMouseControl)
    public void InitCellForPathfinding(MG_hexagonCell selected_compCell)
    {
        if (GetComponent<MG_PathfindingModeGUI>().CheckBox_A_point) SetCell_StartPoint(selected_compCell);//Режим: Установить Стартовую точку
        if (GetComponent<MG_PathfindingModeGUI>().CheckBox_B_point) SetCell_TargetPoint(selected_compCell);//Режим: Установить Цель точку
    }
    //END PATHFINDING Инициализация при клике на клетку (используется в MG_KeyboardMouseControl)

    //BEGIN PATHFINDING Назначаем начальную точку
    public void SetCell_StartPoint(MG_hexagonCell selected_compCell)
    {
        //BEGIN предыдущую точку Начало нужно перекрасить в обычный цвет
        MG_hexagonCell comp_previousStartCell = GetComponent<MG_Pathfinding>().Comp_StartCell;// берем предыдущий заданный объект цели
        if (comp_previousStartCell)
        {
            comp_previousStartCell.CancelSelect();// если предыдущий объект был задан, то нужно ресетнуть цвет.
            //obj_previousStartCell.GetComponent<MG_hexagonCell>().IsSelectedByMouse = false;// Чтобы можно было перекрасить при заходе мышкой
        }
        //END предыдущую точку Начало нужно перекрасить в обычный цвет

        selected_compCell.IsSelectedByMouse = true; //Кликнута мышкой, иначе цвет при наведении перезальется     
        selected_compCell.Rend.material.color = Color.green;//Задаем новый цвет клетки цели
        selected_compCell.Walkable = true;
        GetComponent<MG_Pathfinding>().Comp_StartCell = selected_compCell; //Задаем скрипту MG_Pathfinding объект начальной точки
    }
    //END PATHFINDING Назначаем начальную точку

    //BEGIN PATHFINDING Назначаем начальную точку
    public void SetCell_TargetPoint(MG_hexagonCell selected_compCell)
    {
        //BEGIN предыдущую цель нужно перекрасить в обычный цвет
        MG_hexagonCell comp_previousTargetCell = GetComponent<MG_Pathfinding>().Comp_TargetCell;// берем предыдущий заданный объект цели
        if (comp_previousTargetCell)
        {
            comp_previousTargetCell.GetComponent<MG_hexagonCell>().CancelSelect();// если предыдущий объект был задан, то нужно ресетнуть цвет.
            //obj_previousTargetCell.GetComponent<MG_hexagonCell>().IsSelectedByMouse = false;// Чтобы можно было перекрасить при заходе мышкой
        }
        //END предыдущую цель нужно перекрасить в обычный цвет

        selected_compCell.GetComponent<MG_hexagonCell>().IsSelectedByMouse = true; //Кликнута мышкой, иначе цвет при наведении перезальется      
        selected_compCell.GetComponent<MG_hexagonCell>().Rend.material.color = new Color(0.2F, 0.3F, 0.4F); //ORANGE Задаем новый цвет клетки цели
        selected_compCell.Walkable = true;
        GetComponent<MG_Pathfinding>().Comp_TargetCell = selected_compCell; //Задаем скрипту MG_Pathfinding объект цели   
    }
    //END PATHFINDING Назначаем начальную точку

}
