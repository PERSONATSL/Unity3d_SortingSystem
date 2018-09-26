# Transport System

* Four targets
* Random item targets
* Item will not collide with each other
* Show the count of item

## Main code of this project
*iTEM.cs
```
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 3; // 物品运动速度

    public Transform[] stationPoint; //定义若干条线路数组
    public Collider[] itemFourDirections; //定义物品运动的四个方向的碰撞检测器
    public string[] nameOfAdress; //定义要显示在UI上的地址名数组

    private float currentSpeed; // 物品实际当前速度
    private int roadIndex; //定义一个变量，用于线路的索引
    private int index; // 定义一个变量，用于每条线路的目标点索引
    public GameObject textNameOfAdress; //获取要显示地址名称的TextMash物体

    private float distanceOfNextStationPoint; //物体当前位置和下一个目标位置之间的距离

    void Start()
    {
        //通过名字查找到线路数组的具体内容（因为物体为预制体，无法通过简单的拖拽实现）
        stationPoint[0] = GameObject.Find("RoadOne").transform;
        stationPoint[1] = GameObject.Find("RoadTwo").transform;
        stationPoint[2] = GameObject.Find("RoadThree").transform;
        stationPoint[3] = GameObject.Find("RoadFour").transform;

        currentSpeed = speed;//设置当前实际速度为物品运动速度
        index = 0;
        roadIndex = Random.Range(0, stationPoint.Length);//物品生成时随机一个数字，来确定物品的运动线路
        textNameOfAdress.GetComponent<TextMesh>().text = nameOfAdress[roadIndex].ToString();//通过上面的随机数字确定显示的地址名称


    }

    void Update()
    {

        if (transform.position != stationPoint[roadIndex].transform.GetChild(index).position)//如果物品当前位置不等于目标点位置，就调用MoveToNextPoint();
            MoveToNextPoint();
        else if (transform.position == stationPoint[roadIndex].transform.GetChild(index).position)//如果物品当前位置等于目标点位置，就调用StationPointManager();
        {

            StationPointManager();
        }
        /////通过比较物体当前位置和目标点位置的 x坐标/z坐标 大小来判断物体运动方向（检测前方和后方是否有障碍物）
        if (transform.position.x != stationPoint[roadIndex].GetChild(index).position.x)
        {
            itemFourDirections[1].enabled = false;
            itemFourDirections[3].enabled = false;
            itemFourDirections[2].enabled = true;
            itemFourDirections[0].enabled = true;

        }
        /////通过比较物体当前位置和目标点位置的 x坐标/z坐标 大小来判断物体运动方向（检测前方和后方是否有障碍物）
        if (transform.position.z != stationPoint[roadIndex].GetChild(index).position.z)
        {
            itemFourDirections[0].enabled = false;
            itemFourDirections[2].enabled = false;
            itemFourDirections[1].enabled = true;
            itemFourDirections[3].enabled = true;
        }

    }


    public void MoveToNextPoint()
    {
        //通过Vector3.MoveTowards（）方法实现物体向目标点运动
        transform.position = Vector3.MoveTowards(transform.position, stationPoint[roadIndex].GetChild(index).position, Time.deltaTime * speed);
        speed += 0.15f;
        if (speed > currentSpeed)
            speed = currentSpeed;
    }
    public void StationPointManager()
    {
        speed = 0;
        if (index == stationPoint[roadIndex].childCount - 1)//如果到达最后一个目标点，调用RemoveItem.cs中的Remove()方法销毁物品
        {
            LevelManager.numberOfItemAll++;//LevelManager.cs中的 numberOfItem 计数加一
            if (roadIndex == 0)
                LevelManager.numberOfItemBeiJing++;
            if (roadIndex == 1)
                LevelManager.numberOfItemShangHai++;
            if (roadIndex == 2)
                LevelManager.numberOfItemChengDu++;
            if (roadIndex == 3)
                LevelManager.numberOfItemShenZhen++;
            GetComponent<RemoveItem>().Remove();
            GetComponent<Item>().enabled = false;//关闭物品上的Item脚本
        }
        else//如果没达到最后一个目标点位置，index加一
            index++;
    }

    public float DistanceOfNextStationPoint()
    {
        //返回物体当前坐标与目标点坐标的距离差
        return ((stationPoint[roadIndex].GetChild(index).position - transform.position).magnitude);
    }


    private void OnTriggerStay(Collider other)//碰撞检测进入
    {
        //判断当前物品的下个目标位置 是否和 碰撞检测到的物品的下个目标点 位置相同 ，如果相同的话，距离目标点近的先走，远的等待
        if (other.tag == "Item" && stationPoint[roadIndex].GetChild(index).position
            == other.GetComponent<Item>().stationPoint[other.GetComponent<Item>().roadIndex].GetChild(other.GetComponent<Item>().index).position
            && other.GetComponent<Item>().DistanceOfNextStationPoint() < DistanceOfNextStationPoint())
        {

            speed -= 0.5f;
            if (speed < 0)
                speed = 0;
        }

        if (other.tag == "Item" && stationPoint[roadIndex].GetChild(index).position
            != other.GetComponent<Item>().stationPoint[other.GetComponent<Item>().roadIndex].GetChild(other.GetComponent<Item>().index).position
            && other.GetComponent<Item>().DistanceOfNextStationPoint() > DistanceOfNextStationPoint())
        {

            speed -= 0.5f;
            if (speed < 0)
                speed = 0;
        }

    }
    private void OnTriggerExit(Collider other)// 碰撞检测离开，恢复物品速度
    {
        if (other.tag == "Item")
        {
            speed += 0.5f;
            if (speed > currentSpeed)
                speed = currentSpeed;
        }
    }
}
```


## *Finally ScreenShot*

![9](https://user-images.githubusercontent.com/42737061/46076944-b7b8f180-c1c1-11e8-935e-13106ba45956.PNG)
