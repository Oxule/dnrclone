using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PawnsGroup : MonoBehaviour
{
    public List<GameObject> Pawns = new List<GameObject>();
    public List<Vector3> Positions = new List<Vector3>();
    public GameObject PawnPrefab;
    public float DistanceMultiplier = 1f;
    public float Speed;
    public float GroupSpeed = 0.1f;
    public float TrackComplete = 1f;
    public List<ITrigger> Triggers = new List<ITrigger>();
    public float PawnRectSize = 1f;
    public GameObject DeathParticlePrefab;
    public SplineComputer TrackSpline;

    public TMP_Text gemsT;

    public void DefaultGroup()
    {
        //
    }

    public void Restart()
    {
        //scene restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Start()
    {
        GameManager.Start();
        for (int i = 0; i < 35; i++)
        {
            GameObject newPawn = Instantiate(PawnPrefab, transform);
            Pawns.Add(newPawn);
            Positions.Add(newPawn.transform.position);
        }

        //find all triggers
        Triggers.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<ITrigger>());
    }

    public void Group(List<Vector2> points)
    {
        //move pawns to points
        for (int i = 0; i < Pawns.Count; i++)
        {
            //get point if points count is more than pawns count
            var p = (float) i / Pawns.Count * points.Count;
            int p1 = Mathf.FloorToInt(p);
            int p2 = Mathf.CeilToInt(p);
            Vector3 point;
            if (p2 < points.Count)
            {
                point = Vector2.Lerp(points[p1], points[p2], p - p1);
            }
            else
            {
                if (p1 < points.Count)
                    point = points[p1];
                else
                    return;
            }
            point.z = point.y;
            point.y = transform.position.y;
            Positions[i] = point;
        }
    }

    public void KillPawn(int p)
    {
        if(p >= Pawns.Count)
            return;
        //spawn death particle
        var pr = Instantiate(DeathParticlePrefab, Pawns[p].transform.position, Quaternion.identity);
        //remove pawn
        Destroy(Pawns[p]);
        Destroy(pr, 2f);
        Pawns.RemoveAt(p);
        Positions.RemoveAt(p);
    }

    public void AddPawn()
    {
        GameObject newPawn = Instantiate(PawnPrefab, transform);
        Pawns.Add(newPawn);
        Positions.Add(newPawn.transform.position);
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < Pawns.Count; i++)
        {
            Pawns[i].transform.localPosition = Vector3.Lerp(Pawns[i].transform.localPosition,
                Positions[i] * DistanceMultiplier, Speed);
        }
        TrackComplete -= GroupSpeed * Time.fixedDeltaTime;
        //get track position from spline
        var pos = TrackSpline.EvaluatePosition(TrackComplete);
        //direction
        var dir = (pos - transform.position).normalized;
        //rotate group
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.position = pos;
    }

    public void Update()
    {
        //make game always vertical
        Screen.orientation = ScreenOrientation.Portrait;
        gemsT.text = GameManager.Gems.ToString();
        if (Pawns.Count == 0)
        {
            GameManager.EndGame(false);
            return;
        }
        for (int i = 0; i < Positions.Count; i++)
        {
            var triggers = Physics.OverlapBox(
                transform.TransformPoint(Positions[i] * DistanceMultiplier),
                new Vector3(PawnRectSize, PawnRectSize, PawnRectSize)
            ).Where(x => x.TryGetComponent<ITrigger>(out var t));
            foreach (var ts in triggers)
            {
                foreach (var t in ts.GetComponents<ITrigger>())
                {
                    t.OnEnter(i);
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < Positions.Count; i++)
        {
            //local to world
            Gizmos.DrawCube(transform.TransformPoint(Positions[i]*DistanceMultiplier), Vector3.one * PawnRectSize);
        }
    }
}
