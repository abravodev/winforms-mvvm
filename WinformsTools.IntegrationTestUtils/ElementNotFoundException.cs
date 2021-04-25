using FlaUI.Core.Definitions;
using System;

namespace WinformsTools.IntegrationTestUtils
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(ControlType controlType, string name)
            : base($"Element of type '{controlType}' and name '{name}' not found")
        {

        }
    }
}
