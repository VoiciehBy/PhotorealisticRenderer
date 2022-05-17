using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PhotorealisticRenderer.Shapes;

namespace PhotorealisticRenderer.ObjReader;

public readonly record struct VertexReference(int Vertex, int? VertexTexture, int? VertexNormal);

public readonly record struct Face(List<VertexReference> Vertices);

public class ObjFile
{
    public string ObjectName { get; private set; }
    public List<Vector3> Vertices { get; } = new();
    public List<Vector3> VertexTextures { get; } = new();
    public List<Vector3> VectorNormals { get; } = new();
    public List<Face> Faces { get; } = new();

    protected ObjFile()
    {
    }

    public IEnumerable<Triangle> GetTriangles()
    {
        foreach (var face in Faces)
        {
            for (var i = 1; i < face.Vertices.Count - 1; i++)
            {
                var a = face.Vertices[0];
                var b = face.Vertices[i];
                var c = face.Vertices[i + 1];

                var triangle = new Triangle(
                    Vertices[a.Vertex - 1],
                    Vertices[b.Vertex - 1],
                    Vertices[c.Vertex - 1]);

                if (VectorNormals.Any() && a.VertexNormal.HasValue && b.VertexNormal.HasValue && c.VertexNormal.HasValue)
                {
                    triangle.SetNormals(
                        VectorNormals[a.VertexNormal.Value - 1],
                        VectorNormals[b.VertexNormal.Value - 1],
                        VectorNormals[c.VertexNormal.Value - 1]);
                }

                yield return triangle;
            }
        }
    }

    public static ObjFile Load(string path) => Load(new FileInfo(path));

    public static ObjFile Load(FileInfo path)
    {
        if (!path.Exists)
            return null;

        var lines = File.ReadAllLines(path.FullName);
        if (lines.Length == 0)
            return null;

        var file = new ObjFile();

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            if (!string.IsNullOrWhiteSpace(line) && line[0] != '#')
                file.ParseLine(line, lines, ref index);
        }

        return file;
    }

    private void ParseLine(in string line, in string[] lines, ref int index)
    {
        var split = line.Split(' ');
        if (split.Length == 0)
            return;

        switch (split[0])
        {
            case "o":
            {
                if (split.Length > 1)
                    ObjectName = line[2..];
                break;
            }
            case "v":
            {
                if (split.Length != 4)
                    throw new Exception($"The .obj file contains a `v` element with incorrect parameter amount: {line}");
                if (!split[1].TryParseDouble(out var x))
                    throw new Exception($"The .obj file contains a `v` element with a parameter that isn't a correct number: {line}");
                if (!split[2].TryParseDouble(out var y))
                    throw new Exception($"The .obj file contains a `v` element with a parameter that isn't a correct number: {line}");
                if (!split[3].TryParseDouble(out var z))
                    throw new Exception($"The .obj file contains a `v` element with a parameter that isn't a correct number: {line}");

                Vertices.Add(new Vector3(x, y, z));
                break;
            }
            case "vt":
            {
                if (split.Length is > 4 or < 2)
                    throw new Exception($"The .obj file contains a `vt` element with incorrect parameter amount: {line}");

                var v = 0d;
                var w = 0d;

                if (!split[1].TryParseDouble(out var u))
                    throw new Exception($"The .obj file contains a `vn` element with a parameter that isn't a correct number: {line}");
                if (split.Length > 2 && !split[2].TryParseDouble(out v))
                    throw new Exception($"The .obj file contains a `vn` element with a parameter that isn't a correct number: {line}");
                if (split.Length > 3 && !split[3].TryParseDouble(out w))
                    throw new Exception($"The .obj file contains a `vn` element with a parameter that isn't a correct number: {line}");

                VertexTextures.Add(new Vector3(u, v, w));

                break;
            }
            case "vn":
            {
                if (split.Length != 4)
                    throw new Exception($"The .obj file contains a `vn` element with incorrect parameter amount: {line}");
                if (!split[1].TryParseDouble(out var x))
                    throw new Exception($"The .obj file contains a `vn` element with a parameter that isn't a correct number: {line}");
                if (!split[2].TryParseDouble(out var y))
                    throw new Exception($"The .obj file contains a `vn` element with a parameter that isn't a correct number: {line}");
                if (!split[3].TryParseDouble(out var z))
                    throw new Exception($"The .obj file contains a `vn` element with a parameter that isn't a correct number: {line}");

                VectorNormals.Add(new Vector3(x, y, z));
                break;
            }
            case "f":
            {
                if (split.Length < 4)
                    throw new Exception($"The .obj file contains a `f` element with incorrect parameter amount: {line}");

                var face = new Face(new List<VertexReference>());

                for (var i = 1; i < split.Length; i++)
                {
                    var values = split[i].Split('/');
                    if (values.Length > 3)
                        throw new Exception($"The .obj file contains a `f` element with a parameter that has incorrect amount of sub-parameters: {line}");
                    if (!values[0].TryParseInt(out var vertex))
                        throw new Exception($"The .obj file contains a `f` element with a parameter that isn't a correct number: {line}");
                    int? vertexTexture = null;
                    int? vertexNormal = null;
                    if (values.Length > 1 && !string.IsNullOrWhiteSpace(values[1]) && values[1].TryParseInt(out var temp))
                        vertexTexture = temp;
                    if (values.Length > 2 && !string.IsNullOrWhiteSpace(values[2]) && values[2].TryParseInt(out temp))
                        vertexNormal = temp;

                    face.Vertices.Add(new VertexReference(vertex, vertexTexture, vertexNormal));
                }

                Faces.Add(face);

                break;
            }
        }
    }
}