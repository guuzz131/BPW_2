using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragObject : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Canvas canvas;
    public GameObject spawnEntity;
    public List<GameObject> points = new List<GameObject>();
    public List<GameObject> objects = new List<GameObject>();

    private RectTransform childObject;
    private GameObject currentParent;
    int p;

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentParent = transform.parent.gameObject;
        p = points.IndexOf(currentParent);
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int t = 0;
        float minDistance = Vector3.Distance(transform.position, points[0].transform.position);
        for (int i = 0; i < points.Count; i++)
        {
            float distanceBetween = Vector3.Distance(transform.position, points[i].transform.position);
            if (distanceBetween < minDistance)
            {
                minDistance = distanceBetween;
                t = i;
                //Debug.Log(minDistance + " " + t);
            }
        }
        /*if (points[t].transform.childCount > 0)
        {
            int i = t;
            int k = -1;
            if (p > t) k = 1;
            if (i == 0)
            {
                GameObject child = points[i].transform.GetChild(0).gameObject;
                child.transform.SetParent(points[i + 1].transform);
                child.GetComponent<RectTransform>().anchoredPosition = points[i + 1].GetComponent<RectTransform>().anchoredPosition;
                i = i + 1;
            } else if (i == points.Count - 1)
            {
                GameObject child = points[i].transform.GetChild(0).gameObject;
                child.transform.SetParent(points[i -1].transform);
                child.GetComponent<RectTransform>().anchoredPosition = points[i -1].GetComponent<RectTransform>().anchoredPosition;
                i = i -1;
            }
            while (points[i + k].transform.childCount > 0 && (i-1 >= 0 && i+1 < points.Count))
            {
                Debug.Log(i);
                GameObject child = points[i].transform.GetChild(0).gameObject;
                child.transform.SetParent(points[i + k].transform);
                child.GetComponent<RectTransform>().anchoredPosition = points[i + k].GetComponent<RectTransform>().anchoredPosition;
                i = i + k;
            }

        }*/
        if (points[t].transform.childCount > 0)
        {
            GameObject child = points[t].transform.GetChild(0).gameObject;
            child.transform.SetParent(points[p].transform);
            child.GetComponent<RectTransform>().anchoredPosition = points[p].GetComponent<RectTransform>().anchoredPosition;
        }
        transform.SetParent(points[t].transform); 
        gameObject.GetComponent<RectTransform>().anchoredPosition = points[t].GetComponent<RectTransform>().anchoredPosition;

        for (int i  = 0; i < objects.Count; i++)
        {
                if (points[i].transform.childCount == 0)
                {
                    GameObject child = points[i + 1].transform.GetChild(0).gameObject;
                    child.transform.SetParent(points[i].transform);
                    child.GetComponent<RectTransform>().anchoredPosition = points[i].GetComponent<RectTransform>().anchoredPosition;
                }
        }
    }

    private void Start()
    {
        childObject = transform.GetChild(0).GetComponent<RectTransform>();
        
    }
    public void GiveNewPlayer()
    {
        gameObject.GetComponent<Animation>().Play();
        Invoke("DestroyThis", .5f);

    }
    void DestroyThis()
    {
        
        for (int i = 0; i < objects.Count; i++)
        {
            if (points[i].transform.childCount == 0)
            {
                GameObject child = points[i + 1].transform.GetChild(0).gameObject;
                child.transform.SetParent(points[i].transform);
                child.GetComponent<RectTransform>().anchoredPosition = points[i].GetComponent<RectTransform>().anchoredPosition;
            }
            objects[i].GetComponent<DragObject>().objects.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
