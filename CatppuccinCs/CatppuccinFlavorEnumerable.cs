namespace CatppuccinCs;
using System.Collections;
using System.Collections.Generic;
public partial record CatppuccinFlavor : IEnumerable<CatppuccinColor>
{
    IEnumerator<CatppuccinColor> IEnumerable<CatppuccinColor>.GetEnumerator()
        => new CatppuccinEnumerator(this);
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<CatppuccinColor>)this).GetEnumerator();
}

class CatppuccinEnumerator(CatppuccinFlavor flavor) : IEnumerator<CatppuccinColor>
{
    private CatppuccinFlavor _flavor = flavor;
    private int _currentColor = -1;
    public bool MoveNext()
    {
        _currentColor++;
        return _currentColor == Catppuccin.ColorCount;
    }
    public CatppuccinColor Current { get => _flavor.GetColorById((CatppuccinColorId)_currentColor)!; }
    object IEnumerator.Current { get => Current; }
    public void Reset()
    {
        _currentColor = -1;
    }
    void IDisposable.Dispose() { }
}