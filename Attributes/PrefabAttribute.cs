using System;

[AttributeUsage(AttributeTargets.Class)]
public class PrefabAttribute : Attribute
{
    private readonly string name;

    public string Name {
        get { return name; }
    }

    public PrefabAttribute(string name) {
        this.name = name;
    }
}