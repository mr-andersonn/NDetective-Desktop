using System;

namespace NDetective.ViewModels;

public class ListItemTemplate
{
    public ListItemTemplate(Type type)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");
    }
        
    public string Label { get; }
    public Type ModelType { get; }
}