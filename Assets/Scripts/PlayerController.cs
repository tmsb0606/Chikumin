using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;
    public GameObject circle;
    private GameObject pointCircle;

    [SerializeField]
    private LayerMask layerMask;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        drawCursor();
        if (Input.GetMouseButton(0))
        {
            CallChikumin();
        }
    }

    void OnDrawGizmos()
    {
        if (currentPosition != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentPosition, 0.5f);
        }
    }
    private void drawCursor()
    {
        //int layerMask = 1 << 6;
        //int layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(6) });
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var raycastHitList = Physics.RaycastAll(ray,layerMask).ToList();
        print(raycastHitList);
        if (raycastHitList.Any())
        {
            if (pointCircle == null)
            {
                pointCircle = Instantiate(circle);
            }
            var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList.First().point);
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

            currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            print(currentPosition);
            currentPosition.y = 0;
            pointCircle.transform.position = currentPosition;
        }
        else
        {
            if (pointCircle != null)
            {
                pointCircle.SetActive(false);
                pointCircle = null;

            }

        }
    }
    private void CallChikumin()
    {
       // pointCircle.GetComponent<>
    }
}
