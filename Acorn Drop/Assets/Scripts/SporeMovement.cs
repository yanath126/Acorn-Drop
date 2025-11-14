using UnityEngine;

public class SporeMovement : MonoBehaviour
{

    [SerializeField] float speed = .25f;
    [SerializeField] float distance = 3f;
    [SerializeField] float ApproachDistance = 1f;
    [SerializeField] Transform RightSpore;
    [SerializeField] Transform LeftSpore;

    Vector3 startPosLeft;
    Vector3 startPosRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosLeft = LeftSpore.position;
        startPosRight = RightSpore.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, 1f);
        


        Vector3 LeftOffset = new Vector3(movement*distance, movement*distance, 0);
        Vector3 RightOffset = new Vector3(-movement*distance, movement*distance, 0);

        LeftSpore.position = startPosLeft + LeftOffset;
        RightSpore.position = startPosRight + RightOffset;
    }

}
