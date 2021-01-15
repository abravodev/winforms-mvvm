using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using WinformsTools.MVVM.Bindings;

namespace UserManager.Tests.TestUtils
{
    public class PropertyChangeListener
    {
        private readonly BindableObject _bindableObject;
        private readonly PropertyInfo[] _properties;

        public PropertyChangeListener(BindableObject bindableObject)
        {
            _bindableObject = bindableObject;
            _properties = _bindableObject.GetType().GetProperties();
        }

        public List<PropertyChangedEvent> Changes { get; } = new List<PropertyChangedEvent>();

        public static PropertyChangeListener Start(BindableObject bindableObject)
        {
            return new PropertyChangeListener(bindableObject).Start();
        }

        private PropertyChangeListener Start()
        {
            _bindableObject.PropertyChanged += BindableObject_PropertyChanged;
            return this;
        }

        public PropertyChangeListener Stop()
        {
            _bindableObject.PropertyChanged -= BindableObject_PropertyChanged;
            return this;
        }

        public List<PropertyChangedEvent<TProperty>> GetChanges<TProperty>(string propertyName)
        {
            return this.Changes
                .Where(x => x.PropertyName == propertyName)
                .Select(x => x.Map<TProperty>())
                .ToList();
        }

        private void BindableObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEvent changedEvent = new PropertyChangedEvent(
                propertyName: e.PropertyName,
                value: GetPropertyValue(e.PropertyName),
                timestamp: DateTime.UtcNow);
            this.Changes.Add(changedEvent);
        }

        private object GetPropertyValue(string propertyName)
        {
            var property = _properties.FirstOrDefault(x => x.Name == propertyName);
            return property.GetValue(_bindableObject);
        }

        public class PropertyChangedEvent<TProperty>
        {
            public PropertyChangedEvent(string propertyName, TProperty value, DateTime timestamp)
            {
                PropertyName = propertyName;
                Value = value;
                Timestamp = timestamp;
            }

            public string PropertyName { get; }

            public TProperty Value { get; }

            public DateTime Timestamp { get; }
        }

        public class PropertyChangedEvent
        {
            public PropertyChangedEvent(string propertyName, object value, DateTime timestamp)
            {
                PropertyName = propertyName;
                Value = value;
                Timestamp = timestamp;
            }

            public string PropertyName { get; }

            public object Value { get; }

            public DateTime Timestamp { get; }

            public PropertyChangedEvent<TProperty> Map<TProperty>()
            {
                return new PropertyChangedEvent<TProperty>(PropertyName, (TProperty)Value, Timestamp);
            }
        }
    }
}
