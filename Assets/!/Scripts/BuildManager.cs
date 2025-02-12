using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Already pre existing build manager");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
