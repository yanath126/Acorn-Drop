using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    bool isDragging = false;
    Vector3 ImousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // when player clicks mouse
        {
            ImousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging) // when holding down the mouse, player begins to cut the vine
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);

            Debug.Log("Dragging");
            //cut
            ImousePosition = currentMousePosition;
        }
        
        if (Input.GetMouseButtonUp(0)) //after mouse click releases
        {
            isDragging = false;
        }

    }
}
