using UnityEngine;

public class Node : MonoBehaviour
{
    Color hoverColor;
    Color initialColor;
    private GameObject turret;
    Vector3 turretPlaceOffset = new Vector3(0f, 0.52f, 0f);
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
    }
    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there");
        }
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + turretPlaceOffset, transform.rotation);
    }
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = initialColor;
    }
}
