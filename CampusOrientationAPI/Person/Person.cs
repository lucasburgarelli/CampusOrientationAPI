﻿using System.Diagnostics.CodeAnalysis;

namespace CampusOrientationAPI.Person;

public sealed class Person
{
    [NotNull]
    public String? Ra { get; set; }
    [NotNull]
    public String? Name { get; set; }
    public String? Password { get; set; }
}
