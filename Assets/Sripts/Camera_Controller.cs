using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private Vector3 targetPoint = Vector3.zero;

    public Player_Controller player_Controller;

    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = new Vector3(player_Controller.transform.position.x,player_Controller.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Movimiento de la cámara básico
        targetPoint.x = player_Controller.transform.position.x;
        targetPoint.y = player_Controller.transform.position.y;

        transform.position = Vector3.Lerp(transform.position, targetPoint, cameraSpeed* Time.deltaTime);
    }

}
