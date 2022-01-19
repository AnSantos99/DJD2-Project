using UnityEngine;

public class OutlinerEffect : MonoBehaviour
{
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }


}
