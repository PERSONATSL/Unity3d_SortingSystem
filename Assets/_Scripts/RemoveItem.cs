using UnityEngine;

public class RemoveItem : MonoBehaviour {

    public void Remove()//销毁自身
    {
        Destroy(gameObject);
    }
}
