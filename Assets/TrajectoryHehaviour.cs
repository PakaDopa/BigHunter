using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryHehaviour : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int resolution = 30; // 점 개수
    public float dotSpacing = 0.1f; //점 간 간격
    public float timeStep = 0.01f; // 점 간 시간 간격
    public float maxTime = 3f; // 전체 궤적 시간
    public Transform startPoint; // 발사 위치
    [SerializeField] private Vector2 offset;

    [SerializeField] private GameObject dotPrefab;
    private List<GameObject> dots = new List<GameObject>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();


        for (int i = 0; i < resolution; i++)
        {
            GameObject dot = Instantiate(dotPrefab, transform);
            dot.SetActive(false);
            dots.Add(dot);
        }
    }
    public void ShowTrajectory(Vector2 initialVelocity, float gravity)
    {
        for (int i = 3; i < resolution; i++)
        {
            float t = i/7.0f * dotSpacing;
            Vector2 pos = new Vector2(startPoint.position.x + offset.x, startPoint.position.y + offset.y)
                        + initialVelocity * t
                        + 0.5f * Physics2D.gravity * gravity * t * t;

            dots[i].transform.position = new(pos.x, pos.y, -0.9f);
            dots[i].transform.localScale = new(0.5f - t, 0.5f - t, 0.5f - t);
            dots[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.75f - t);
            dots[i].SetActive(true);
        }
    }

    public void HideTrajectory()
    {
        foreach (var dot in dots)
            dot.SetActive(false);
    }
}
