using UnityEngine;
public class LevelManager : MonoBehaviour
{

    public GameObject itemPrefab;//物品预制体

    public float rangeTime; //计时器
    private float instantiateRangeTime;

    public GameObject itemCounts,beiJing,shangHai,chengDu,shenZhen;//显示包裹数的提示的3dText
    public static int numberOfItemAll, numberOfItemBeiJing, numberOfItemShangHai, numberOfItemChengDu, numberOfItemShenZhen;//物品计数

    void Start()
    {
        instantiateRangeTime = rangeTime;//初始化时间
        //Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);//开始先生成一个物品
    }


    void Update()
    {
        //当计时器小于等于0时，再生成一个物品
        rangeTime -= 0.1f;
        if (rangeTime <= 0)
        {
            rangeTime = instantiateRangeTime;
            Instantiate(itemPrefab,Vector3.zero,Quaternion.identity);

        }
        ShowNumberOfItem();
    }
    public void ShowNumberOfItem()//显示车内包裹数的提示UI
    {
        itemCounts.GetComponent<TextMesh>().text = "全部包裹数：" + numberOfItemAll.ToString();
        beiJing.GetComponent<TextMesh>().text = "北京市包裹数：" + numberOfItemBeiJing.ToString();
        shangHai.GetComponent<TextMesh>().text = "上海市包裹数：" +  numberOfItemShangHai.ToString();
        chengDu.GetComponent<TextMesh>().text = "成都市包裹数：" + numberOfItemChengDu.ToString();
        shenZhen.GetComponent<TextMesh>().text = "深圳市包裹数：" + numberOfItemShenZhen.ToString();
    }
}
