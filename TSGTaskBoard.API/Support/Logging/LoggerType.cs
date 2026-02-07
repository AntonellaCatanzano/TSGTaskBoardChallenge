using System.Runtime.Serialization;

namespace TSGTaskBoard.API.Support.Logging
{
    public enum LoggerType
    {
        [EnumMember(Value = "Console")]
        CONSOLE,

        [EnumMember(Value = "File")]
        FILE
    }
}
