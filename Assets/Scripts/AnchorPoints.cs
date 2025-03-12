using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class AnchorPoints : MonoBehaviour
{
    [SerializeField] private int heigh, width;
    [SerializeField] private GameObject anchor;//position?
    public bool anchorToggle; //Build menu needs access 

    /*Can build basic grid system but visually need to have points, not squares*/

    void Start()
    {
        CreateAnchorLayout();
    }
    void CreateAnchorLayout()
    {
        for (int i = 0; i < this.width; i++)
        {
            for (int j = 0; j < this.heigh; j++)
            {
                var existingAnchor = Instantiate(anchor, new Vector2(i, j), Quaternion.identity);//need this set into cnavs for no error, for now just work on framework 
                existingAnchor.name = $"Point {i} {j}";

            }
        }
    }




}
