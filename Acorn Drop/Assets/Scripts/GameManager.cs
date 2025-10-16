using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    bool isDragging = false;
    Vector3 ImousePosition;
    HingeJoint2D joint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // when player clicks mouse
        {
            ImousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);
            //Debug.Log("Is Clicking");
            isDragging = true;
        }

        if (Input.GetButton("Fire1") && isDragging) // when holding down the mouse, player begins to cut the vine
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);

            //Debug.Log("Dragging");
            //check for a swipe and destroy the vines
            ImousePosition = currentMousePosition;
        }

        if (Input.GetButtonUp("Fire1")) //after mouse click releases
        {
            isDragging = false;
        }

    }





}
