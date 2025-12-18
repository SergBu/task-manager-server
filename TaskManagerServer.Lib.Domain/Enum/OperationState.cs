using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TaskManagerServer.Lib.Domain.Enum;

[JsonConverter(typeof(StringEnumConverter))]
public enum OperationState
{
    /// <summary>
    /// inProcess
    /// </summary>
    [EnumMember(Value = "inProcess")]
    InProcess,

    /// <summary>
    /// completed
    /// </summary>
    [EnumMember(Value = "completed")]
    Completed,

    /// <summary>
    /// failed
    /// </summary>
    [EnumMember(Value = "failed")]
    Failed
}
