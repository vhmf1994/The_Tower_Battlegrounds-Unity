using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenOrganizer : MonoBehaviour
{
    private void OnValidate()
    {
        OrganizeChildren();
    }

    [Button]
    private void OrganizeChildren()
    {
        // Lista tempor�ria para armazenar os filhos organizados
        List<Transform> filhosOrganizados = new List<Transform>();

        // Percorrer os filhos do objeto pai e armazen�-los na lista tempor�ria
        foreach (Transform child in transform)
        {
            filhosOrganizados.Add(child);
        }

        // Ordenar a lista tempor�ria com base nas coordenadas X e Z
        filhosOrganizados.Sort((a, b) =>
        {
            if (a.position.z != b.position.z)
                return a.position.z.CompareTo(b.position.z);
            else
                return a.position.x.CompareTo(b.position.x);
        });

        // Atualizar a hierarquia dos filhos no objeto pai
        for (int i = 0; i < filhosOrganizados.Count; i++)
        {
            filhosOrganizados[i].SetSiblingIndex(i);
        }
    }
}
