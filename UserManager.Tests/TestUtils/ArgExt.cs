using NSubstitute;

namespace UserManager.Tests.TestUtils
{
    public static class ArgExt
    {
        public static T AnyEquivalent<T>(T item, params string[] ignoreList)
        {
#pragma warning disable NS1004 // Argument matcher used with a non-virtual member of a class.
            return Arg.Is<T>(x => ObjectComparer.AreObjectsEqual(x, item, ignoreList));
#pragma warning restore NS1004 // Argument matcher used with a non-virtual member of a class.
        }
    }
}
