using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Keyboard Orbit")]
public class MG_KeyboardMouseControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 20.0f;
    [SerializeField]
    private float x = 0f;
    [SerializeField]
    private float y = 0f;
    [SerializeField]
    private float z = -10f;
    [SerializeField]
    private float zoomLevel = 2f;// уровень зума
    [SerializeField]
    private float zoomAmount = 0f; //Для мыши
    [SerializeField]
    private float maxToClamp = 10f;//Для мыши
    [SerializeField]
    private float rotSpeed = 10f;//Для мыши

    [SerializeField]
    private GameObject selectedObject; //выбранный мышкой объект (до этого предыдущий)
    [SerializeField]
    private GameObject hitObject; //выбранный мышкой объект (с помощью клика)
    [SerializeField]
    private GameObject obj_Pathfinding;// Объект Pathfinding (должен быть на сцене!)

    public GameObject Obj_Pathfinding
    {
        get
        {
            return obj_Pathfinding;
        }

        set
        {
            obj_Pathfinding = value;
        }
    }

    public GameObject HitObject
    {
        get
        {
            return hitObject;
        }

        set
        {
            hitObject = value;
        }
    }

    public GameObject SelectedObject
    {
        get
        {
            return selectedObject;
        }

        set
        {
            selectedObject = value;
        }
    }

    public float RotSpeed
    {
        get
        {
            return rotSpeed;
        }

        set
        {
            rotSpeed = value;
        }
    }

    public float MaxToClamp
    {
        get
        {
            return maxToClamp;
        }

        set
        {
            maxToClamp = value;
        }
    }

    public float ZoomAmount
    {
        get
        {
            return zoomAmount;
        }

        set
        {
            zoomAmount = value;
        }
    }

    public float ZoomLevel
    {
        get
        {
            return zoomLevel;
        }

        set
        {
            zoomLevel = value;
        }
    }

    public float Z
    {
        get
        {
            return z;
        }

        set
        {
            z = value;
        }
    }

    public float Y
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

    public float X
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

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    //================================Методы


    void Start()
    {
        Obj_Pathfinding = GameObject.FindWithTag("MG_Pathfinding");
        X = transform.position.x; //Устанавливаем стандартное значение после старта игры (Иначе баг будет с позициями камеры)
        Y = transform.position.y; //Устанавливаем стандартное значение после старта игры (Иначе баг будет с позициями камеры)
    }

    void Update()
    {
        MouseControl();
        KeyboardContro();
    }

    private void MouseControl()
    {
        //BEGIN Управление зумом с помощью средней кнопкой мыши
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize > 5)
            {
                Camera.main.orthographicSize--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize < 40)
            {
                Camera.main.orthographicSize++;
            }
        }
        //END Управление зумом с помощью средней кнопкой мыши
    }

    private void KeyboardContro()
    {
        //BEGIN Управление движением камеры с помощью кнопок

        if (Input.GetKey(KeyCode.D))
        {
            float bonusSpeed = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bonusSpeed = Speed * 2;
            }

            X = X + ((Speed + bonusSpeed) * Time.deltaTime);
            transform.position = new Vector3(X, Y, Z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            float bonusSpeed = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bonusSpeed = Speed * 2;
            }
            X = X - ((Speed + bonusSpeed) * Time.deltaTime);
            transform.position = new Vector3(X, Y, Z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float bonusSpeed = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bonusSpeed = Speed * 2;
            }
            Y = Y - ((Speed + bonusSpeed) * Time.deltaTime);
            transform.position = new Vector3(X, Y, Z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            float bonusSpeed = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bonusSpeed = Speed * 2;
            }
            Y = Y + ((Speed + bonusSpeed) * Time.deltaTime);
            transform.position = new Vector3(X, Y, Z);
        }
        //END Управление движением камеры с помощью кнопок

        //BEGIN Управление зумом с помощью клавиши
        float CurrentZoomLevel = Camera.main.orthographicSize;

        if (Input.GetKey(KeyCode.PageUp))
        {
            //if (!(CurrentZoomLevel - 5 < 5))
            //    Camera.main.orthographicSize = CurrentZoomLevel - 5;
            // else
            Camera.main.orthographicSize = 5;
        }

        if (Input.GetKey(KeyCode.PageDown))
        {
            //if (!(CurrentZoomLevel + 5 > 40))
            //    Camera.main.orthographicSize = CurrentZoomLevel + 5;
            //else
            Camera.main.orthographicSize = 40;
        }

        if (Input.GetKey(KeyCode.Keypad5))
        {
            Camera.main.orthographicSize = 20;
        }
        //END Управление зумом с помощью клавиши


        //BEGIN Нажатие по клетки левой кнопки мышки
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
            if (hit)
            {
                HitObject = hit.transform.gameObject;
                //int x = hitObject.GetComponent<MG_hexagonCell>().GameCoordinate_x;
                //int y = hitObject.GetComponent<MG_hexagonCell>().GameCoordinate_y;
                //print(hit.transform.gameObject.tag + " Coordinates: " + x + ", " + y);

                if (SelectedObject)
                {
                    //BEGIN Если предыдущая кликнутая клетка не совпадает с текущей, то задаем Кликнуто и перекрашиваем в другой цвет
                    if (!GameObject.ReferenceEquals(HitObject, SelectedObject))
                    {
                        //selectedObject.GetComponent<MG_hexagonCell>().CancelSelect(); //УБРАНО НА ВРЕМЯ, ТЕСТИРУЮ ПОКА PATHFINDING
                        //hitObject.GetComponent<MG_hexagonCell>().SelectedByMouse(); //УБРАНО НА ВРЕМЯ, ТЕСТИРУЮ ПОКА PATHFINDING
                        SelectedObject = HitObject;
                    }
                    //END Если предыдущая кликнутая клетка не совпадает с текущей, то задаем Кликнуто и перекрашиваем в другой цвет                                 
                }
                else
                {
                    //**Если предыдущей клетки не было кликнуто, то задаем ее и перекрашиваем
                    //hitObject.GetComponent<MG_hexagonCell>().SelectedByMouse(); //УБРАНО НА ВРЕМЯ, ТЕСТИРУЮ ПОКА PATHFINDING
                    SelectedObject = HitObject;
                }
                Obj_Pathfinding.GetComponent<MG_PathfindingCells>().InitCellForPathfinding(HitObject.GetComponent<MG_hexagonCell>()); //для Pathfinding, проверяем из GUI что нужно поставить в данной клетки (Цель или Старт, перекрасить в воду или землю)
            }
        }
        //END Нажатие по клетки левой кнопки мышки
    }

}