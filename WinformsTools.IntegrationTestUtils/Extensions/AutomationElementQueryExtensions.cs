using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using WinformsTools.IntegrationTestUtils.Elements;

namespace WinformsTools.IntegrationTestUtils.Extensions
{
    public static class AutomationElementQueryExtensions
    {
        public static ModalElement GetModalByTitle(this Window window, string name)
        {
            return Retry.WhileNull(() => window.ModalWindows.SingleOrDefault(x => x.Title.Equals(name))).Result.AsModal();
        }

        public static TAutomationElement Get<TAutomationElement>(this AutomationElement automationElement, string inputName, ControlType type)
            where TAutomationElement : AutomationElement
        {
            var info = ElementInfo.Get<TAutomationElement>();
            var element = automationElement.FindFirstDescendant(x => 
                x.ByControlType(type).And(x.ByName(inputName)));
            return info.Map(element) as TAutomationElement;
        }

        public static TAutomationElement Get<TAutomationElement>(this AutomationElement automationElement, string inputName)
            where TAutomationElement : AutomationElement
        {
            var info = ElementInfo.Get<TAutomationElement>();
            var element = automationElement.FindFirstDescendant(x => 
                x.ByControlType(info.ControlType).And(x.ByName(inputName)));
            return info.Map(element) as TAutomationElement;
        }

        public static TAutomationElement[] GetAllChildren<TAutomationElement>(this AutomationElement automationElement)
            where TAutomationElement : AutomationElement
        {
            var info = ElementInfo.Get<TAutomationElement>();
            var element = automationElement.FindAllChildren(x => x.ByControlType(info.ControlType));
            return element.Select(x => info.Map(x) as TAutomationElement).ToArray();
        }

        private class ElementInfo
        {
            public ElementInfo(Type type, ControlType controlType, Func<AutomationElement, AutomationElement> map)
            {
                Type = type;
                ControlType = controlType;
                Map = map;
            }

            public Type Type { get; }

            public ControlType ControlType { get; }

            public Func<AutomationElement, AutomationElement> Map { get; }

            public static ElementInfo Native<TAutomationElement>(ControlType controlType)
                where TAutomationElement : AutomationElement
            {
                return new ElementInfo(typeof(TAutomationElement), controlType, x => x.As<TAutomationElement>());
            }

            public static ElementInfo Get<TAutomationElement>() where TAutomationElement : AutomationElement
            {
                var info = All.FirstOrDefault(x => x.Type == typeof(TAutomationElement));
                if (info == null)
                {
                    throw new ArgumentException($"Invalid type = {typeof(TAutomationElement).Name}", nameof(TAutomationElement));
                }

                return info;
            }

            private static List<ElementInfo> All => new List<ElementInfo>
            {
                ElementInfo.Native<Button>(ControlType.Button),
                ElementInfo.Native<ComboBox>(ControlType.ComboBox),
                ElementInfo.Native<DataGridView>(ControlType.Table),
                ElementInfo.Native<Menu>(ControlType.Menu),
                ElementInfo.Native<MenuItem>(ControlType.MenuItem),
                ElementInfo.Native<TextBox>(ControlType.Text),
                // TODO: Continue with the list

                new ElementInfo(typeof(VisualForm), ControlType.Pane, x => x.AsForm())
                // TODO: Continue with the list
            };
        }
    }
}
