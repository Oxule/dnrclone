using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class GroupDrawer : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private UILineRenderer line;
    public List<Vector2> Points = new List<Vector2>();

    private void Start()
    {
        line = transform.GetChild(0).GetComponent<UILineRenderer>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var p = eventData.position;
        //convert to local space
        p = transform.InverseTransformPoint(p);
        //if point out of bounds, ignore
        var rect = GetComponent<RectTransform>().rect;
        if(p.x < rect.xMin || p.x > rect.xMax || p.y < rect.yMin || p.y > rect.yMax)
            return;
        Points.Add(p);
        line.Points = Points.ToArray();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Points.Clear();
        line.Points = Points.ToArray();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.FindWithTag("Player").GetComponent<PawnsGroup>().Group(Points);
    }
}
