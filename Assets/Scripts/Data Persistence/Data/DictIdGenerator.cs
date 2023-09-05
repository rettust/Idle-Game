using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictIdGenerator : MonoBehaviour
{
    public bool isCollected;
    [SerializeField] public string id;
    [ContextMenu("generate guid")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
