using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public GameObject paperPrefab;
    public Transform collectPoint;

    int paperLimit = 10;
    private void OnEnable()
    {
        TriggerManager.OnPaperCollect += GetPaper;
        TriggerManager.OnPaperGive += GivePaper;
    }
    private void OnDisable()
    {
        TriggerManager.OnPaperCollect -= GetPaper;
        TriggerManager.OnPaperGive -= GivePaper;

    }
     void GivePaper()
    {
        if(paperList.Count > 0)
        {
            TriggerManager.workerManager.GetPaper();
            RemoveLast();
        }
    }
    void GetPaper()
    {
        if(paperList.Count <= paperLimit)
        {
            GameObject temp = Instantiate(paperPrefab, collectPoint);
            temp.transform.position = new Vector3(collectPoint.position.x, 0.5f+ ((float)paperList.Count) / 20, collectPoint.position.z);
            paperList.Add(temp);
            if(TriggerManager.printerManager != null)
            {
                TriggerManager.printerManager.RemoveLast();
            }
        }
    }
    public void RemoveLast()
    {
        if (paperList.Count > 0)
        {
            Destroy(paperList[paperList.Count - 1]);
            paperList.RemoveAt(paperList.Count - 1);
        }
    }
}
