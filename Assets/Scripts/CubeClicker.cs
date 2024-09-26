using UnityEngine;

public class CubeClicker : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.TryGetComponent(out Cube cube))
            {
                if (cube.CanDivide())
                {
                    _exploder.CreateSeparatingExplosion(cube);
                }
                else
                {
                    _exploder.CreateSimpleExplosion(cube);
                }

                cube.Destroy();
            }
        }
    }
}