# Rollaball Project

Este proyecto es un juego de ejemplo llamado Rollaball. A continuación se describen los diferentes scripts utilizados en el proyecto y su función.

## Archivos de Scripts

### 1. Rotator.cs

```csharp
// filepath: /c:/Users/nicoX/Rollaball/Assets/Scripts/Rotator.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
```
Función: Este script se utiliza para rotar un objeto en el juego. En cada frame, el objeto se rota en los ejes X, Y y Z a una velocidad constante.

### 2. CameraControllerThirdPerson.cs

```csharp
// filepath: /c:/Users/nicoX/Rollaball/Assets/Scripts/CameraControllerThirdPerson.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject.
    public GameObject player;
    // The distance between the camera and the player.
    private Vector3 offset;
    // Sensibilidad del ratón
    public float mouseSensitivity = 100.0f;
    private float yRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in the CameraController script.");
            return;
        }

        // Calculate the initial offset between the camera's position and the player's position.
        offset = transform.position - player.transform.position;

        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {
        // Ensure the player reference is set
        if (player == null)
        {
            return;
        }

        // Obtener el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;

        // Rotar el jugador en el eje Y (horizontal)
        player.transform.Rotate(Vector3.up * mouseX);

        // Mantener el mismo desplazamiento entre la cámara y el jugador durante todo el juego.
        transform.position = player.transform.position + offset;

        // Rotar la cámara alrededor del jugador
        transform.LookAt(player.transform.position);
    }
}
```
Función: Este script controla una cámara en tercera persona que sigue al jugador. La cámara mantiene una distancia constante del jugador y rota alrededor de él en función del movimiento del ratón.

### 3. CameraControlerFirstPerson

```csharp
// filepath: /c:/Users/nicoX/Rollaball/Assets/Scripts/CameraControllerFirstPerson.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_PrimPersona : MonoBehaviour {
    // Reference to the player GameObject.
    public GameObject player;
    // Sensibilidad del ratón
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    // Start is called before the first frame update
    void Start(){
        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate(){
        // Obtener el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        // Rotar la cámara en el eje X (vertical) y Y (horizontal)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        // Rotar el jugador en el eje Y (horizontal)
        player.transform.Rotate(Vector3.up * mouseX);

        // Asegurarse de que la cámara siga al jugador
        transform.position = player.transform.position;
    }
}
```
Función: Este script controla una cámara en primera persona que sigue al jugador. La cámara rota en función del movimiento del ratón y se asegura de que su posición coincida con la del jugador.

### 4. CameraControllerCinematic

```csharp
// filepath: /C:/Users/nicoX/Rollaball/Assets/Scripts/CameraControllerCinematic.cs
using UnityEngine;

public class CameraControllerCinematic : MonoBehaviour
{
    // Punto central alrededor del cual la cámara girará
    public Transform centerPoint;
    // Radio de la órbita
    public float orbitRadius = 10.0f;
    // Velocidad angular de la cámara
    public float orbitSpeed = 10.0f;
    // Altura de la cámara
    public float height = 5.0f;

    private float angle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (centerPoint == null)
        {
            Debug.LogError("Center point is not assigned in the CameraControllerCinematic script.");
            return;
        }

        // Colocar la cámara en la posición inicial de la órbita
        UpdateCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (centerPoint == null)
        {
            return;
        }

        // Incrementar el ángulo para mover la cámara
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360.0f)
        {
            angle -= 360.0f;
        }

        // Actualizar la posición de la cámara
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Calcular la nueva posición de la cámara en la órbita
        float x = centerPoint.position.x + orbitRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = centerPoint.position.z + orbitRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = centerPoint.position.y + height;

        // Establecer la nueva posición y rotación de la cámara
        transform.position = new Vector3(x, y, z);
        transform.LookAt(centerPoint);
    }
}
```
Función: Este script controla una cámara cinemática que orbita alrededor de un punto central. La cámara se mueve en una trayectoria circular a una altura constante y siempre mira hacia el punto central.
