using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PlayerManager : MonoBehaviour
{

    public GameObject PlayerPrefab; //Префаб игрока
    public GameObject Prefab_buildings; //Префаб построек игрока
    public GameObject Prefab_units; //Префаб юнитов игрока

    public int players_count; //Кол-во человеческих игроков
    public int AI_count;    //Кол-во AI игроков
    public int TotalPlayers_count;    //Кол-во AI игроков
    public GameObject[] ArrayOfAllPlayers; //Массив всех игроков

    public void CreatePlayers() //Создать игроков
    {
        TotalPlayers_count = (players_count + AI_count);
        ArrayOfAllPlayers = new GameObject[TotalPlayers_count];

        int num = 0;

        for (int i = 0; i < TotalPlayers_count; i++)
        {
            //BEGIN Создание игрока
            GameObject newPlayer = Instantiate(PlayerPrefab); //Создание нового игрока
            GameObject buildings = Instantiate(Prefab_buildings); //Создание child объекта для игрока (Строения)
            GameObject units = Instantiate(Prefab_units); //Создание child объекта для игрока (Юниты)

            ArrayOfAllPlayers[i] = newPlayer; //кладем в массив
            newPlayer.transform.parent = this.transform; //Вставляем данный child в Parent

            if (players_count > num)
            {
                num++;
                newPlayer.GetComponent<MG_Player>().PlayerNumber = num; //задаем номер игроку
                newPlayer.GetComponent<MG_Player>().Personality = "n/a"; //задаем персональность
                newPlayer.GetComponent<MG_Player>().DifficultLevel = "n/a"; //задаем уровень сложности
                newPlayer.GetComponent<MG_Player>().IsRealPlayer = true; //задаем что это настоящий игрок  
                newPlayer.GetComponent<MG_Player>().IsAlive = true; //задаем Живой или Уничтожен в игре
                newPlayer.name = "Player " + num; 
            }
            else
            {
                num++;
                newPlayer.GetComponent<MG_Player>().PlayerNumber = num; //задаем номер игроку
                newPlayer.GetComponent<MG_Player>().Personality = "Passive"; //задаем персональность
                newPlayer.GetComponent<MG_Player>().DifficultLevel = "Normal"; //задаем уровень сложности
                newPlayer.GetComponent<MG_Player>().IsRealPlayer = false; //задаем что это НЕ настоящий игрок        
                newPlayer.GetComponent<MG_Player>().IsAlive = true; //задаем Живой или Уничтожен в игре
                newPlayer.name = "Player " + num + " (AI)";
            }

            buildings.transform.parent = newPlayer.transform; //Вставляем данный child (Постройки) в Parent (Игрок)
            buildings.name = "P" + num + "_buildings";
            units.transform.parent = newPlayer.transform; //Вставляем данный child (Юниты) в Parent (Игрок)               
            units.name = "P" + num + "_units";

            //END Создание игрока
        }
    }

    public void RemoveAllPlayers()
    {
        //BEGIN Удаляем всех игроков
        var children = new List<GameObject>();
        foreach (Transform cell in this.transform)
        {
            //if (cell.gameObject.CompareTag("hexagon"))
            children.Add(cell.gameObject);
        }
        children.ForEach(c => DestroyImmediate(c));
        //END Удаляем всех игроков
    }
}