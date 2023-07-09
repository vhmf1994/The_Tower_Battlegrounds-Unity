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
        // Cria um raio a partir da posição do mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Lança o raio e verifica se colidiu com algum objeto no mundo
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Mouse")))
        {
            // Obtém a posição do ponto de colisão
            Vector3 mouseWorldPosition = hit.point;

            return mouseWorldPosition;
        }
        else
        {
            return Vector3.one * Mathf.Infinity;
        }
    }
}
