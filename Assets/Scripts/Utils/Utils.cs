using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetWorldMousePosition()
    {
        Vector3 worldMousePosition;

        worldMousePosition = GetWorldMousePositionWithY();
        worldMousePosition.y = 0;

        return worldMousePosition;
    }

    public static Vector3 GetWorldMousePositionWithY()
    {
        // Cria um raio a partir da posi��o do mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Lan�a o raio e verifica se colidiu com algum objeto no mundo
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Mouse")))
        {
            // Obt�m a posi��o do ponto de colis�o
            Vector3 mouseWorldPosition = hit.point;

            return mouseWorldPosition;
        }
        else
        {
            return Vector3.one * Mathf.Infinity;
        }
    }
}
