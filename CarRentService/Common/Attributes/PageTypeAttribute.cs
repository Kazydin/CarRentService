using System;

namespace CarRentService.Common.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PageTypeAttribute : Attribute
{
    public Type PageType { get; }

    public PageTypeAttribute(Type pageType)
    {
        PageType = pageType ?? throw new ArgumentNullException(nameof(pageType));
    }
}

