using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public GameObject wallPrefab;

    private Transform firstPoint = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("WallPoint"))
                {
                    Debug.Log("Sphère cliquée : " + hit.collider.name);
                    if (firstPoint == null)
                    {
                        firstPoint = hit.collider.transform;
                    }
                    else
                    {
                        CreateWall(firstPoint.position, hit.collider.transform.position);
                        firstPoint = null;
                    }
                }
            }
        }
    }

    void CreateWall(Vector3 a, Vector3 b)
    {
        Vector3 mid = (a + b) / 2f;
        GameObject wall = Instantiate(wallPrefab, mid, Quaternion.identity);

        Vector3 diff = b - a;
        diff.y = 0;

        float length = diff.magnitude;
        
        wall.transform.localScale = new Vector3(0.2f, 1f, length);
        wall.transform.rotation = Quaternion.LookRotation(diff);
    }
}
