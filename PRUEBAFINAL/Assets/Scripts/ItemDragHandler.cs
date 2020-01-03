using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
     GameObject grid;
     Transform father;
   

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("PlayerCanvas");
        father = transform.parent;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
        transform.SetParent(grid.transform);
    }

 

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.SetParent(father);
        gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
       
    }

  
}
