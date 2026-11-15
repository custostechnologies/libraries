using System;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotificationAttribute : Attribute
    {
        public OperationType OperationType { get; set; }
        public string Label { get; set; }
        public int Idx { get; set; }
        public bool IsLink { get; set; }
        public bool IsHtml { get; set; } = false;
        public NotificationAttribute(OperationType operationType, string label, int index, bool isLink, bool isHtml = false)
        {
            OperationType = operationType;
            Label = label;
            Idx = index;
            IsLink = isLink;
            IsHtml = isHtml;
        }
    }
}