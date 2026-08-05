using System.Collections.Generic;
using UnityEngine;

public class ColourManager
{
    private List<ColourSource> colourSources;
    private Vector4 _combinedColour;
    private SpriteRenderer _sprite;

    public void AddOrChangeContribution(string name, bool add, Color color, float amount)
    {
        ColourSource source = colourSources.Find(source => source.Name == name);
        if (source != null) 
        { 
            source = new ColourSource(name, add, new Vector3(color.r, color.g, color.b), amount);
            colourSources.Add(source);
        }
        else
        {
            source.UpdateColour(add, new Vector3(color.r, color.g, color.b), amount);
        }
    }

    private void UpdateCombinedColour()
    {
        _combinedColour = new Vector4(0.5f, 0.5f, 0.5f, 1);
        foreach (var source in colourSources)
        {
            if (source.Add)
            {
                _combinedColour += source.Amount / 255 * new Vector4(source.Colour.x, source.Colour.y, source.Colour.z, 0);
            }
            else
            {
                _combinedColour -= source.Amount / 255 * new Vector4(source.Colour.x, source.Colour.y, source.Colour.z, 0);
            }
        }
        _sprite.color = _combinedColour;
    }
}
