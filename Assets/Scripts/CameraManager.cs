using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private BoxCollider2D mapCollider;
    [SerializeField] private float xMin, xMax, yMin, yMax;

    private void Start()
    {
        if (!mainCam) mainCam = GetComponent<Camera>();

        PutColliders();
    }

    private void FixedUpdate()
    {
        Vector3 pos = PlayersManager.Instance.GetCenterBetweenPlayers();
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        pos.z = -10;
        this.transform.position = pos;
    }

    private void PutColliders()
    {
        GameObject left = new GameObject("LeftCollider", typeof(BoxCollider2D));
        GameObject right = new GameObject("RightCollider", typeof(BoxCollider2D));
        GameObject top = new GameObject("TopCollider", typeof(BoxCollider2D));
        GameObject bottom = new GameObject("BottomCollider", typeof(BoxCollider2D));

        left.transform.SetParent(transform);
        right.transform.SetParent(transform);
        top.transform.SetParent(transform);
        bottom.transform.SetParent(transform);

        Vector2 leftBottomCorner = mainCam.ViewportToWorldPoint(Vector3.zero);
        Vector2 rightTopCorner = mainCam.ViewportToWorldPoint(Vector3.one);

        left.transform.position = new Vector2(
            leftBottomCorner.x,
            mainCam.transform.position.y
        );
        right.transform.position = new Vector2(
            rightTopCorner.x,
            mainCam.transform.position.y
        );
        top.transform.position = new Vector2(
            mainCam.transform.position.x,
            rightTopCorner.y
        );
        bottom.transform.position = new Vector2(
            mainCam.transform.position.x,
            leftBottomCorner.y
        );

        left.transform.localScale = new Vector3(.1f, Mathf.Abs(rightTopCorner.y - leftBottomCorner.y));
        right.transform.localScale = new Vector3(.1f, Mathf.Abs(rightTopCorner.y - leftBottomCorner.y));
        top.transform.localScale = new Vector3(Mathf.Abs(rightTopCorner.x - leftBottomCorner.x), .1f);
        bottom.transform.localScale = new Vector3(Mathf.Abs(rightTopCorner.x - leftBottomCorner.x), .1f);
    }
}
