using Serilog.Parsing;
using System.Text;

namespace WinformsTools.MVVM.Tests.TestUtils
{
    public static class SerilogFormatter
    {
        public static string Format(string messageTemplate, object[] messageParams)
        {
            var parser = new MessageTemplateParser();
            var template = parser.Parse(messageTemplate);
            var format = new StringBuilder();
            var index = 0;
            foreach (var token in template.Tokens)
            {
                if (token is TextToken)
                    format.Append(token);
                else
                    format.Append("{" + index++ + "}");
            }
            var netStyle = format.ToString();
            return string.Format(netStyle, messageParams);
        }
    }
}
