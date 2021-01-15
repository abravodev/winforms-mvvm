using System;
using System.Linq;
using System.Collections.Generic;

namespace UserManager.Startup
{
    public class ExecutionParameters
    {
        private const char ParameterSeparator = '=';

        public ExecutionParameters(string[] parameters) : this(parameters.Select(x => Parse(x)))
        {
            
        }

        public ExecutionParameters(IEnumerable<(string Name, string Value)> parameters)
        {
            Parameters = parameters.ToDictionary(x => x.Name, x => x.Value);
        }

        public IDictionary<string, string> Parameters { get; }

        public static (string Name, string Value) Parse(string parameter, char separator = ParameterSeparator)
        {
            var parts = parameter.Split(separator);
            switch (parts.Length)
            {
                case 1: return (parts[0], parts[0]);
                case 2: return (parts[0], parts[1]);
                default: throw new ArgumentException($"{nameof(parameter)} has more than one separator '{separator}' ({parameter})");
            }
        }

        public string Get(string parameterName) => Parameters.ContainsKey(parameterName) ? Parameters[parameterName] : null;

        public static ExecutionParameters Create(params (string Name, string Value)[] parameters)
        {
            return new ExecutionParameters(parameters);
        }

        public override string ToString()
        {
            return string.Join(" ", Parameters.Select(x => ToString(x.Key, x.Value)));
        }

        private string ToString(string parameterName, string parameterValue)
        {
            if (parameterName.Equals(parameterValue))
            {
                return parameterName;
            }

            return $"{parameterName}{ParameterSeparator}{parameterValue}";
        }
    }
}
