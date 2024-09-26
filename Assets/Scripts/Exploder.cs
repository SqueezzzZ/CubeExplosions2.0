using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private float _explosionForce = 100;
    [SerializeField] private float _explosionRadius = 5;

    private int _scalingDivisor = 2;
    private int _partitionChanceDivisor = 2;

    public void CreateSeparatingExplosion(Cube cube)
    {
        List<Cube> cubes = _cubeSpawner.CreateRandomAmountCubes(cube.Position, cube.Size / _scalingDivisor, cube.PartitionChance / _partitionChanceDivisor);

        MakeCubesExplosion(cubes, cube.Position, _explosionForce, _explosionRadius);
    }

    public void CreateSimpleExplosion(Cube cube)
    {
        float explosionRadius = CalculateCubeExplosionRadius(cube);
        float explosionForce = CalculateCubeExplosionForce(cube);

        List<Cube> cubes = GetCubesInRadius(cube.Position, explosionRadius);

        MakeCubesExplosion(cubes, cube.Position, explosionForce, explosionRadius);
    }

    private void MakeCubesExplosion(List<Cube> cubes, Vector3 position, float explosionForce, float explosionRadius)
    {
        foreach (var cube in cubes)
        {
            cube.AddExplosionForce(explosionForce, explosionRadius, position);
        }
    }

    private List<Cube> GetCubesInRadius(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        List<Cube> hittedCubes = new List<Cube>();

        foreach (Collider hit in hits)
        {
            if (hit.transform.TryGetComponent(out Cube cube))
            {
                hittedCubes.Add(cube);
            }
        }

        return hittedCubes;
    }

    private float CalculateCubeExplosionForce(Cube cube)
    {
        if (cube.Size.x > 0)
            return _explosionForce / cube.Size.x;
        else return 0;
    }

    private float CalculateCubeExplosionRadius(Cube cube)
    {
        if (cube.Size.x > 0)
            return _explosionRadius / cube.Size.x;
        else return 0;
    }
}