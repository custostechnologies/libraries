using System;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotificationAttribute : Attribute
    {
        public OperationType OperationType { get; set; }
        public string Label { get; set; }
        public int Idx { get; set; }
        public bool IsLink { get; set; }
        public NotificationAttribute(OperationType operationType, string label, int index, bool isLink)
        {
            OperationType = operationType;
            Label = label;
            Idx = index;
            IsLink = isLink;
        }
    }
}