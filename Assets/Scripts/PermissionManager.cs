using System.Collections.Generic;
using UnityEngine;

public class PermissionManager : MonoBehaviour
{
    public List<string> permissionsList;

    private void Awake()
    {
        permissionsList = new List<string>();
        permissionsList.Add("Native");
    }

    public void AddPermission(string permissionName)
    {
        foreach (string s in permissionsList)
        {
            if (s == permissionName)
            {
                Debug.Log("Permission already present");
                return;
            }
        }
        permissionsList.Add(permissionName);
        Debug.Log("Permission successfully added");
    }

    public void RemovePermission(string permissionName)
    {
        permissionsList.Remove(permissionName);
        Debug.Log("Permission removed");
    }
}
