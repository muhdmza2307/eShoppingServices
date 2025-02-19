using System.ComponentModel;

namespace Order.Common.Enums;

public enum OrderState
{
    [Description("Pending")] Pending = 1,
    [Description("Completed")] Completed = 2
}