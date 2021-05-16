namespace WinformsTools.MVVM.Bindings
{
    public class InverseBoolConverter : SourceToControlConverter<bool, bool>
    {
        public override bool Convert(bool source) => !source;
    }
}
