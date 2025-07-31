using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class YarnUntangle : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("How many samples (and spheres) to have in the line")]
    public int points = 10;

    [Tooltip("Distance between each sample along X")]
    public float spacing = 1.0f;

    [Header("Sine Animation")]
    [Tooltip("Maximum vertical offset")]
    public float amplitude = 0.5f;

    [Tooltip("How many full waves per second")]
    public float frequency = 1f;

    [Tooltip("Phase offset per sample (spreads the wave out)")]
    public float phaseOffset = 0.5f;

    [Header("Color")]
    [Tooltip("Gradient to map sine wave value to color")]
    public Gradient colorGradient;

    private List<Vector3> basePositions = new List<Vector3>();
    private List<GameObject> balls = new List<GameObject>();
    private List<Renderer> ballRenderers = new List<Renderer>();
    private LineRenderer line;

    private int lastPoints;
    private float lastSpacing;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.widthCurve = AnimationCurve.Constant(0, 1, 0.1f);
        line.material = new Material(Shader.Find("Sprites/Default"));

        lastPoints = -1;
        lastSpacing = -1;

        // If no gradient is set, create a default rainbow
        if (colorGradient == null)
        {
            colorGradient = new Gradient();
            colorGradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.red, 0f),
                    new GradientColorKey(Color.yellow, 0.33f),
                    new GradientColorKey(Color.green, 0.66f),
                    new GradientColorKey(Color.blue, 1f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f)
                }
            );
        }
    }

    void Update()
    {
        if (points != lastPoints || !Mathf.Approximately(spacing, lastSpacing))
        {
            RebuildLineAndBalls();
            lastPoints = points;
            lastSpacing = spacing;
        }

        AnimateAndRenderWave();
    }

    void RebuildLineAndBalls()
    {
        foreach (var b in balls)
            if (b != null) Destroy(b);
        balls.Clear();
        ballRenderers.Clear();
        basePositions.Clear();

        line.positionCount = Mathf.Max(1, points);

        float totalLength = (points - 1) * spacing;
        float halfLength = totalLength / 2f;

        for (int i = 0; i < points; i++)
        {
            Vector3 pos = transform.position + Vector3.right * (i * spacing - halfLength);
            basePositions.Add(pos);

            var s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            s.transform.position = pos;
            s.transform.parent = transform;
            s.name = $"Ball_{i}";

            balls.Add(s);
            ballRenderers.Add(s.GetComponent<Renderer>());
        }
    }

    void AnimateAndRenderWave()
    {
        if (points < 1) return;

        float t = Time.time * frequency * Mathf.PI * 2f;

        for (int i = 0; i < points; i++)
        {
            float phase = t + i * phaseOffset;
            float y = Mathf.Sin(phase) * amplitude;

            Vector3 wavePos = basePositions[i] + Vector3.up * y;

            line.SetPosition(i, wavePos);
            balls[i].transform.position = wavePos;

            // Normalize sine to 0–1 and sample gradient
            float normalized = Mathf.InverseLerp(-amplitude, amplitude, y);
            Color color = colorGradient.Evaluate(normalized);

            ballRenderers[i].material.color = color;
        }
    }
}
