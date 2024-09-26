using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private readonly string _materialPropertyName = "_Color";

    public int PartitionChance { get; private set; } = 100;

    public Vector3 Size => transform.localScale;
    public Vector3 Position => transform.position;

    public void SetPartitionChance(int partitionChance)
    {
        if (partitionChance > 0 && partitionChance <= PartitionChance)
            PartitionChance = partitionChance;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.SetColor(_materialPropertyName, color);
    }

    public void SetSize(Vector3 size)
    {
        transform.localScale = size;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void AddExplosionForce(float explosionForce, float explosionRadius, Vector3 position)
    {
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, position, explosionRadius);
    }

    public bool CanDivide()
    {
        int minChance = 1;
        int maxChance = 101;

        return Random.Range(minChance, maxChance) < PartitionChance;
     }

    public void Init(Vector3 size, Color color, int partitionChance)
    {
        SetColor(color);
        SetSize(size);
        SetPartitionChance(partitionChance);
    }
}